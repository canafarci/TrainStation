using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoNPCAcceptZoneTrigger : ContiniousAcceptTrigger
{
    Coroutine _loopCoroutine;
    [SerializeField] Animator _npcAnimator;
    [SerializeField] GameObject _slider;
    ICheckableLoop _checkableLoop;
    override protected void Awake()
    {
        base.Awake();
        _checkableLoop = GetComponent<ICheckableLoop>();
        _failCallback = () =>
        {
            _npcAnimator.SetBool("Writing", false);
            _slider.SetActive(false);

            if (_loopCoroutine != null)
                StopCoroutine(_loopCoroutine);
        };
        _successCallback = () =>
        {
            StartLoop();
        };
    }
    private void Start() => StartCoroutine(CheckLoop());
    protected override void OnEnterTrigger() => _waitZone.ChangeSpeed(4f);
    protected override void OnExitTrigger() => _waitZone.ChangeSpeed(1f);
    IEnumerator CheckLoop()
    {
        while (true)
        {
            CheckAndStartLoop();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CheckAndStartLoop()
    {
        if (_loopCoroutine != null || !_checkableLoop.CanAccept()) { return; }
        StartLoop();
    }

    void StartLoop()
    {
        _slider.SetActive(true);
        _npcAnimator.SetBool("Writing", true);
        _loopCoroutine = StartCoroutine(_waitZone.WaitLoop(_successCallback, _failCallback));
    }
}
