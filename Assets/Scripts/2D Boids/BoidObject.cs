using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidObject : MonoBehaviour
{

    BoidsManager GroupHandler;
    public BoidsManager BoidGroup { get { return GroupHandler; } }

    Collider2D BoidCollider;
    public Collider2D BoidsCollider { get { return BoidCollider; } }

    float RightConstraint = 10;
    float TopConstraint = 5;
    float Buffer = 0.5f;

    private void Start()
    {
        BoidCollider = GetComponent<Collider2D>();
    }

    public void Initialize(BoidsManager Boids)
    {
        GroupHandler = Boids;
    }

    public void Move(Vector2 Velocity)
    {
        //RESTRICT BOIDS WITHIN A AREA
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

        transform.up = Velocity;
        transform.position += (Vector3)Velocity * Time.deltaTime;
    }
}
