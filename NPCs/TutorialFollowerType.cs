using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFollowerType : FollowerType
{
    protected override bool DecideFollowerType()
    {
        if (FindObjectsOfType<TutorialFollowerType>().Length == 1)
            return true;
        else
            return false;
    }
}
