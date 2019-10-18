using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(BoidObject Boid, List<Transform> NeighbourBoids, BoidsManager AllBoids);
}
