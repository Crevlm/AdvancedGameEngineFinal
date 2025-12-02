using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 20f; // how far from the player the enemies will spawn in
    public float spawnInterval = 3f; // how often the enemies will spawn

    private Transform player;
    private float spawnTimer = 0f; // counts time until next spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime; 


        //Check if its time to spawn
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; //reset the timer
        }
    }

    void SpawnEnemy()
    {
        //pick a random direction around the player
        Vector2 circle = Random.insideUnitCircle.normalized;
        //convert that vector2 point into a 3D direction
        Vector3 direction = new Vector3(circle.x, 0f, circle.y);
        //Pick a spawn point at the desired distance
        Vector3 spawnPos = player.position + direction * spawnRadius;

        //Raycast to make sure the enemy stays on the ground
        //RaycastHit hit;
        //if (Physics.Raycast(spawnPos + Vector3.up * 10f, Vector3.down, out hit, 50f))
        //{
        //    spawnPos = hit.point; //snap to the floor
        //    spawnPos.y += 1f; 
        //}

        float terrainY = Terrain.activeTerrain.SampleHeight(spawnPos);
        spawnPos.y = terrainY + 1f; 

        //Spawn the enemy at this point
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
