using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _FX;
    public void PlayFX()
    {
        foreach (ParticleSystem PS in _FX)
        {
            PS.Play();
        }
    }
}
