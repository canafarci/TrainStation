using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialPayUnlockTrigger : PayUnlockTrigger
{
    protected override void Awake()
    {
        base.Awake();

        _successCallback = () =>
        {
            _waitZone.ResetLoop();
        };

        _failCallback = () =>
        {
            StopAllCoroutines();
        };
    }
}
