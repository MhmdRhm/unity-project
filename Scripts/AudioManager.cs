using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip backgroundClip;
    public AudioClip gameOverClip;
    public AudioClip gameCompleteClip;
    public AudioClip menuClip;
    public AudioClip powerupClip;
    public AudioClip creditsClip;
    public AudioClip attackClip;
    public AudioClip hitClip;
    private AudioSource audioSource;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void PlayMenuMusic()
    {
        audioSource.loop = true;
        audioSource.clip = menuClip;
        audioSource.Play();
    }

    public void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundClip;
        audioSource.Play();
    }

    public void PlayPowerupClip()
    {
        audioSource.PlayOneShot(powerupClip);
    }

    public void PlayGameOverClip()
    {
        audioSource.clip = null;
        audioSource.PlayOneShot(gameOverClip);
    }

    public void PlayGameCompleteClip()
    {
        audioSource.clip = null;
        audioSource.PlayOneShot(gameCompleteClip);
    }

    public void PlayCreditsClip()
    {
        audioSource.clip = creditsClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayAttackClip()
    {
        audioSource.PlayOneShot(attackClip);
    }

    public void PlayHitClip()
    {
        audioSource.PlayOneShot(hitClip);
    }
}
