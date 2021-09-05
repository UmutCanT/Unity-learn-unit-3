using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    GameObject[] obstaclePrefab;

    Vector3 spawnPos = new Vector3(25, 0, 0);
    Vector3 playerSpawn = new Vector3(-5, 0, 0);

    float spawnRate = 2f;
    float spawnerDelay = 2f;

    void Awake()
    {
        Instantiate(playerPrefab, playerSpawn, Quaternion.Euler(0, 90, 0));
    }

    // Start is called before the first frame update
    void Start()
    {       
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("ObstacleSpawner", spawnerDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ObstacleSpawner()
    {
        if (!playerController.GameOver)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, Quaternion.identity);
        }  
    }
}
