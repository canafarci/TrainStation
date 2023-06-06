using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicDropZone : DropZone
{
    TrainState _state;
    [SerializeField] Transform[] _repairSpots;
    [SerializeField] Transform _trainTransform;
    [SerializeField] ParticleSystem[] _fxs;
    private void Awake() => _state = GetComponentInParent<TrainState>();
    protected override void Drop(NavMeshNPC[] npcs)
    {
        if (!_state.TrainIsBroken || npcs.Length != 2) { return; }

        for (int i = 0; i < npcs.Length; i++)
        {
            GameManager.Instance.References.PlayerInventory.RemoveFollower(npcs[i]);

            MechanicRepairSequence seq = npcs[i].GetComponent<MechanicRepairSequence>();
            seq.Repair(_repairSpots[i], () => _state.OnTrainRepaired());
            seq.FX = _fxs[i];
            seq.TrainTransform = _trainTransform;
        }
    }
}
