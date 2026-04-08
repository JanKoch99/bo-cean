using UnityEngine;
using System.Collections;
public class WaterManager : MonoBehaviour
{
    public static WaterManager Instance;

    [Range(0f, 5f)] public float waterSpeed = 1f;
    private float baseWaterSpeed = 1f;
    public float waterAnimationSpeed
    {
        get { return waterSpeed * 0.02f; }
    }
    
    [Range(0f, 1f)] public float waterHeight = 0f;
    private float baseHeight = 0f;
    private Coroutine waterHeightRoutine;

    private Coroutine waterSpeedRoutine;

    public Vector3 flowDirection = Vector3.back;
    
    void Awake()
    {
        Instance = this;
        baseHeight = waterHeight;
        baseWaterSpeed = waterSpeed;
    }

    public void IncreaseWaterHeightButton()
    {
        IncreaseWaterHeight(3f);
    }

    public void IncreaseWaterSpeedButton()
    {
        IncreaseWaterSpeed(3f);
    }
    
    public void IncreaseWaterHeight(float amount)
    {
        if (waterHeightRoutine != null)
        {
            return;
        }

        waterHeightRoutine = StartCoroutine(WaterPulse(amount));
    }

    public void IncreaseWaterSpeed(float amount)
    {
        if (waterSpeedRoutine != null)
        {
            return;
        }
        
        waterSpeedRoutine = StartCoroutine(WaterSpeedPulse(amount));
    }

    private IEnumerator WaterPulse(float amount)
    {
        float startHeight = waterHeight;
        float targetHeight = baseHeight + amount;
        
        // Water should rise in 0.5s
        float time = 0f;
        float duration = 0.5f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            waterHeight = Mathf.Lerp(startHeight, targetHeight, t);
            yield return null;
        }
        
        waterHeight = targetHeight;
        
        // Wait for 1s before resetting
        yield return new WaitForSeconds(1f);
        
        // Water should fall back to base height in 0.5s
        time = 0f;
        duration = 0.5f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            waterHeight = Mathf.Lerp(targetHeight, baseHeight, t);
            yield return null;
        }

        waterHeight = baseHeight;

        waterHeightRoutine = null;
    }

    private IEnumerator WaterSpeedPulse(float amount)
    {
        float startSpeed = waterSpeed;
        float peakSpeed = baseWaterSpeed + amount;

        // Speed up
        yield return LerpSpeed(startSpeed, peakSpeed, 0.5f);

        // hold
        yield return new WaitForSeconds(1f);

        // slow down
        yield return LerpSpeed(peakSpeed, startSpeed, 0.5f);

        waterSpeedRoutine = null;
    }

    private IEnumerator LerpSpeed(float from, float to, float duration)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            waterSpeed = Mathf.Lerp(from, to, t);
            yield return null;
        }

    }
}
