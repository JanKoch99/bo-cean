using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Boat took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boat has died!");
        
        if (WaterManager.Instance != null)
        {
            WaterManager.Instance.waterSpeed = 0f;
        }
        
        // TODO: Restart level?
    }
}
