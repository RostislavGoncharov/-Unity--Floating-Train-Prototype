using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundEmitter : MonoBehaviour
{
    [SerializeField]
    protected List<AudioClip> clips = new List<AudioClip>();

    public abstract void PlaySound(int clipIndex, float volume, bool fadeIn, float fadeTimeInSeconds, AnimationCurve fadeCurve);

    public virtual void StopSound()
    {
        TryGetComponent(out AudioSource audioSource);
        audioSource.Stop();
    }
}
