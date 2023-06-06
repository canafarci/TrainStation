using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableDropZone : DropZone
{
    [SerializeField] SitPosition[] _sitPositions;
    [SerializeField] MoneyStacker _stacker;
    IStackTween _tweener;
    private void Awake()
    {
        _tweener = GetComponent<IStackTween>();
        FindObjectOfType<Waiter>(true).AddToList(this);
    }
    protected override void Drop(StackableItem[] items)
    {
        StackableItem item = items[0];

        PlayerAnimator animator = FindObjectOfType<PlayerAnimator>();
        Inventory inventory = GameManager.Instance.References.PlayerInventory;
        ConsumableDrop(inventory, animator, item);

    }

    public void NPCDrop(Inventory inventory, IChangeableIdleAnimation animator, StackableItem item) => ConsumableDrop(inventory, animator, item);
    void ConsumableDrop(Inventory inventory, IChangeableIdleAnimation animator, StackableItem item)
    {
        GameObject consumablePrefab = GameManager.Instance.References.GameConfig.ConsumablePrefab;
        Vector3 itemPos = item.transform.position;
        Quaternion itemRot = item.transform.rotation;

        foreach (SitPosition sitPos in _sitPositions)
        {
            if (!sitPos.IsOccupied) { continue; }
            FollowerState state = sitPos.NPC.GetComponent<FollowerState>();
            if (state.HadConsumable || !state.IsSeated) { continue; }
            FollowerUI ui = sitPos.NPC.GetComponent<FollowerUI>();

            inventory.RemoveItem(item);
            animator.ResetIdle();
            Destroy(item.gameObject);

            StackableItem itemInstance = GameObject.Instantiate(consumablePrefab,
                                                                 itemPos,
                                                                 itemRot,
                                                                 sitPos.NPC.transform).
                                                                    GetComponent<StackableItem>();

            StartCoroutine(_tweener.StackTween(itemInstance,
                                                Vector3.zero,
                                                () => Destroy(itemInstance.gameObject)));

            state.HadConsumable = true;
            ui.ConsumableUIActive = false;

            _stacker.StackMoney(GameManager.Instance.References.GameConfig.StackPerConsumableDrop, sitPos.NPC.transform);
        }
    }
}
