using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    public float threshold;
    public float timeStep;
    public float timeToPeak;
    public float restSmoothTime;

    float m_previousAudioValue;
    float m_audioValue;
    float m_timer;

    protected bool m_isPeak;

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        m_previousAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.spectrumValue;

        if (m_previousAudioValue > threshold && m_audioValue <= threshold)
        {
            if (m_timer > timeStep)
            {
                OnPeak();
            }
        }

        if (m_previousAudioValue <= threshold && m_audioValue > threshold)
        {
            if (m_timer > timeStep)
            {
                OnPeak();
            }
        }

        m_timer += Time.deltaTime;
    }

    public virtual void OnPeak()
    {
        Debug.Log("Peak Detected");
        m_timer = 0;
        m_isPeak = true;
    }
}
