using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectPrefabs = new GameObject[4];
    private GameObject objectPrefab;
    
    private static float xRange = 6;
    private static float xPos;
    private static float zPos;
    private Vector3 spawnPos;

    private GameManager gameManager;
    private float startDelay = 3;
    private float repeatRate = 3;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnObject", startDelay, repeatRate);
    }

    private void Update()
    {
        // Initialize a random spawn object and spawn position
        xPos = Random.Range(-xRange, xRange);
        zPos = Random.Range(6, 11);

        objectPrefab = objectPrefabs[Random.Range(0, 4)];
        spawnPos = new Vector3(xPos, 0, zPos);
    }

    void SpawnObject()
    {
        // If the game is not over, spawn objects
        if (gameManager.isGameActive == true)
        {
            Instantiate(objectPrefab, spawnPos, objectPrefab.transform.rotation);
        }
    }
}
