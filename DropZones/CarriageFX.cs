using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarriageFX : MonoBehaviour
{
    [SerializeField] GameObject _passengerInside;
    Vector3 _baseScale;
    bool _playedAnimation;
    private void Awake()
    {
        _baseScale = _passengerInside.transform.localScale;
        _passengerInside.SetActive(false);
    }
    public void Reset()
    {
        _passengerInside.SetActive(false);
        _playedAnimation = false;
    }
    public void CheckAndPlayFX()
    {
        if (_playedAnimation) { return; }
        PlayFX();
        _playedAnimation = true;
    }
    public void PlayFX()
    {
        _passengerInside.SetActive(true);
        Sequence seq = DOTween.Sequence();

        _passengerInside.transform.localScale = Vector3.one * 0.0001f;
        _passengerInside.transform.DOScale(_baseScale, 0.75f);
    }
}
