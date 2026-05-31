using UnityEngine;

public class FinishPoint : FloatingObject
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Boat"))
        {
            SceneController.instance.NextLevel();
        }
    }
    
}
