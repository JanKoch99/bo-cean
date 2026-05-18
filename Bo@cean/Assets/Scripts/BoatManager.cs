using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public int maxHealth = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private int currentHealth;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        // Debug.Log("Boat took damage! Current health: " + currentHealth);
        if (currentHealth >= 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if (currentHealth == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);        
        }
        else if (currentHealth == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else if (currentHealth <= 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Boat has died!");
        
        // if (WaterManager.Instance != null)
        // {
        //     WaterManager.Instance.waterSpeed = 0f;
        // }

        Respawn();
        // TODO: Restart level?
    }

    private void Respawn()
    {
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        transform.position = startPos;
        currentHealth = maxHealth;

        WaterManager.Instance.Reset();
    }
}
