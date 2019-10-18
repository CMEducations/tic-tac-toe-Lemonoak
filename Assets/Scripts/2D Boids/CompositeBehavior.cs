using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boid/Behavior/CompositeBehavior")]
public class CompositeBehavior : BoidBehavior
{
    public BoidBehavior[] Behaviours;
    public float[] Weights;

    public override Vector2 CalculateMove(BoidObject Boid, List<Transform> NeighbourBoids, BoidsManager AllBoids)
    {
        if(Weights.Length != Behaviours.Length)
        {
            Debug.LogError("WEIGHTS AND BEHAVIOURS ARE NOT THE SAME AMOUNTS");
            return Vector2.zero;
        }

        Vector2 Move = Vector2.zero;
        for (int i = 0; i < Behaviours.Length; i++)
        {
            Vector2 PartialMove = Behaviours[i].CalculateMove(Boid, NeighbourBoids, AllBoids) * Weights[i];

            if(PartialMove != Vector2.zero)
            {
                if(PartialMove.sqrMagnitude > Weights[i] * Weights[i])
                {
                    PartialMove.Normalize();
                    PartialMove *= Weights[i];
                }

                Move += PartialMove;
            }
        }

        return Move;
    }
}
