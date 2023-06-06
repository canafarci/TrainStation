using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageAccept : MonoBehaviour, IAcceptAction
{
    [SerializeField] NPCWaitQueue _luggageQueue, _sitQueue;
    IStackTween _tweener;
    [SerializeField] Transform _targetPos;
    [SerializeField] MoneyStacker _stacker;

    private void Awake()
    {
        _tweener = GetComponent<IStackTween>();
    }
    public IEnumerator OnAccept()
    {
        NavMeshNPC npc = _luggageQueue.Peek();
        StackableItem luggage = npc.GetComponent<FollowerType>().Luggage.GetComponent<StackableItem>();

        StartCoroutine(_tweener.StackTween(luggage, _targetPos.position));
        npc.GetComponent<FollowerState>().HasItem = false;
        npc.GetComponent<FollowerType>().DisableCircle();

        _luggageQueue.Dequeue(npc);
        _sitQueue.AddToQueue(npc);

        _stacker.StackMoney(GameManager.Instance.References.GameConfig.StackPerLuggageAccept);
        yield return null;
    }
}