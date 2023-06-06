using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCSeatingArea : NPCWaitQueue
{
    public int MaxSize { get { return _sitPositions.Length; } }
    [SerializeField] SitPosition[] _sitPositions;
    public event Action<int> MaxSizeChangeHandler;
    private void Start() => MaxSizeChangeHandler.Invoke(_sitPositions.Length);
    public void UpdateSitPositions(SitPosition[] newPositions)
    {
        SitPosition[] positions = new SitPosition[_sitPositions.Length + newPositions.Length];

        for (int i = 0; i < _sitPositions.Length; i++)
            positions[i] = _sitPositions[i];

        for (int i = 0; i < newPositions.Length; i++)
            positions[i + _sitPositions.Length] = newPositions[i];

        _sitPositions = positions;
        MaxSizeChangeHandler.Invoke(_sitPositions.Length);
    }
    public new void Dequeue(NavMeshNPC npc)
    {
        SitPosition pos = _sitPositions.First(x => x.NPC == npc);
        pos.NPC = null;
    }
    public override void MoveToPositions(NavMeshNPC npc)
    {
        SitPosition pos = _sitPositions.
        Where(x => x.IsOccupied == false).
        OrderBy(x => Vector3.Distance(npc.transform.position, x.transform.position)).FirstOrDefault();

        pos.NPC = npc;
        npc.GetComponent<Follower>().GetToPosAndSit(pos.transform,
                                        () => npc.GetComponent<FollowerState>().IsSeated = true);

    }
}
