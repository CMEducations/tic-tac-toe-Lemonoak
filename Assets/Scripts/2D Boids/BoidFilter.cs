using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoidFilter : ScriptableObject
{
    public abstract List<Transform> Filter(BoidObject Boids, List<Transform> Original);
}
