using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    public float MoveSpeed = 1.0f;
    float RotationSpeed = 10.0f;

    Vector3 AverageGoal;
    Vector3 AveragePosition;

    float NeighbourDistance = 2.0f;

    bool Turning = false;

    private void Start()
    {
        MoveSpeed = 1.0f;
    }
    void Update()
    {
        if (Vector3.Distance(this.transform.position, Vector3.zero) >= BoidsTracker.GoalRange)
        {
            Turning = true;
        }
        else
        {
            Turning = false;
        }

        if(Turning)
        {
            Vector3 Direction = Vector3.zero - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(Direction),
                                                  RotationSpeed * Time.deltaTime);
        }
        else
        {
            MoveWithOtherBoids();
        }

        transform.Translate(0, 0, Time.deltaTime * Mathf.Clamp(MoveSpeed, 0.0f, 5.0f));
    }

    void MoveWithOtherBoids()
    {
        GameObject[] OtherBoids;
        OtherBoids = BoidsTracker.AllBoids;


        Vector3 GroupCenter = Vector3.zero;
        Vector3 AvoidGroup = Vector3.zero;
        float GroupSpeed = 5.0f;

        Vector3 GroupGoalPosition = BoidsTracker.GoalPosition;

        float Distance;

        int GroupSize = 0;
        foreach (GameObject Boids in OtherBoids)
        {
            if(Boids != this.gameObject)
            {
                Distance = Vector3.Distance(Boids.transform.position, this.transform.position);
                if(Distance <= NeighbourDistance)
                {
                    GroupCenter += Boids.transform.position;
                    GroupSize++;

                    if(Distance < 1.0f)
                    {
                        AvoidGroup = AvoidGroup + (this.transform.position - Boids.transform.position);
                    }

                    Boid AnotherBoid = Boids.GetComponent<Boid>();
                    GroupSpeed = GroupSpeed + AnotherBoid.MoveSpeed;
                }
            }
        }

        if(GroupSize > 0)
        {
            GroupCenter = GroupCenter / GroupSize + (GroupGoalPosition - this.transform.position);
            MoveSpeed = GroupSpeed / GroupSize;

            Vector3 Direction = (GroupCenter + AvoidGroup) - this.transform.position;
            if( Direction != Vector3.zero)
            {
                this.transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                      Quaternion.LookRotation(Direction), 
                                                      RotationSpeed * Time.deltaTime);
            }
        }
    }
}
