using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainState : MonoBehaviour
{
    public bool TrainIsReady = true;
    public bool TrainIsBroken = false;
    public bool TrainIsFull { get { return _totalInsidePassengers == MaxPassengers; } }
    public int MaxPassengers = 4;
    [SerializeField] GameObject[] _brokenUIObjects;
    [SerializeField] MoneyStacker _stacker;
    int _totalInsidePassengers;
    public event Action TrainTakeoffHandler, TrainReturnHandler;

    //!FOR VIDEO
    [SerializeField] int _brokenPerRun;
    int _brokenCounter = 0;

    private void Awake()
    {
        FindObjectOfType<LevelProgress>().AddStateToList(this);
    }

    public void IncreaseInsidePassengerCount()
    {
        _totalInsidePassengers++;
        CheckIfReadyToTakeOff();
    }
    public void OnTrainReturn()
    {
        _brokenCounter++;
        TrainReturnHandler?.Invoke();

        if (_brokenCounter == _brokenPerRun)
        {
            _brokenUIObjects.ToList().ForEach(x => x.SetActive(true));
            TrainIsBroken = true;
            _brokenCounter = 0;
        }
    }
    public void OnTrainRepaired()
    {
        _brokenUIObjects.ToList().ForEach(x => x.SetActive(false));
        TrainIsBroken = false;
        TrainIsReady = true;
        CheckIfReadyToTakeOff();
    }
    void CheckIfReadyToTakeOff()
    {
        if (_totalInsidePassengers != MaxPassengers || TrainIsBroken) { return; }

        for (int i = 0; i < _totalInsidePassengers / 4; i++)
        {
            TrainTakeoffHandler?.Invoke();
            _stacker.StackMoney(GameManager.Instance.References.GameConfig.StackPerTrainCarriage);
        }
        _totalInsidePassengers = 0;
    }
}
