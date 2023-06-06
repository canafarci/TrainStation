using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ContiniousAcceptTrigger : WaitTrigger
{
    //override awake function to make callback restart the loop on loop completion
    override protected void Awake()
    {
        base.Awake();
        _failCallback = () =>
        {
            StopAllCoroutines();
            GameManager.Instance.References.Slider.SetActive(false);
        };
        _successCallback = () =>
        {
            StartCoroutine(_waitZone.WaitLoop(_successCallback, _failCallback));
        };
    }

    protected override void OnEnterTrigger()
    {
        GameManager.Instance.References.Slider.SetActive(true);
        base.OnEnterTrigger();
    }

    protected override void OnExitTrigger()
    {
        base.OnExitTrigger();
        GameManager.Instance.References.Slider.SetActive(false);
    }
}
