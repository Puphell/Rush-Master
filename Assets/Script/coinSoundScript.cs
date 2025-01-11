using UnityEngine;

public class coinSoundScript : MonoBehaviour
{
    public AudioClip coinCollectSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (coinCollectSound != null)
            {
                audioSource.PlayOneShot(coinCollectSound);
            }
        }
    }
}