using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Cohesion")]
public class Cohesion : FilteredBoidBehavior
{
    public override Vector2 CalculateMove(BoidObject Boid, List<Transform> NeighbourBoids, BoidsManager AllBoids)
    {
        if(NeighbourBoids.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 CohesionMove = Vector2.zero;
        foreach(Transform Item in NeighbourBoids)
        {
            CohesionMove += (Vector2)Item.position;
        }
        CohesionMove /= NeighbourBoids.Count;

        CohesionMove -= (Vector2)Boid.transform.position;
        return CohesionMove;
    }
}
