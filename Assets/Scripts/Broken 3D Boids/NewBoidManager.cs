using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoidManager : MonoBehaviour
{
    public static NewBoidManager Instance;

    public GameObject BoidToSpawn;

    public static int AmountOfBoids = 100;
    public static GameObject[] AllBoids = new GameObject[AmountOfBoids];

    Vector3 SpawnPosition = Vector3.zero;
    public float SpawnRange = 10.0f;

    public static float XMaxPosition = 20.0f;
    public static float YMaxPosition = 20.0f;
    public static float ZMaxPosition = 20.0f;

    public bool Separation = true;
    public bool Alignment = true;
    public bool Cohersion = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < AmountOfBoids; i++)
        {
            RandomizeBoidsSpawn();
            AllBoids[i] = Instantiate(BoidToSpawn, SpawnPosition, Quaternion.identity);
        }
    }

    void RandomizeBoidsSpawn()
    {
        SpawnPosition = new Vector3(Random.Range(-SpawnRange, SpawnRange),
                                    Random.Range(-SpawnRange, SpawnRange),
                                    Random.Range(-SpawnRange, SpawnRange));
    }
}
