using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptActionWaitingLoop : WaitingLoop
{
    IFillable _fillable;
    IAcceptAction _acceptAction;

    //add fill UI feedback and and add Follower Accept behaviour to the waiting loop
    override protected void Awake()
    {
        base.Awake();
        _acceptAction = GetComponent<IAcceptAction>();
    }
    public override IEnumerator WaitLoop(Action successCallback, Action failCallback)
    {
        yield return base.WaitLoop(successCallback, failCallback);
    }
    protected override IEnumerator OnSuccess()
    {
        yield return StartCoroutine(_acceptAction.OnAccept());
    }

    protected override void LoopCycle(float step, Action failCallback)
    {
        base.LoopCycle(step, failCallback);
        _fillable.SetFill(_maxTime, _currentTime);
    }
    public override void ResetLoop()
    {
        base.ResetLoop();
        if (_fillable == null)
            _fillable = GetComponent<IFillable>();
        _fillable.SetFill(_maxTime, 0);
    }
}
