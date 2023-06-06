using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayUnlockTrigger : WaitTrigger
{
    [SerializeField] GameObject _objectToSwitchActivation;

    override protected void Awake()
    {
        base.Awake();

        _failCallback = () =>
        {
            StopAllCoroutines();
        };
    }

    protected override void OnEnterTrigger()
    {
        base.OnEnterTrigger();
        _objectToSwitchActivation.SetActive(true);
    }

    protected override void OnExitTrigger()
    {
        base.OnExitTrigger();
        _objectToSwitchActivation.SetActive(false);
    }
}
