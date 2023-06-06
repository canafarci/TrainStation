using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PassengerDropZone : DropZone
{
    [SerializeField] Transform _targetTransform;
    [SerializeField] GameObject _dropObject;
    [SerializeField] TextMeshProUGUI _text;
    TrainState _state;
    QueueSize _queueSize;
    CarriageFX _fx;
    int _maxPassengers;
    int _passengersInside, _passengerInsideText = 0;
    List<Follower> _getInList = new List<Follower>();

    private void Awake()
    {
        _state = GetComponentInParent<TrainState>();
        _queueSize = FindObjectOfType<QueueSize>();
        _fx = GetComponent<CarriageFX>();
    }
    private void OnEnable()
    {
        _state.TrainReturnHandler += OnTrainReturn;
    }
    private void OnDisable()
    {
        _state.TrainReturnHandler -= OnTrainReturn;
    }
    private void Start()
    {
        _maxPassengers = ConstantValues.PASSENGER_PER_CARRIAGE;
        _text.text = _passengersInside.ToString() + "/" + _maxPassengers.ToString();
    }
    protected override void Drop(NavMeshNPC[] npcs)
    {
        if (!_state.TrainIsReady) { return; }
        StartCoroutine(GetInLoop(npcs));
    }
    IEnumerator GetInLoop(NavMeshNPC[] npcs)
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            if (_passengersInside > 3) { break; }
            _passengersInside++;
            Follower follower = npcs[i].GetComponent<Follower>();
            GameManager.Instance.References.PlayerInventory.RemoveFollower(follower);
            follower.StopFollow();
            follower.transform.DOLookAt(_targetTransform.position, 0.5f);
            _getInList.Add(follower);
        }

        foreach (Follower follower in _getInList)
            yield return StartCoroutine(follower.OpenDoorAndGetInRoutine(_targetTransform, () =>
            {
                UpdateCarriage();
                _fx.CheckAndPlayFX();
            }));

        _getInList.Clear();
    }

    void UpdateCarriage()
    {
        _passengerInsideText++;
        _text.text = _passengerInsideText.ToString() + "/" + _maxPassengers.ToString();
        _state.IncreaseInsidePassengerCount();
        _queueSize.OnDepart();

        if (_passengerInsideText == 4)
        {
            _text.DOColor(Color.green, 0.1f);
            _dropObject.SetActive(false);
        }
    }

    void OnTrainReturn()
    {
        _text.DOColor(Color.white, 0.001f);
        _text.text = "0/4";
        _passengersInside = 0;
        _passengerInsideText = 0;
        _fx.Reset();
    }
}