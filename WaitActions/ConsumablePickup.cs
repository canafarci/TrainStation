using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablePickup : MonoBehaviour, IAcceptAction
{
    [SerializeField] Transform _spawnPosition;
    PlayerAnimator _animator;
    private void Awake() => _animator = FindObjectOfType<PlayerAnimator>();
    public IEnumerator OnAccept()
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;
        GameObject consumablePrefab = GameManager.Instance.References.GameConfig.ConsumablePrefab;

        if (inventory.ItemCount >= inventory.MaxItemCount) { yield break; }

        StackableItem item = GameObject.Instantiate(consumablePrefab, _spawnPosition).GetComponent<StackableItem>();
        inventory.StackItem(item);
        _animator.SetHoldingIdle();

        yield return null;
    }

    public void NPCAccept(Inventory inventory)
    {
        GameObject consumablePrefab = GameManager.Instance.References.GameConfig.ConsumablePrefab;
        if (inventory.ItemCount >= inventory.MaxItemCount) { return; }

        StackableItem item = GameObject.Instantiate(consumablePrefab, _spawnPosition).GetComponent<StackableItem>();
        inventory.StackItem(item);
        inventory.GetComponent<NavMeshAnimator>().SetHoldingIdle();
    }
}
