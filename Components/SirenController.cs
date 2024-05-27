using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stckytwl.UrusaiRen;

public class SirenController : MonoBehaviour
{
    private float _clipLength;

    private void Start()
    {
        var sirenChildTransform = transform.GetChild(0);
        var sirenAudioSource = sirenChildTransform.GetComponent<AudioSource>();
        _clipLength = sirenAudioSource.clip.length;

        StartCoroutine(Work(sirenAudioSource));
        Invoke(nameof(Kill), _clipLength * ChangeReserveSirenVolumePatch.PlayAmount);
    }

    private IEnumerator Work(AudioSource sirenAudioSource)
    {
        yield return null;
        sirenAudioSource.Stop();
        yield return null;
        for (var i = 0; i < ChangeReserveSirenVolumePatch.PlayAmount; i++)
        {
            sirenAudioSource.PlayOneShot(sirenAudioSource.clip);
            yield return new WaitForSeconds(_clipLength);
        }
    }

    private void Kill()
    {
        Destroy(this);
    }
}