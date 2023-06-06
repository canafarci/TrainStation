using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneTrigger : MonoBehaviour
{
    protected IDropAction _dropZone;

    protected void Awake()
    {
        _dropZone = GetComponent<IDropAction>();
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnterZone();
        }
    }

    virtual protected void OnEnterZone()
    {
        throw new System.NotImplementedException();
    }
}
