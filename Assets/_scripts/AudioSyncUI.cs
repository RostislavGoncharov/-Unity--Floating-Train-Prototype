using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncUI : AudioSyncer
{
    public Vector3 peakScale;
    public Vector3 restScale;

    public override void OnPeak()
    {
        base.OnPeak();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", peakScale);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isPeak)
            return;

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
    }

    IEnumerator MoveToScale(Vector3 target)
    {
        Vector3 _curr = transform.localScale;
        Vector3 _initial = _curr;
        float _timer = 0;

        while (_curr != target)
        {
            _curr = Vector3.Lerp(_initial, target, _timer / timeToPeak);
            _timer += Time.deltaTime;

            transform.localScale = _curr;

            yield return null;
        }

        m_isPeak = false;
    }
}
