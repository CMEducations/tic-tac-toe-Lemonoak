using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/SmoothCohesion")]
public class SmoothCohesion : FilteredBoidBehavior
{
    Vector2 CurrentVelocity = Vector2.zero;
    public float BoidSmoothTime = 0.5f;
    public override Vector2 CalculateMove(BoidObject Boid, List<Transform> NeighbourBoids, BoidsManager AllBoids)
    {
        if (NeighbourBoids.Count == 0)
        {
            return Vector2.zero;
        }

        Vector2 CohesionMove = Vector2.zero;
        List<Transform> FilterStuff = (Filter == null) ? NeighbourBoids : Filter.Filter(Boid, NeighbourBoids);
        foreach (Transform Item in FilterStuff)
        {
            CohesionMove += (Vector2)Item.position;
        }
        CohesionMove /= NeighbourBoids.Count;

        CohesionMove -= (Vector2)Boid.transform.position;
        if (float.IsNaN(CurrentVelocity.x) || float.IsNaN(CurrentVelocity.y))
            CurrentVelocity = Vector2.zero;
        CohesionMove = Vector2.SmoothDamp(Boid.transform.up, CohesionMove, ref CurrentVelocity, BoidSmoothTime);
        return CohesionMove;
    }
}
