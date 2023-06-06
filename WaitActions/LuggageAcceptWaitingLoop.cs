using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageAcceptWaitingLoop : AcceptActionWaitingLoop, ICheckableLoop
{
    [SerializeField] NPCWaitQueue _luggageQueue;


    public override IEnumerator WaitLoop(Action successCallback, Action failCallback)
    {
        if (!CanAccept()) { failCallback(); yield break; }

        yield return base.WaitLoop(successCallback, failCallback);
    }
    public bool CanAccept()
    {
        NavMeshNPC npc = _luggageQueue.Peek();
        if (npc == null)
            return false;

        return true;
    }
}
