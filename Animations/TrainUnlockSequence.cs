using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainUnlockSequence : MonoBehaviour, IUnlockSequence
{
    [SerializeField] Vector3[] _trainStartPositions;
    [SerializeField] Transform _endCar;
    MoveTrain _mover;
    TrainState _state;

    private void Awake()
    {
        _mover = GetComponentInParent<MoveTrain>();
        _state = GetComponentInParent<TrainState>();
    }

    public void UnlockSequence(int targetIndex)
    {
        _endCar.parent = null;
        _state.MaxPassengers += 4;
        _mover.MoveToNewStartPos(_trainStartPositions[targetIndex], () => _endCar.parent = _mover.transform);
    }
}
