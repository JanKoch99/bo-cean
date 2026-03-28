using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter(Collider collider)
    {
        BoatManager boatManager = collider.GetComponent<BoatManager>();
        if (boatManager != null)
        {
            boatManager.TakeDamage(damage);
        }
    }
}
