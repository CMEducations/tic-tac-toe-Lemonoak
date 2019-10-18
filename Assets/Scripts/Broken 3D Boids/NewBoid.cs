using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoid : MonoBehaviour
{
    float MoveSpeed = 5.0f;
    float NeighbourDistanceRadius = 3.0f;
    public float RotationSpeed = 5.0f;


    [SerializeField]
    Vector3 AverageDirection = Vector3.zero;
    [SerializeField]
    Vector3 AveragePosition = Vector3.zero;
    [SerializeField]
    Vector3 AwayDirection = Vector3.zero;

    bool bSeparation = true;
    bool bAlignment = true;
    bool bCohersion = true;

    public float RightConstraint = 20;
    public float TopConstraint = 20;
    public float DepthConstraint = 20;
    float Buffer = 0.5f;
    private void Update()
    {

        bSeparation = NewBoidManager.Instance.Separation;
        bAlignment = NewBoidManager.Instance.Alignment;
        bCohersion = NewBoidManager.Instance.Cohersion;

        Move(Alignment(), Cohesion(), Separation());
        BoidsMovement();
    }


    void Move(Vector3 Alignment, Vector3 Cohesion, Vector3 Separation)
    {

        if (transform.position.x > RightConstraint + Buffer)
        {
            transform.position = new Vector3(-RightConstraint - Buffer, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -RightConstraint - Buffer)
        {
            transform.position = new Vector3(RightConstraint + Buffer, transform.position.y, transform.position.z);
        }

        if (transform.position.y > TopConstraint + Buffer)
        {
            transform.position = new Vector3(transform.position.x, -TopConstraint - Buffer, transform.position.z);
        }
        if (transform.position.y < -TopConstraint - Buffer)
        {
            transform.position = new Vector3(transform.position.x, TopConstraint + Buffer, transform.position.z);
        }

        if (transform.position.z > DepthConstraint + Buffer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -DepthConstraint - Buffer);
        }
        if (transform.position.z < -DepthConstraint - Buffer)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, DepthConstraint + Buffer);
        }

        transform.Translate(0, 0, Time.deltaTime * MoveSpeed);
        //transform.LookAt(Alignment + Separation +Cohesion);
        //transform.rotation = Quaternion.LookRotation(Alignment + Separation + Cohesion);
    }

    void BoidsMovement()
    {
        GameObject[] OtherBoids = NewBoidManager.AllBoids;

        float Distance;
        int NeighbourBoids = 0;

        Vector3 AwayDirection = Vector3.zero;
        Vector3 BoidsCenter = Vector3.zero;
        for (int i = 0; i < NewBoidManager.AmountOfBoids; i++)
        {
            Distance = Vector3.Distance(this.transform.position, OtherBoids[i].transform.position);
            if (Distance <= NeighbourDistanceRadius)
            {
                BoidsCenter += OtherBoids[i].transform.position;
                NeighbourBoids++;

                if (Distance < NeighbourDistanceRadius / 2)
                {
                    AwayDirection += this.transform.position - OtherBoids[i].transform.position;
                }
            }
        }
        if (NeighbourBoids > 0)
        {
            BoidsCenter = BoidsCenter / NeighbourBoids;

            Vector3 Direction = (BoidsCenter + AwayDirection) - this.transform.position;
            if (Direction != Vector3.zero)
            {
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * RotationSpeed);
            }
        }
    }

    //Steer away from boids too close to you
    Vector3 Separation()
    {
        GameObject[] Boids = NewBoidManager.AllBoids;

        float Distance;
        int BoidAmount = 0;

        Vector3 Difference = Vector3.zero;

        for (int i = 0; i < NewBoidManager.AmountOfBoids; i++)
        {
            Distance = Vector3.Distance(transform.position, Boids[i].transform.position);
            if (Distance < NeighbourDistanceRadius / 2)
            {
                Difference += transform.position - Boids[i].transform.position;
                BoidAmount++;
            }
        }
        AwayDirection = Difference;
        if(bSeparation)
            return AwayDirection;

        return Vector3.zero;
    }

    //Steer towards the same heading has the other boids in the current boid group
    Vector3 Alignment()
    {
        GameObject[] Boids = NewBoidManager.AllBoids;

        float Distance;
        int BoidAmount = 0;

        for (int i = 0; i < NewBoidManager.AmountOfBoids; i++)
        {
            Distance = Vector3.Distance(transform.position, Boids[i].transform.position);
            if(Distance < NeighbourDistanceRadius)
            {
                AverageDirection += Boids[i].transform.forward;
                BoidAmount++;
            }
        }

        if(BoidAmount > 0)
        {
            AverageDirection = AverageDirection / BoidAmount;
        }

        AverageDirection.Normalize();
        if (bAlignment)
            return AverageDirection;

        return Vector3.zero;
    }

    //Steer towards the middle position of the current boid group
    Vector3 Cohesion()
    {
        GameObject[] Boids = NewBoidManager.AllBoids;

        float Distance;
        int BoidAmount = 0;

        for (int i = 0; i < NewBoidManager.AmountOfBoids; i++)
        {
            Distance = Vector3.Distance(transform.position, Boids[i].transform.position);
            if (Distance < NeighbourDistanceRadius)
            {
                AveragePosition += Boids[i].transform.position;
                BoidAmount++;
            }
        }

        if (BoidAmount > 0)
        {
            AveragePosition = AveragePosition - transform.position;
        }

        if (bCohersion)
            return AveragePosition;

        return Vector3.zero;
    }

}
