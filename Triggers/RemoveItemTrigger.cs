using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemTrigger : MonoBehaviour
{
    [SerializeField] Transform _target;
    IStackTween _tweener;
    private void Awake()
    {
        _tweener = GetComponent<IStackTween>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = GameManager.Instance.References.PlayerInventory;

            if (inventory.ItemCount == 0) { return; }

            StackableItem[] items = inventory.GetItems(Enums.StackableItemType.Consumable);
            FindObjectOfType<PlayerAnimator>().ResetIdle();
            foreach (StackableItem item in items)
            {
                inventory.RemoveItem(item);
                item.transform.parent = _target;
                StartCoroutine(_tweener.StackTween(item, Vector3.zero, () => Destroy(item.gameObject)));
            }
        }
    }
}
