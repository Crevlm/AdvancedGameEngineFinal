using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 20f;

    //Hoard Settings
    [SerializeField]
    private float separationRadius = 1f;
    private Vector3 velocity;
    public float acceleration = 1f;
    public float maxChaseSpeed = 1.5f;

    private Enemy[] cachedEnemies;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //basically find all the enemies at once and save them as cachedEnemies
        cachedEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy Y = " + transform.position.y);

        Vector3 separationPush = Vector3.zero;
        int closeCount = 0;


   

        foreach (Enemy e in cachedEnemies)
        {
            //ignore self 
            if (e == this || e == null) continue;
           
            float dist = Vector3.Distance(transform.position, e.transform.position);
            //Debug.Log("Dist to " + e.name + " = " + dist);

            if (dist < separationRadius)
            {
                closeCount++;

                Vector3 away = (transform.position - e.transform.position).normalized;

                //Force the enemies to stay on the ground
                away.y = 0f;

                separationPush += away;

            }

        }

        //only move if enemy is too close to another enemy
        
        if (closeCount > 0)
        {
            Vector3 sepDir = separationPush.normalized;

            //Make sure that Y stays locked during pushes and movement
            sepDir.y = 0f;

            transform.position += sepDir * moveSpeed * Time.deltaTime;
        }

        //Hoards toward the player 
        Vector3 chaseDir = (player.position - transform.position).normalized;
        chaseDir.y = 0; //Keep enemies stuck to the ground
        velocity += chaseDir * acceleration * Time.deltaTime;  //Accelerate toward the player
        velocity = Vector3.ClampMagnitude(velocity, maxChaseSpeed); //Lock the max speed
        velocity.y = 0f; // removes any sort of vertical drift
     
        transform.position += velocity * Time.deltaTime; //actually moves the enemy

        float terrainY = Terrain.activeTerrain.SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, terrainY, transform.position.z);
    }
}
