using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaterManager : MonoBehaviour
{
    public SoundManager soundManager;
    
    public static WaterManager Instance;

    [Header("Abilities Settings")]
    public float rainCooldown = 5f;
    public float windCooldown = 5f;
    public ParticleSystem rainParticles;

    [Header("UI Elements")]
    public Image rainCooldownImage;
    public Image windCooldownImage;

    private float rainTimer = 0f;
    private float windTimer = 0f;

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

    void Update()
    {
        UpdateCooldowns();
        HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            IncreaseWaterHeightButton();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (LightningClickSpawner.Instance != null)
            {
                LightningClickSpawner.Instance.EnableLightning();
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            IncreaseWaterSpeedButton();
        }
    }

    private void UpdateCooldowns()
    {
        if (rainTimer > 0)
        {
            rainTimer -= Time.deltaTime;
            if (rainCooldownImage != null)
                rainCooldownImage.fillAmount = rainTimer / rainCooldown;
        }

        if (windTimer > 0)
        {
            windTimer -= Time.deltaTime;
            if (windCooldownImage != null)
                windCooldownImage.fillAmount = windTimer / windCooldown;
        }
    }

    public void IncreaseWaterHeightButton()
    {
        if (rainTimer <= 0)
        {
            IncreaseWaterHeight(3f);
            rainTimer = rainCooldown;
        }
    }

    public void IncreaseWaterSpeedButton()
    {
        if (windTimer <= 0)
        {
            IncreaseWaterSpeed(3f);
            windTimer = windCooldown;
        }
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
        if (rainParticles != null) rainParticles.Play();
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
        if (rainParticles != null) rainParticles.Stop();
        
        waterHeightRoutine = null;
        
    }
}