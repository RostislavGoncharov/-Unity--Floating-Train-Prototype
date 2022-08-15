using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayOneShot(SoundEmitter obj, AudioClip clip, float volume, bool fadeIn, float fadeTimeInSeconds, AnimationCurve fadeCurve)
    {
        if (obj.TryGetComponent(out AudioSource audioSource)) {
            audioSource.loop = false;
            StartCoroutine(FadeInOneShot(obj, clip, fadeIn, fadeTimeInSeconds, volume, fadeCurve));
        }
        else
        {
            AudioSource newAudioSource = obj.gameObject.AddComponent<AudioSource>() as AudioSource;
            newAudioSource.loop = false;
            StartCoroutine(FadeInOneShot(obj, clip, fadeIn, fadeTimeInSeconds, volume, fadeCurve));
        }
    }

    public void PlayLoop(SoundEmitter obj, AudioClip clip, float volume, bool fadeIn, float fadeTimeInSeconds, AnimationCurve fadeCurve)
    {
        if (obj.TryGetComponent(out AudioSource audioSource)) {
            audioSource.loop = true;
            audioSource.clip = clip;
            StartCoroutine(FadeIn(obj, fadeIn, fadeTimeInSeconds, volume, fadeCurve));
        }
        else
        {
            AudioSource newAudioSource = obj.gameObject.AddComponent<AudioSource>() as AudioSource;
            newAudioSource.loop = true;
            newAudioSource.clip = clip;
            StartCoroutine(FadeIn(obj, fadeIn, fadeTimeInSeconds, volume, fadeCurve));
        }
    }

    private IEnumerator FadeIn(SoundEmitter obj, bool fadeIn, float fadeTime, float targetVolume, AnimationCurve fadeCurve)
    {
        if (obj.TryGetComponent(out AudioSource audioSource))
        {
            if (fadeIn)
            {
                audioSource.volume = 0;
                float timeElapsed = 0;
                audioSource.Play();

                while (audioSource.volume < targetVolume)
                {
                    timeElapsed += Time.deltaTime;
                    float time = timeElapsed / fadeTime;
                    audioSource.volume = fadeCurve.Evaluate(time) * targetVolume;
                    
                    //audioSource.volume += fadeTime * Time.deltaTime;

                    //if (audioSource.volume > targetVolume)
                    //{
                    //    audioSource.volume = targetVolume;
                    //}

                    yield return null;
                }
            }
            
            else
            {
                audioSource.volume = targetVolume;
                audioSource.Play();
            }
        }
    }

    private IEnumerator FadeInOneShot(SoundEmitter obj, AudioClip clip, bool fadeIn, float fadeTime, float targetVolume, AnimationCurve fadeCurve)
    {
        if (obj.TryGetComponent(out AudioSource audioSource))
        {
            if (fadeIn)
            {
                audioSource.volume = 0;
                audioSource.PlayOneShot(clip);
                float timeElapsed = 0;

                while (audioSource.volume < targetVolume)
                {
                    timeElapsed += Time.deltaTime;
                    float time = timeElapsed / fadeTime;
                    audioSource.volume = fadeCurve.Evaluate(time) * targetVolume;
                    //audioSource.volume += fadeTime * Time.deltaTime;

                    //if (audioSource.volume > targetVolume)
                    //{
                    //    audioSource.volume = targetVolume;
                    //}

                    yield return null;
                }
            }

            else
            {
                //audioSource.volume = targetVolume;
                audioSource.PlayOneShot(clip, targetVolume);
            }
        }
    }
}
