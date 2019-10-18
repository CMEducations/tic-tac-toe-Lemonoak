using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsTracker : MonoBehaviour
{
    //Boid to spawn
    public GameObject BoidPrefab;

    //How many boids to spawn
    public static int BoidAmount = 20;
    public static GameObject[] AllBoids = new GameObject[BoidAmount];

    //Spawn boids stuff
    Vector3 SpawnPosition;
    public float SpawnRange = 10;

    public static float GoalRange = 20;
    public static Vector3 GoalPosition = Vector3.zero;

    void Start()
    {
        for (int i = 0; i < BoidAmount; i++)
        {
            RandomizeBoidsSpawn();
            AllBoids[i] = Instantiate(BoidPrefab, SpawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        FindGoalPosition();
    }

    void RandomizeBoidsSpawn()
    {
        SpawnPosition = new Vector3(Random.Range(-SpawnRange, SpawnRange),
                                    Random.Range(-SpawnRange, SpawnRange),
                                    Random.Range(-SpawnRange, SpawnRange));
    }

    void FindGoalPosition()
    {
        if(Random.Range(0, 10000) < 50)
        {
            GoalPosition = new Vector3(Random.Range(-GoalRange, GoalRange),
                                       Random.Range(-GoalRange, GoalRange),
                                       Random.Range(-GoalRange, GoalRange));
        }
    }
}
