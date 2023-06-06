using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MechanicRepairSequence : MonoBehaviour
{
    Mechanic _mechanic;
    Animator _animator;
    public ParticleSystem FX;
    public Transform TrainTransform;
    private void Awake()
    {
        _mechanic = GetComponent<Mechanic>();
        _animator = GetComponentInChildren<Animator>();
    }
    public void Repair(Transform trans, Action callback) => StartCoroutine(RepairSequenceCoroutine(trans, callback));
    IEnumerator RepairSequenceCoroutine(Transform trans, Action callback)
    {
        yield return StartCoroutine(_mechanic.GetToPosForRepair(trans.position));
        transform.DORotate(trans.rotation.eulerAngles, 0.1f);
        _animator.Play("Crouch");
        DropToolbox();
        FX.Play();
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger("Idle");
        yield return new WaitForSeconds(1f);
        FX.Stop();
        _mechanic.ReturnToQueue();
        callback();
    }

    void DropToolbox()
    {
        StackableItem item = _mechanic.Item;
        item.transform.parent = null;
        Vector3[] path = new Vector3[3] {transform.position,
                                        (transform.position + TrainTransform.position) /2f + new Vector3(0f, 2f, 0f),
                                        TrainTransform.position
                                        };

        item.transform.DOPath(path, 1f).onComplete = () =>
        {
            item.transform.position = _mechanic.ToolboxSlot.transform.position;
            StartCoroutine(_mechanic.ToolboxSlot.DropItem(item));
        };
    }
}
