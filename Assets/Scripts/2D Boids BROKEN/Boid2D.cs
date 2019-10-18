using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid2D : MonoBehaviour
{

    public float RightConstraint = 10;
    public float TopConstraint = 5;
    float Buffer = 0.5f;

    public float MoveSpeed = 5.0f;
    public float RotationSpeed = 5.0f;

    float NeighbourDistance = 6.0f;

    [SerializeField]
    Vector2 Direction;

    private void Update()
    {
        Move();
        BoidsMovement();
    }

    void Move()
    {
        //RESTRICT BOIDS WITHIN A AREA
        if(transform.position.x > RightConstraint + Buffer)
        {
            transform.position = new Vector3(-RightConstraint - Buffer, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -RightConstraint - Buffer)
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

        this.transform.Translate(Time.deltaTime * Mathf.Clamp(MoveSpeed, 0.0f, 5.0f), 0, 0);

        //Direction = transform.right * Separation() * Alignment() * Cohesion();
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * RotationSpeed);
    }

    void BoidsMovement()
    {
        GameObject[] OtherBoids = BoidManager2D.Boids;

        float Distance;
        int NeighbourBoids = 0;

        Vector3 AwayDirection = Vector3.zero;
        Vector3 BoidsCenter = Vector3.zero;
        for (int i = 0; i < BoidManager2D.AmountOfBoids; i++)
        {
            Distance = Vector2.Distance(this.transform.position, OtherBoids[i].transform.position);
            if (Distance <= NeighbourDistance)
            {
                BoidsCenter += OtherBoids[i].transform.position;
                NeighbourBoids++;

                if(Distance < NeighbourDistance/2)
                {
                    AwayDirection += this.transform.position - OtherBoids[i].transform.position;
                }
            }
        }
        if(NeighbourBoids > 0)
        {
            BoidsCenter = BoidsCenter / NeighbourBoids;

            Vector3 Direction = (BoidsCenter + AwayDirection) - this.transform.position;
            if(Direction != Vector3.zero)
            {
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), Time.deltaTime * RotationSpeed);
            }
        }
    }

    //Steer away from boids too close to you
    Vector2 Separation()
    {
        GameObject[] OtherBoids = BoidManager2D.Boids;

        float Distance;
        int NeighbourBoids = 0;

        Vector3 AwayDirection = Vector2.zero;
        for (int i = 0; i < BoidManager2D.AmountOfBoids; i++)
        {
            Distance = Vector2.Distance(this.transform.position, OtherBoids[i].transform.position);
            if(Distance <= NeighbourDistance)
            {
                AwayDirection += this.transform.position - OtherBoids[i].transform.position;
                NeighbourBoids++;
            }
        }
        if (NeighbourBoids > 0)
            return AwayDirection;

        return Vector2.zero;
    }

    //Steer towards the same heading has the other boids in the current boid group
    Vector2 Alignment()
    {
        GameObject[] OtherBoids = BoidManager2D.Boids;

        float Distance;
        int NeighbourBoids = 0;

        Vector3 AverageHeading = Vector2.zero;
        for (int i = 0; i < BoidManager2D.AmountOfBoids; i++)
        {
            Distance = Vector2.Distance(this.transform.position, OtherBoids[i].transform.position);
            if (Distance <= NeighbourDistance)
            {
                AverageHeading += OtherBoids[i].transform.forward;
                NeighbourBoids++;
            }
        }
        if (NeighbourBoids > 0)
            return AverageHeading;

        return Vector2.zero;
    }

    //Steer towards the middle position of the current boid group
    Vector2 Cohesion()
    {
        GameObject[] OtherBoids = BoidManager2D.Boids;

        float Distance;
        int NeighbourBoids = 0;

        Vector3 MiddleOfGroup = Vector2.zero;
        for (int i = 0; i < BoidManager2D.AmountOfBoids; i++)
        {
            Distance = Vector2.Distance(this.transform.position, OtherBoids[i].transform.position);
            if (Distance <= NeighbourDistance)
            {
                MiddleOfGroup += OtherBoids[i].transform.forward;
                NeighbourBoids++;
            }
        }
        if (NeighbourBoids > 0)
        {
            MiddleOfGroup = MiddleOfGroup - transform.position;
            return MiddleOfGroup;
        }

        return Vector2.zero;
    }
}
