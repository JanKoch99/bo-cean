using UnityEngine;

public class FinishPoint : FloatingObject
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger Enter");
        if (collision.CompareTag("Boat"))
        {
            SceneController.instance.NextLevel();
        }
    }
    
}
