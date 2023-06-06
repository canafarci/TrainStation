using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitPosition : MonoBehaviour
{
    public NavMeshNPC NPC = null;
    public bool IsOccupied { get { return NPC != null; } }
}
