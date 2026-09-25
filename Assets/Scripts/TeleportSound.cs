using UnityEngine;

public class TeleportSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip whooshClip;

    public void PlayTeleportSound()
    {
        if (audioSource != null && whooshClip != null)
        {
            audioSource.PlayOneShot(whooshClip);
        }
    }
}