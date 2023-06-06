using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour, IDropAction
{
    public void CheckDrop(Enums.FollowerType followerType)
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;
        NavMeshNPC[] npcs = inventory.GetFollowers(followerType);
        if (npcs.Length == 0) { return; }

        Drop(npcs);
    }

    public void CheckDrop(Enums.StackableItemType itemType)
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;
        StackableItem[] items = inventory.GetItems(itemType);
        if (items.Length == 0) { return; }

        Drop(items);
    }

    virtual protected void Drop(NavMeshNPC[] npcs)
    {
        throw new System.NotImplementedException();
    }
    virtual protected void Drop(StackableItem[] items)
    {
        throw new System.NotImplementedException();
    }
}
