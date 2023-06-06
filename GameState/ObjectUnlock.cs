using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectUnlock : Unlocker
{
    [SerializeField] GameObject[] _objectToSwitchActivation;

    override public void Unlock()
    {
        _objectToSwitchActivation.ToList().ForEach(x => x.SetActive(!x.activeSelf));
        base.Unlock();
        gameObject.SetActive(false);
    }
}
