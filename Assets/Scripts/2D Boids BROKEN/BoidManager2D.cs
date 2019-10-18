using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager2D : MonoBehaviour
{
    public GameObject BoidToSpawn;

    public static int AmountOfBoids = 10;
    public static GameObject[] Boids = new GameObject[AmountOfBoids];

    Vector2 SpawnPosition = Vector2.zero;
    float SpawnRange = 3;

    void Start()
    {
        for (int i = 0; i < AmountOfBoids; i++)
        {
            RandomizeBoidsSpawn();
            Boids[i] = Instantiate(BoidToSpawn, SpawnPosition, Quaternion.identity);
        }
    }

    void RandomizeBoidsSpawn()
    {
        SpawnPosition = new Vector3(Random.Range(-SpawnRange, SpawnRange),
                                    Random.Range(-SpawnRange, SpawnRange),
                                    2.0f);
    }
}
