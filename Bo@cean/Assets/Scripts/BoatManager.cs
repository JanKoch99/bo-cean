using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public int maxHealth = 3;
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
        Debug.Log("Boat took damage! Current health: " + currentHealth);

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
        transform.position = startPos;
        currentHealth = maxHealth;

        WaterManager.Instance.Reset();
    }
}
