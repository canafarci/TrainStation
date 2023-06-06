using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatUnlock : MonoBehaviour, ICustomUnlockBehaviour
{
    [SerializeField] SitPosition[] _sitPositions;
    [SerializeField] NPCSeatingArea _seatingArea;
    public void OnUnlock()
    {
        _seatingArea.UpdateSitPositions(_sitPositions);
    }
}
