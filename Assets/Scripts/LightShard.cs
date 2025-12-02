using UnityEngine;
using TMPro;

public class LightShard : MonoBehaviour
{
    private static int lightShards = 0;
    public static TextMeshProUGUI lightShardText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (lightShardText == null)
        {
            lightShardText = GameObject.Find("LightShardText").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player picked up a light shard!");
            lightShards++;
            lightShardText.text = "Light Shards: " + lightShards + " of 5";
            LightSystem lightSystem = other.GetComponent<LightSystem>();
            if (lightSystem != null)
            {
                lightSystem.AddLight();
            }

            Destroy(gameObject);
        }
    }
}
