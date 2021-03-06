﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (AudioSource))]
public class AudioFFT : MonoBehaviour
{
    AudioSource _audioSource;
    public float[] _samples;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _samples = new float[512];
    }

    // Update is called once per frame
    void Update()
    {
        
        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Hamming);
    }
}
