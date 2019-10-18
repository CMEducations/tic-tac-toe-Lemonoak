using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public BoidObject BoidPrefab;
    List<BoidObject> Boids = new List<BoidObject>();
    public BoidBehavior Behavior;

    public int BoidAmount = 100;
    const float BoidDensity = 0.08f;

    public float MoveSpeed = 10.0f;
    public float MaxMoveSpeed = 5.0f;

    public float NeighbourRadius = 1.0f;
    public float AvoidanceRadius = 0.5f;

    float SquareMaxSpeed;
    float SquareNeighbourRadius;
    float SquareAvoidanceRadius;
    public float SquareAvoidanceRadiusGet { get { return SquareAvoidanceRadius; } }

    public Target Target;
    public GameObject TargetObj;

    private void Start()
    {
        SquareMaxSpeed = MaxMoveSpeed * MaxMoveSpeed;
        SquareNeighbourRadius = NeighbourRadius * NeighbourRadius;
        SquareAvoidanceRadius = SquareNeighbourRadius * AvoidanceRadius * AvoidanceRadius;

        for (int i = 0; i < BoidAmount; i++)
        {
            BoidObject NewBoid = Instantiate(BoidPrefab, Random.insideUnitCircle * BoidAmount * BoidDensity, Quaternion.Euler(Vector3.forward * Random.Range(0.0f, 360.0f)), this.transform);
            NewBoid.name = "Boid " + i;
            NewBoid.Initialize(this);
            Boids.Add(NewBoid);
        }
    }

    private void Update()
    {
        foreach(BoidObject Boid in Boids)
        {
            List<Transform> NeighbourObjects = GetNearbyObjects(Boid);

            //Boid.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, NeighbourObjects.Count / 6.0f);

            Vector2 Move = Behavior.CalculateMove(Boid, NeighbourObjects, this);
            Move *= MoveSpeed;
            if (Move.sqrMagnitude > SquareMaxSpeed)
            {
                Move = Move.normalized * MaxMoveSpeed;
            }
            Boid.Move(Move);
        }

        //SET TARGET ON MOUSE
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TargetPos.z = 0;
        if(Input.GetKey(KeyCode.Mouse0))
        {
            TargetObj.SetActive(true);
            Target.TargetTrans = TargetObj.transform.position;
            TargetObj.transform.position = TargetPos;
        }
        else
        {
            Target.TargetTrans = Vector2.zero;
            TargetObj.SetActive(false);
        }
    }

    List<Transform> GetNearbyObjects(BoidObject Boid)
    {
        List<Transform> NeighbourObjects = new List<Transform>();
        Collider2D[] NeighbourColliders = Physics2D.OverlapCircleAll(Boid.transform.position, NeighbourRadius);
        foreach(Collider2D Colliders in NeighbourColliders)
        {
            if( Colliders != Boid.BoidsCollider)
            {
                NeighbourObjects.Add(Colliders.transform);
            }
        }
        return NeighbourObjects;
    }
}
