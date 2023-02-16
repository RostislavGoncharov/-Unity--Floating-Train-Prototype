/*
 * This class controls the Y-scale of a UI segment used for audio visualization.
 * The band variable selects a frequency band (0 - 7 in the current version) to listen to. 
 * Use this version of the script if you're using the AudioSpectrumAudioListener script in the scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationElementAudioListener : MonoBehaviour
{
    [SerializeField] int band;

    float defaultScaleY;

    private void Start()
    {
        // Making sure that the element doesn't shrink below initial Y-scale if the band is silent/quiet
        defaultScaleY = transform.localScale.y;

        // Making sure the band variable isn't out of bounds
        if (band > AudioSpectrum.bandBuffers.Length - 1)
        {
            band = AudioSpectrum.bandBuffers.Length - 1;
        }

        if (band < 0)
        {
            band = 0;
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, AudioSpectrumAudioListener.bandBuffers[band], transform.localScale.z);

        if (transform.localScale.y < defaultScaleY)
        {
            transform.localScale = new Vector3(transform.localScale.x, defaultScaleY, transform.localScale.z);
        }
    }
}
