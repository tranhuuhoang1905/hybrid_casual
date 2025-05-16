using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip shootSound;
    // private AudioSource audioSource;

    void Awake()
    {
        // audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlayShootSound()
    {
        if (shootSound != null)
        {
            
            AudioManager.Instance.PlaySFX(shootSound);
            // AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, 0.3f);
        }
    }

    public void PlayHitSound()
    {
        if (hitSound != null)
        {
            AudioManager.Instance.PlaySFX(hitSound);
            // AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, 0.3f);

        }
    }
}
