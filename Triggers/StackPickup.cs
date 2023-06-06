using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPickup : MonoBehaviour
{
    [SerializeField] float _clearStackRate;
    Stacker _stacker;
    Coroutine _unloadCoroutine;
    void Awake() => _stacker = GetComponent<Stacker>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _unloadCoroutine = StartCoroutine(UnloadLoop());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StopCoroutine(_unloadCoroutine);
    }

    IEnumerator UnloadLoop()
    {
        Inventory playerStacker = GameManager.Instance.References.PlayerInventory;

        for (int i = 0; i < Mathf.Infinity; i++)
        {
            yield return new WaitForSeconds(_clearStackRate);

            StackableItem item;
            if (playerStacker.MaxItemCount > playerStacker.ItemCount && _stacker.ItemStack.TryPop(out item))
            {
                playerStacker.StackItem(item);
            }
        }
    }
}
