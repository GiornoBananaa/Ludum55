using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource _audioSrc;
    public float _volume;

    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        _audioSrc.volume = _volume;
    }

    public void SetVolume(float vol)
    {
        _volume = vol;
    }
}
