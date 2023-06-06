using UnityEngine;

public class WaitingSpot
{
    public WaitingSpot(Transform transform)
    {
        Transform = transform;
    }
    public Transform Transform;
    public bool IsOccupied = false;
}
