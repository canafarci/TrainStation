using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTrigger : MonoBehaviour
{
    protected IWaitZone _waitZone;
    protected Action _successCallback, _failCallback;

    virtual protected void Awake()
    {
        _waitZone = GetComponent<IWaitZone>();
        _successCallback = () => { };
        _failCallback = () => { };
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnterTrigger();
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnExitTrigger();
        }
    }
    virtual protected void OnEnterTrigger()
    {
        StartCoroutine(_waitZone.WaitLoop(_successCallback, _failCallback));
    }

    virtual protected void OnExitTrigger()
    {
        StopAllCoroutines();
    }
}
