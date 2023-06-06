using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for all delayed wait behaviours
//Keeps track of time spent inside the waiting area and calls suspends the functions while there is still time, 
public class WaitingLoop : MonoBehaviour, IWaitZone
{
    [SerializeField] protected float _maxTime;
    protected float _currentTime;
    protected float _speedModifier = 1f;

    virtual protected void Awake() => ResetLoop();
    virtual public IEnumerator WaitLoop(Action successCallback, Action failCallback)
    {
        float step = ConstantValues.WAIT_ZONES_TIME_STEP;

        while (_currentTime > 0f)
        {
            yield return new WaitForSeconds(step);
            LoopCycle(_speedModifier * step, failCallback);
        }
        ResetLoop();
        yield return StartCoroutine(OnSuccess());
        successCallback();
    }

    virtual protected void LoopCycle(float step, Action failCallback)
    {
        _currentTime -= step;
    }

    virtual protected IEnumerator OnSuccess()
    {
        yield return null;
    }
    virtual public void ResetLoop() => _currentTime = _maxTime;

    public void ChangeSpeed(float multiplier)
    {
        _speedModifier = multiplier;
    }
}
