using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarriageUnlockTween : MonoBehaviour
{
    [SerializeField] Transform _startTransform;
    Vector3 _startPos, _startRot, _startScale;
    private void Awake()
    {
        _startPos = transform.localPosition;
        _startRot = transform.localRotation.eulerAngles;
        _startScale = transform.localScale;
    }
    private void Start() => Tween();
    private void Tween()
    {
        transform.localPosition = _startTransform.localPosition;
        transform.localRotation = _startTransform.localRotation;
        transform.localScale = _startTransform.localScale;

        Sequence seq = DOTween.Sequence();

        Vector3[] path = new Vector3[] {_startTransform.localPosition,
                                        (_startTransform.localPosition + _startPos ) / 2f ,
                                        _startPos
                                        };

        seq.Append(transform.DOLocalPath(path, .5f));
        seq.Insert(0f, transform.DORotate(_startRot, .5f));
        seq.Insert(0f, transform.DOScale(_startScale, .5f));
        seq.Append(transform.DOScale(_startScale * 1.3f, 0.15f));
        seq.Append(transform.DOScale(_startScale, 0.35f));
    }
}
