using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : NavMeshNPC
{
    NavMeshAnimator _animator;
    List<ConsumableDropZone> _list = new List<ConsumableDropZone>();
    ConsumablePickup _pickup;
    Inventory _inventory;
    int _currentDropZoneIndex = 0;
    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<NavMeshAnimator>();
        _inventory = GetComponent<Inventory>();
        _pickup = FindObjectOfType<ConsumablePickup>();
    }

    private void Start()
    {
        StartCoroutine(WaiterLoop());
    }
    public void AddToList(ConsumableDropZone cdz) => _list.Add(cdz);

    IEnumerator WaiterLoop()
    {
        while (true)
        {
            //check if inventory has consumable
            if (_inventory.ItemCount > 0)
            {
                //if yes, go to current cdz in list and try drop
                ConsumableDropZone cdz = _list[_currentDropZoneIndex];
                yield return StartCoroutine(GetToPosCoroutine(cdz.transform.position));
                cdz.NPCDrop(_inventory, _animator, _inventory.GetItems(Enums.StackableItemType.Consumable)[0]);
                //increment current cdz index   
                IncreaseCounter();
                //restart loop
                continue;
            }
            //if there is no consumable
            else
            {
                //go to pickup zone
                yield return StartCoroutine(GetToPosCoroutine(_pickup.transform.position));
                _pickup.NPCAccept(_inventory);
            }
            //restart loop

        }
    }
    void IncreaseCounter()
    {
        if (_currentDropZoneIndex == _list.Count - 1)
            _currentDropZoneIndex = 0;
        else
            _currentDropZoneIndex += 1;
    }

}
