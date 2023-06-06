using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayAcceptLoop : PayLoop
{
    IAcceptAction _acceptable;
    protected override void Awake()
    {
        base.Awake();
        _acceptable = GetComponent<IAcceptAction>();
    }
    public override IEnumerator WaitLoop(Action successCallback, Action failCallback)
    {
        yield return base.WaitLoop(successCallback, failCallback);
    }

    protected override IEnumerator OnSuccess()
    {
        yield return StartCoroutine(_acceptable.OnAccept());
    }
}
