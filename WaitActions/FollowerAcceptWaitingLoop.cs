using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAcceptWaitingLoop : AcceptActionWaitingLoop, ICheckableLoop
{
    [SerializeField] NPCWaitQueue _queue, _luggageQueue, _sitQueue;
    QueueSize _sizeChecker;
    protected override void Awake()
    {
        base.Awake();
        _sizeChecker = FindObjectOfType<QueueSize>();
    }
    public override IEnumerator WaitLoop(Action successCallback, Action failCallback)
    {

        if (!CanAccept()) { failCallback(); yield break; }

        yield return base.WaitLoop(successCallback, failCallback);
    }

    public bool CanAccept()
    {
        NavMeshNPC npc = _queue.Peek();
        if (npc == null)
            return false;
        FollowerState state = npc.GetComponent<FollowerState>();

        //return if there is not enough space
        if (!_sizeChecker.CanAcceptNPCs)
            return false;
        if (state.HasItem && _luggageQueue.QueueIsFull)
            return false;

        return true;
    }
}
