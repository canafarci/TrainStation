using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour, IUnlockable
{
    protected ISaveable _saver;
    ICustomUnlockBehaviour _customUnlockBehaviour;

    virtual protected void Awake()
    {
        _saver = GetComponent<ISaveable>();
        _customUnlockBehaviour = GetComponent<ICustomUnlockBehaviour>();
    }
    private void Start()
    {
        _saver.Load(() => Unlock());
    }
    virtual public void Unlock()
    {
        _saver.Save(1);
        if (_customUnlockBehaviour != null)
            _customUnlockBehaviour.OnUnlock();

    }
}
