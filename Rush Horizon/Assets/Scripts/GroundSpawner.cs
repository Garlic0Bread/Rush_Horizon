using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;  // Reference to your ground prefab
    public GameObject obstaclePrefab; // Reference to your obstacle prefab
    public Transform player;         // Reference to your player object
    public int numberOfGrounds = 5;  // Number of ground segments to spawn initially
    private Queue<GameObject> activeGrounds = new Queue<GameObject>();
    private float spawnZ = 0f;       // Position at which the next ground will spawn
    private float groundLength = 50f; // Length of each ground segment along Z-axis
    private float safeZone = 60f;    // How far the player has to move before new ground is spawned

    void Start()
    {
        // Spawn initial grounds at the start
        for (int i = 0; i < numberOfGrounds; i++)
            SpawnGround(i == 0); // The first ground might not have obstacles
    }

    void Update()
    {
        // Check if the player has moved far enough to spawn a new ground segment
        if (player.position.z > spawnZ - (numberOfGrounds * groundLength))
        {
            SpawnGround(false); // Spawn ground with obstacles after the initial one
            RemoveOldGround();  // Remove the oldest ground to save memory
        }
    }

    // Method to spawn a new ground segment
    void SpawnGround(bool isStartingGround)
    {
        // Instantiate a new ground segment at the spawnZ position
        GameObject newGround = Instantiate(groundPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);

        if (!isStartingGround)
            AddObstacles(newGround); // Add obstacles if it's not the starting ground

        // Enqueue the newly spawned ground to the activeGrounds queue
        activeGrounds.Enqueue(newGround);

        // Move the spawn position forward by the length of the ground
        spawnZ += groundLength;
    }

    // Method to remove the oldest ground segment to free up memory
    void RemoveOldGround()
    {
        // Dequeue the oldest ground and destroy it
        Destroy(activeGrounds.Dequeue());
    }

    // Method to add obstacles to the ground
    void AddObstacles(GameObject ground)
    {
        int obstacleCount = Random.Range(1, 4); // Add 1 to 3 obstacles per ground segment

        for (int i = 0; i < obstacleCount; i++)
        {
            // Randomly place obstacles on the ground
            Vector3 obstaclePosition = new Vector3(
                Random.Range(-8.5f, 8.5f), // X position, randomly within lane limits
                0.5f, // Y position (height)
                ground.transform.position.z + Random.Range(-20, 20) // Random Z position relative to the ground
            );

            // Instantiate obstacle prefab (you'll need to set your own obstacle prefab here)
            GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
            obstacle.transform.parent = ground.transform; // Attach to the ground for easy cleanup
        }
    }
}
