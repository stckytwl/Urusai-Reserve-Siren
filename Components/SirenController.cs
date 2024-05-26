using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stckytwl.UrusaiRen;

public class SirenController : MonoBehaviour
{
    private void Start()
    {
        var parentGameObject = transform;
        var sirenChildTransform = parentGameObject.GetChild(0);
        var sirenAudioSource = sirenChildTransform.GetComponent<AudioSource>();
        StartCoroutine(Work(sirenAudioSource));
        Invoke(nameof(Kill), 0.5f);
    }

    private static IEnumerator Work(AudioSource sirenAudioSource)
    {
        sirenAudioSource.Stop();
        yield return null;
        sirenAudioSource.volume = Plugin.PluginVolume.Value / 100f; // it dont work
        sirenAudioSource.PlayOneShot(sirenAudioSource.clip);
    }

    private void Kill()
    {
        Destroy(this);
    }
}