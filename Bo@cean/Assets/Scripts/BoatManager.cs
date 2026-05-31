using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (WaterManager.Instance != null)
        {
            WaterManager.Instance.StartDie();
        }
        Invoke("Respawn", 2f);
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
