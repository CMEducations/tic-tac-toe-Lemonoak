using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Separation")]
public class Separation : FilteredBoidBehavior
{
    public override Vector2 CalculateMove(BoidObject Boid, List<Transform> NeighbourBoids, BoidsManager AllBoids)
    {
        if (NeighbourBoids.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 SeparationMove = Vector2.zero;
        int AvoidObjects = 0;
        foreach (Transform Item in NeighbourBoids)
        {
            if (Vector2.SqrMagnitude(Item.position - Boid.transform.position) < AllBoids.SquareAvoidanceRadiusGet)
            {
                AvoidObjects++;
                SeparationMove += (Vector2)(Boid.transform.position - Item.position);
            }
        }
        if(AvoidObjects > 0)
        {
            SeparationMove /= AvoidObjects;
        }
        return SeparationMove;
    }
}
