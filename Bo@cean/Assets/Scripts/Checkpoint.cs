using UnityEngine;

public class Checkpoint : FloatingObject
{
    [SerializeField] private GameObject checkpointText;

    private void Start()
    {
        checkpointText.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Boat"))
        {
            checkpointText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Trigger Exit");

        if (collision.CompareTag("Boat"))
        {
            checkpointText.SetActive(false);
        }
    }
}