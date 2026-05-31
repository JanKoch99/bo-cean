using UnityEngine;
using System.Collections;

public class WaterPoisonManager : MonoBehaviour
{
    [Header("Damage")]
    public int damagePerSecond = 1;
    public float damageDelay = 2f;

    [Header("Message")]
    [TextArea]
    public string warningMessage = "Go back to better water!";
    public float warningMessageDuration = 2f;

    private Coroutine damageRoutine;
    private bool showWarning = false;
    private bool blinkState = true;
    private float blinkInterval = 0.25f;

    private GUIStyle warningStyle;
    private GUIStyle warningShadowStyle;

    private void OnTriggerEnter(Collider other)
    {
        BoatManager boat = other.GetComponentInParent<BoatManager>();
        if (boat == null)
        {
            return;
        }

        StartCoroutine(ShowWarningRoutine());

        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
        }

        damageRoutine = StartCoroutine(DamageBoatRoutine(boat));
    }

    private void OnTriggerExit(Collider other)
    {
        BoatManager boat = other.GetComponentInParent<BoatManager>();
        if (boat == null)
        {
            return;
        }

        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }

    private IEnumerator DamageBoatRoutine(BoatManager boat)
    {
        yield return new WaitForSeconds(damageDelay);

        while (boat != null)
        {
            boat.TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1f);
        }

        damageRoutine = null;
    }

    private IEnumerator ShowWarningRoutine()
    {
        showWarning = true;
        float elapsed = 0f;
        while (elapsed < warningMessageDuration)
        {
            blinkState = !blinkState;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        showWarning = false;
        blinkState = true; // Reset für nächstes Mal
    }

    private void OnGUI()
    {
        if (showWarning && blinkState)
        {
            if (warningStyle == null)
            {
                warningStyle = new GUIStyle();
                warningStyle.fontSize = 40;
                warningStyle.fontStyle = FontStyle.Bold;
                warningStyle.normal.textColor = Color.red;
                warningStyle.alignment = TextAnchor.MiddleCenter;

                warningShadowStyle = new GUIStyle(warningStyle);
                warningShadowStyle.normal.textColor = Color.black;
            }

            // Zeichne die Nachricht in der Mitte des Bildschirms
            float width = Screen.width;
            float height = 100f;
            float x = 0;
            float y = (Screen.height - height) * 0.5f;

            // Schatten für bessere Lesbarkeit
            GUI.Label(new Rect(x + 2, y + 2, width, height), warningMessage, warningShadowStyle);
            GUI.Label(new Rect(x, y, width, height), warningMessage, warningStyle);
        }
    }
}