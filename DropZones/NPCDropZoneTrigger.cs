using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDropZoneTrigger : DropZoneTrigger
{
    [SerializeField] Enums.FollowerType _followerType;

    protected override void OnEnterZone()
    {
        _dropZone.CheckDrop(_followerType);
    }
}
