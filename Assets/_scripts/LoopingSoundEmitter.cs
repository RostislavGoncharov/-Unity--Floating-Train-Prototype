using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingSoundEmitter : SoundEmitter
{
    public override void PlaySound(int clipIndex, float volume, bool fadeIn, float fadeTimeInSeconds, AnimationCurve fadeCurve)
    {
        AudioManager.Instance.PlayLoop(this, clips[clipIndex], volume, fadeIn, fadeTimeInSeconds, fadeCurve);
    }
}
