using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackTween
{
    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Action callback = null);
    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Vector3 endRot, Action callback = null);
}
