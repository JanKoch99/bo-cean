using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip rainSound;
    public AudioClip thunderSound;
    public AudioClip windSound;

    public void PlayRain()
    {
        if (audioSource != null && rainSound != null)
        {
            audioSource.clip = rainSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopRain()
    {
        if (audioSource != null && audioSource.clip == rainSound)
        {
            audioSource.Stop();
        }
    }

    public void PlayThunder()
    {
        if (audioSource != null && thunderSound != null)
        {
            audioSource.PlayOneShot(thunderSound);
        }
    }

    public void PlayWind()
    {
        if (audioSource != null && windSound != null)
        {
            audioSource.clip = windSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopWind()
    {
        if (audioSource != null && audioSource.clip == windSound)
        {
            audioSource.Stop();
        }
    }
}
