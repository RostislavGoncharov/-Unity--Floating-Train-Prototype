using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPulse : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    float soundVolumeModRate;
    [SerializeField]
    float lightIntensityModRate;
    [SerializeField]
    float maxVolume;
    [SerializeField]
    Light pointLight;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.Log("Tower audio source is null");
        }
    }

    private void Update()
    {
        if (audioSource.volume < maxVolume)
        {
            audioSource.volume += soundVolumeModRate * Time.deltaTime;
            pointLight.intensity += lightIntensityModRate * Time.deltaTime;
        }

        else
        {
            audioSource.volume = 0;
            pointLight.intensity = 0;
        }
    }
}
