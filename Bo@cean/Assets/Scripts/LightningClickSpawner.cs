using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LightningClickSpawner : MonoBehaviour
{
    public GameObject lightningPrefab;
    public GameObject wavePrefab;
    public float spawnHeight = 5f;
    public bool canStrike = false;
    public SoundManager soundManager;

    [Header("Cooldown Settings")]
    public float cooldownTime = 5f;
    public Image cooldownImage;
    private float cooldownTimer = 0f;

    public static LightningClickSpawner Instance;
    private bool initialCanStrike;

    void Awake()
    {
        Instance = this;
        initialCanStrike = canStrike;
    }

    public void ResetSpawner()
    {
        cooldownTimer = 0f;
        canStrike = initialCanStrike;
        if (cooldownImage != null)
            cooldownImage.fillAmount = 0f;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownImage != null)
                cooldownImage.fillAmount = cooldownTimer / cooldownTime;
        }

        if (!canStrike || !Input.GetMouseButtonDown(0) || cooldownTimer > 0)
        {
            return;
        }

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lightningSpawnPos = hit.point + Vector3.up * spawnHeight;
            Instantiate(lightningPrefab, lightningSpawnPos, Quaternion.identity);
            
            soundManager.PlayThunder();
            
            if (wavePrefab != null)
            {
                Vector3 waveSpawnPos = hit.point + Vector3.up * 0.02f;
                GameObject waveObj = Instantiate(wavePrefab, waveSpawnPos, Quaternion.Euler(90f, 0f, 0f));

                WaveRipple wave = waveObj.GetComponent<WaveRipple>();
                if (wave != null)
                {
                    wave.SetImpactPoint(hit.point);
                }
            }
            else
            {
                Debug.LogWarning("wavePrefab ist nicht gesetzt!");
            }

            canStrike = false;
            cooldownTimer = cooldownTime;
        }
    }

    public void EnableLightning()
    {
        canStrike = true;
    }
}