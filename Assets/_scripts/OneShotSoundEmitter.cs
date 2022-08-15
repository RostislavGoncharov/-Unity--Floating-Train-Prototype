using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSoundEmitter : SoundEmitter
{
    public override void PlaySound(int clipIndex, float volume, bool fadeIn, float fadeTimeInSeconds, AnimationCurve fadeCurve)
    {
        AudioManager.Instance.PlayOneShot(this, clips[clipIndex], volume, fadeIn, fadeTimeInSeconds, fadeCurve);
    }
}
