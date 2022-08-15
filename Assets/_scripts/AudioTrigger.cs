using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AudioTrigger : MonoBehaviour
{
    [SerializeField]
    List<SoundEmitter> soundEmitters = new List<SoundEmitter>();
    [SerializeField]
    int clipIndex;
    [SerializeField]
    float volume = 1.0f;
    [SerializeField]
    bool fadeIn = false;
    [SerializeField]
    float fadeTimeInSeconds = 1.0f;
    [SerializeField]
    private AnimationCurve fadeCurve;

    private void OnTriggerEnter(Collider other)
    {
        foreach (SoundEmitter soundEmitter in soundEmitters)
        {
            soundEmitter.PlaySound(clipIndex, volume, fadeIn, fadeTimeInSeconds, fadeCurve);
        }
    }
}
