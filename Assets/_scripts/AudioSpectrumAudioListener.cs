/*
 * This is the base class for audio visualization. 
 * It takes the sound incoming into the audio listener, splits it into frequency bands, and applies band buffers.
 * Band buffers provide smoothing for value changes, so that the visuals don't jump from higher to lower values immediately.
 * This class can be attached to any object in the scene. 
 * Use this version of the script if you want ALL sounds in the scene to affect audio visualization.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrumAudioListener : MonoBehaviour
{
    public static float[] samples = new float[512];
    public static float[] freqBands = new float[8];
    public static float[] bandBuffers = new float[8];

    float[] bufferDecrease = new float[8];

    [SerializeField] float defaultBufferDecreaseValue = 0.005f;
    [SerializeField] float bufferDecreaseMultiplier = 1.2f;

    void Update()
    {
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        GetFrequencyBands();
        UseBandBuffer();
    }

    // Formula to split all samples into a smaller number of frequency bands
    void GetFrequencyBands()
    {
        int count = 0;

        for (int i = 1; i <= freqBands.Length; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i);

            // 2 samples remain unassigned when using the formula, so we add them to the last band
            if (i == freqBands.Length)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqBands[i - 1] = average * 10;
        }
    }


    // Method to smooth out value changes
    void UseBandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (bandBuffers[i] < freqBands[i])
            {
                bandBuffers[i] = freqBands[i];
                bufferDecrease[i] = defaultBufferDecreaseValue;
            }

            if (bandBuffers[i] > freqBands[i])
            {
                bandBuffers[i] -= bufferDecrease[i];
                bufferDecrease[i] *= bufferDecreaseMultiplier;
            }
        }
    }
}
