using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/Target")]
public class Target : FilteredBoidBehavior
{
    public Vector2 TargetTrans;

    public Vector2 TargetTransform(Transform Target)
    {
        TargetTrans = Target.transform.position;
        return Target.transform.position;
    }
    public override Vector2 CalculateMove(BoidObject Boid, List<Transform> NeighbourBoids, BoidsManager AllBoids)
    {
        if(TargetTrans == Vector2.zero)
        {
            return Vector2.zero;
        }
        Vector2 TargetMove = TargetTrans;

        return TargetMove;
    }
}
