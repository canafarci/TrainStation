using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItemDropZoneTrigger : DropZoneTrigger
{
    [SerializeField] Enums.StackableItemType _stackableItemType;

    protected override void OnEnterZone()
    {
        _dropZone.CheckDrop(_stackableItemType);
    }
}
