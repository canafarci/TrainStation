using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropAction
{
    public void CheckDrop(Enums.FollowerType followerType);
    public void CheckDrop(Enums.StackableItemType itemType);
}
