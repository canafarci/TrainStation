using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockLoop : PayLoop
{

    IUnlockable _unlockable;
    protected override void Awake()
    {
        base.Awake();
        _unlockable = GetComponent<IUnlockable>();
    }
    public override IEnumerator WaitLoop(Action successCallback, Action failCallback)
    {
        yield return base.WaitLoop(successCallback, failCallback);
        _unlockable.Unlock();
    }
}
