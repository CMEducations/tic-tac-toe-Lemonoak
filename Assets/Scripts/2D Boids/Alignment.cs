using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Alignment")]
public class Alignment : FilteredBoidBehavior
{
    public override Vector2 CalculateMove(BoidObject Boid, List<Transform> NeighbourBoids, BoidsManager AllBoids)
    {
        if (NeighbourBoids.Count == 0)
        {
            return Boid.transform.up;
        }

        Vector2 AlignmentMove = Vector2.zero;
        foreach (Transform Item in NeighbourBoids)
        {
            AlignmentMove += (Vector2)Item.transform.up;
        }
        AlignmentMove /= NeighbourBoids.Count;
        return AlignmentMove;
    }
}
