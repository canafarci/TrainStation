using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//!NPC Spawner depends on this
public class NPCWaitQueue : MonoBehaviour
{
    public bool QueueIsFull { get { return _list.Count >= _waitTransforms.Length; } }
    [SerializeField] Transform[] _waitTransforms;
    LinkedList<NavMeshNPC> _list = new LinkedList<NavMeshNPC>();

    //adds spawned NPC to the Q
    public void AddToQueue(NavMeshNPC npc)
    {
        _list.AddLast(npc);
        MoveToPositions(npc);
    }
    public NavMeshNPC Peek()
    {
        if (_list.Count < 1) { return null; }
        return _list.First.Value;
    }
    public void Dequeue(NavMeshNPC npc)
    {
        _list.RemoveFirst();
        MoveToPositions(npc);
    }
    //move Q on NPC spawn
    virtual public void MoveToPositions(NavMeshNPC npc)
    {
        LinkedListNode<NavMeshNPC> node = _list.First;
        for (int i = 0; i < _list.Count; i++)
        {
            node.Value.GetToPos(_waitTransforms[i].position);
            node = node.Next;
        }
    }
}