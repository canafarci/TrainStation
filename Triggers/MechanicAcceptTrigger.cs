using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicAcceptTrigger : ResettedAcceptTrigger
{
    [SerializeField] NPCWaitQueue _queue;
    [SerializeField] Animator _npcAnimator;
    protected override void Awake()
    {
        base.Awake();

        _successCallback = () =>
        {
            _waitZone.ResetLoop();
            GameManager.Instance.References.Slider.SetActive(false);
        };

        _failCallback = () =>
        {
            StopAllCoroutines();
            GameManager.Instance.References.Slider.SetActive(false);
        };
    }
    protected override void OnEnterTrigger()
    {
        if (!AnyTrainIsBroken() || !_queue.QueueIsFull) { return; }
        _npcAnimator.Play("NPC Idle to Type");
        GameManager.Instance.References.Slider.SetActive(true);
        base.OnEnterTrigger();
    }

    protected override void OnExitTrigger()
    {
        base.OnExitTrigger();
        _npcAnimator.SetTrigger("WritingToIdle");
        GameManager.Instance.References.Slider.SetActive(false);
        _waitZone.ResetLoop();
    }
    bool AnyTrainIsBroken()
    {
        bool canAcceptMechanics = false;
        foreach (TrainState ts in FindObjectsOfType<TrainState>())
            canAcceptMechanics = canAcceptMechanics || ts.TrainIsBroken;

        return canAcceptMechanics;
    }
}
