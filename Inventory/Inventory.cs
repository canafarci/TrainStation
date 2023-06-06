using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;


//Add and remove StackableItems and NPCs to the linkedLists and keep track of them
public class Inventory : MonoBehaviour
{
    public int MaxItemCount { get { return _maxStackSize; } }
    public int MaxFollowerSize { get { return _maxFollowerSize; } }
    public int ItemCount { get { return _linkedList.Count; } }
    public int FollowerCount { get { return _followers.Count; } }
    [SerializeField] int _maxStackSize, _maxFollowerSize;
    LinkedList<NavMeshNPC> _followers = new LinkedList<NavMeshNPC>();
    LinkedList<StackableItem> _linkedList = new LinkedList<StackableItem>();
    IStackTween _tweener;
    StackPositionCalculator _positionCalculator;
    public event Action<int> InventorySizeChangeHandler, FollowerCountChangeHandler;

    private void Awake()
    {
        _tweener = GetComponent<IStackTween>();
        _positionCalculator = GetComponentInChildren<StackPositionCalculator>();
    }
    public void AddFollowerToList(NavMeshNPC npc)
    {
        _followers.AddFirst(npc);
        FollowerCountChangeHandler?.Invoke(_followers.Count);
    }
    public void RemoveFollower(NavMeshNPC npc)
    {
        _followers.Remove(npc);
        FollowerCountChangeHandler?.Invoke(_followers.Count);
    }
    public void StackItem(StackableItem item)
    {
        item.transform.parent = _positionCalculator.transform;
        Vector3 endPos = _positionCalculator.CalculatePosition(_linkedList, item);
        StartCoroutine(_tweener.StackTween(item, endPos));

        _linkedList.AddFirst(item);
        InventorySizeChangeHandler?.Invoke(_linkedList.Count);
    }
    public void RemoveItem(StackableItem item)
    {
        _linkedList.Remove(item);
        RecalculatePositions();
        InventorySizeChangeHandler?.Invoke(_linkedList.Count);
    }
    public StackableItem[] GetItems(Enums.StackableItemType itemType) => _linkedList.Where(x => x.ItemType == itemType).ToArray();
    public NavMeshNPC[] GetFollowers(Enums.FollowerType followerType) => _followers.Where(x => x.FollowerType == followerType).ToArray();
    private void RecalculatePositions() => _positionCalculator.RecalculatePositions(_linkedList);

    // void CheckType(NavMeshNPC npc)
    // {
    //     if (FollowerCount < 1) { return; }

    //     Enums.FollowerType typeInInventory = _followers.First.Value.FollowerType;
    //     if (typeInInventory == npc.FollowerType) { return; }

    //     foreach (NavMeshNPC character in GetFollowers(typeInInventory))
    //     {
    //         character.ReturnToQueue();
    //     }
    // }
}
