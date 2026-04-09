using UnityEngine;
using System.Collections;

public class BoatWaveReceiver : MonoBehaviour
{
    public float shoveDistance = 1.2f;
    public float shoveDuration = 0.35f;

    private bool isShoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isShoving)
        {
            return;
        }

        if (!other.CompareTag("Wave"))
        {
            return;
        }

        StartCoroutine(ShoveBoatRoutine(other.transform.position));
    }

    private IEnumerator ShoveBoatRoutine(Vector3 wavePosition)
    {
        isShoving = true;

        Vector3 startPos = transform.position;

        Vector3 pushDir = transform.position - wavePosition;
        pushDir.y = 0f;

        if (pushDir.sqrMagnitude < 0.001f)
        {
            pushDir = Vector3.back;
        }
        else
        {
            pushDir.Normalize();
        }

        Vector3 targetPos = startPos + pushDir * shoveDistance;

        float time = 0f;
        while (time < shoveDuration)
        {
            time += Time.deltaTime;
            float t = time / shoveDuration;

            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        isShoving = false;
    }
}