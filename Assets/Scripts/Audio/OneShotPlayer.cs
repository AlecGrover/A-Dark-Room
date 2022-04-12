using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OneShotPlayer : MonoBehaviour
{

    private AudioSource _audioSource;
    [Range(0f, 1f)]
    public float Volume = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
       _audioSource = this.AddComponent<AudioSource>();
       _audioSource.volume = Volume;
       _audioSource.spatialBlend = 0f;
    }

    public void PlayOneShot(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

}
