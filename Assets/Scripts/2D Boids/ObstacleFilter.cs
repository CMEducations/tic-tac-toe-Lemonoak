using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Filter/Obstacle Layer")]
public class ObstacleFilter : BoidFilter
{
    public LayerMask ObstacleMask;
    public override List<Transform> Filter(BoidObject Boids, List<Transform> Original)
    {
        List<Transform> Filtered = new List<Transform>();
        foreach(Transform Item in Original)
        {
            if(ObstacleMask == (ObstacleMask | (1 << Item.gameObject.layer)))
            {
                Filtered.Add(Item);
            }
        }
        return Filtered;
    }
}
