using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableAcceptWaitingLoop : AcceptActionWaitingLoop
{
    public override IEnumerator WaitLoop(Action successCallback, Action failCallback)
    {
        if (GameManager.Instance.References.PlayerInventory.ItemCount > 0) { failCallback(); yield break; }

        yield return base.WaitLoop(successCallback, failCallback);
    }
}
