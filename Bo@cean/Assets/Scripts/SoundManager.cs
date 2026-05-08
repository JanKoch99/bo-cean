using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip rainSound;
    public AudioClip thunderSound;
    public AudioClip windSound;

    public void PlayRain()
    {
        audioSource.clip = rainSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopRain()
    {
        audioSource.Stop();
    }

    public void PlayThunder()
    {
        audioSource.PlayOneShot(thunderSound);
    }

    public void PlayWind()
    {
        audioSource.clip = windSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopWind()
    {
        audioSource.Stop();
    }
}