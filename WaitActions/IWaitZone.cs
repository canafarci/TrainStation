using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWaitZone
{
    IEnumerator WaitLoop(Action successCallback, Action failCallback);
    public void ResetLoop();
    public void ChangeSpeed(float multiplier);
}
