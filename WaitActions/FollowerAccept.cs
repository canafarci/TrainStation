using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! FollowerWaitingLoop depends on this
[RequireComponent(typeof(BoxCollider))]
public class FollowerAccept : MonoBehaviour, IAcceptAction
{
    [SerializeField] NPCWaitQueue _queue, _luggageQueue, _sitQueue;
    [SerializeField] MoneyStacker _stacker;
    QueueSize _sizeChecker;
    private void Awake() => _sizeChecker = FindObjectOfType<QueueSize>();
    public IEnumerator OnAccept()
    {
        NavMeshNPC npc = _queue.Peek();
        FollowerState state = npc.GetComponent<FollowerState>();
        //Set npc state to have been accepted inside
        npc.GetComponent<FollowerState>().HasBeenAcceptedInside = true;
        _queue.Dequeue(npc);
        _sizeChecker.OnAccept();
        _stacker.StackMoney(GameManager.Instance.References.GameConfig.StackPerFollowerAccept);

        if (state.HasItem)
            _luggageQueue.AddToQueue(npc);

        else if (!state.HasItem)
        {
            _sitQueue.AddToQueue(npc);
        }

        yield return null;
    }
}
