using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth = 3;
    public TextMeshProUGUI healthText;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        
        if (playerHealth >= 1)
        {
            playerHealth -= amount;
            

        }
        healthText.text = "Health: " + playerHealth;
        if (playerHealth <=0)
        {
            Debug.Log("You died");
        }

    }
}