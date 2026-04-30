using UnityEngine;
using System.Collections;

public class WaterManager : MonoBehaviour
{
    public SoundManager soundManager;
    
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
            StopCoroutine(waterSpeedRoutine);
        }

        waterSpeedRoutine = StartCoroutine(WaterSpeedPulse(amount));
    }

    private IEnumerator WaterSpeedPulse(float amount)
    {
        float originalSpeed = waterSpeed;
        soundManager.PlayWind();
        float boostedSpeed = Mathf.Clamp(baseWaterSpeed + amount, 0f, 5f);

        waterSpeed = boostedSpeed;

        yield return new WaitForSeconds(1f);

        float time = 0f;
        float duration = 0.5f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            waterSpeed = Mathf.Lerp(boostedSpeed, originalSpeed, t);
            yield return null;
        }

        waterSpeed = originalSpeed;
        soundManager.StopWind();
        waterSpeedRoutine = null;
    }

    private IEnumerator WaterPulse(float amount)
    {
        soundManager.PlayRain();
        float startHeight = waterHeight;
        float targetHeight = baseHeight + amount;

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

        yield return new WaitForSeconds(1f);

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
        soundManager.StopRain();
        
        waterHeightRoutine = null;
        
    }
}