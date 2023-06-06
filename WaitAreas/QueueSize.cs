using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueSize : MonoBehaviour
{
    public bool CanAcceptNPCs { get { return _seatingArea.MaxSize > _activeNPCs; } }
    NPCSeatingArea _seatingArea;
    int _activeNPCs = 0;
    public event Action<int> ActiveNPCCountChangeHandler;
    private void Awake()
    {
        _seatingArea = FindObjectOfType<NPCSeatingArea>();
    }

    public void OnAccept()
    {
        _activeNPCs++;
        ActiveNPCCountChangeHandler.Invoke(_activeNPCs);
    }
    public void OnDepart()
    {
        _activeNPCs--;
        ActiveNPCCountChangeHandler.Invoke(_activeNPCs);
    }
}
