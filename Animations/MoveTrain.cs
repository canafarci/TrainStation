using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MoveTrain : MonoBehaviour
{
    [SerializeField] GameObject[] _objectsToSwitch;
    [SerializeField] Transform _endTransform;
    [SerializeField] float _goForwardDuration, _returnDuration, _takeOffDelay;
    [SerializeField] AnimationCurve _takeOffEase, _returnEase;
    TrainState _state;
    TrainMoveFX _fx;
    Vector3 _startPos;
    Sequence _moveSequence;
    private void OnEnable() => _state.TrainTakeoffHandler += OnTrainFilled;
    private void OnDisable() => _state.TrainTakeoffHandler -= OnTrainFilled;
    private void Awake()
    {
        _startPos = transform.position;
        _fx = GetComponent<TrainMoveFX>();
        _state = GetComponent<TrainState>();
    }
    void OnTrainFilled() => TakeOff();
    public void MoveToNewStartPos(Vector3 newPos, Action callback)
    {
        _fx.MoveToNewStartPosFX();

        _state.TrainIsReady = false;

        CheckPreviousSequence();
        _moveSequence.PrependInterval(0.15f);
        _moveSequence.Append(transform.DOMove(newPos, .85f));

        _moveSequence.onComplete = () =>
        {
            _state.TrainIsReady = true;
            _startPos = transform.position;
            callback();
        };
    }

    public void TakeOff()
    {
        _fx.TakeOffFX();

        _objectsToSwitch.ToList().ForEach(x => x.SetActive(false));
        _state.TrainIsReady = false;

        CheckPreviousSequence();
        _moveSequence.AppendInterval(_takeOffDelay);
        _moveSequence.Append(transform.DOMove(_endTransform.position, _goForwardDuration).SetEase(_takeOffEase));

        _moveSequence.onComplete = () =>
        {
            Return();
        };
    }

    void Return()
    {
        _fx.ReturnFX();

        CheckPreviousSequence();
        _moveSequence.Append(transform.DOMove(_startPos, _returnDuration).SetEase(_returnEase));
        _moveSequence.onComplete = () =>
        {
            _state.OnTrainReturn();
            _objectsToSwitch.ToList().ForEach(x => x.SetActive(true));
            _state.TrainIsReady = true;

            foreach (ISpawner spawner in GetComponentsInChildren<ISpawner>())
                StartCoroutine(spawner.SpawnLoop());
        };
    }

    public void FirstReturn()
    {
        _fx.ReturnFX();

        transform.position = _endTransform.position;
        _objectsToSwitch.ToList().ForEach(x => x.SetActive(false));

        CheckPreviousSequence();
        _moveSequence.Append(transform.DOMove(_startPos, _returnDuration).SetEase(_returnEase));
        _moveSequence.onComplete = () =>
        {
            _objectsToSwitch.ToList().ForEach(x => x.SetActive(true));
            _state.TrainIsReady = true;
        };
    }

    void CheckPreviousSequence()
    {
        if (_moveSequence != null)
            _moveSequence.Kill();

        _moveSequence = DOTween.Sequence();
    }
}
