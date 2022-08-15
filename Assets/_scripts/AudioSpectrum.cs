using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }

    float[] m_audioSpectrum;

    AudioSource audioSource;
    
    void Start()
    {
        m_audioSpectrum = new float[128];
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }
}
