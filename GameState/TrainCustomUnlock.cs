using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCustomUnlock : CustomCameraUnlock
{
    [SerializeField] MoveTrain _mover;
    override public void OnUnlock()
    {
        base.OnUnlock();
        _mover.FirstReturn();
    }
}
