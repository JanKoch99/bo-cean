using UnityEngine;
using System.Collections;

public class WaveRipple : MonoBehaviour
{
    public float expandSpeed = 8f;
    public float maxScale = 8f;
    public float lifeTime = 4f;

    [Header("Push Settings")]
    public float pushStrength = 1.2f;
    public float upwardLift = 0f;

    private bool hasHitBoat = false;
    private Vector3 impactPoint;

    public void SetImpactPoint(Vector3 point)
    {
        impactPoint = point;
    }

    void Start()
    {
        StartCoroutine(LifetimeRoutine());
    }

    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        if (transform.localScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHitBoat)
        {
            return;
        }

        if (!other.CompareTag("Boat"))
        {
            return;
        }

        hasHitBoat = true;

        Vector3 boatPos = other.transform.position;
        Vector3 pushDir = boatPos - impactPoint;
        pushDir.y = 0f;

        if (pushDir.sqrMagnitude < 0.001f)
        {
            pushDir = Vector3.back;
        }
        else
        {
            pushDir.Normalize();
        }

        Vector3 finalPush = pushDir * pushStrength;
        finalPush.y = upwardLift;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            rb.MovePosition(other.transform.position + finalPush);
        }
        else
        {
            other.transform.position += finalPush;
        }
    }
}