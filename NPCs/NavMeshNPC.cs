using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNPC : MonoBehaviour
{
    public Enums.FollowerType FollowerType;
    protected NavMeshAgent _agent;
    protected Coroutine _moveRoutine = null;
    protected Coroutine _followLoop;
    public Transform Target;
    virtual protected void Awake() => _agent = GetComponent<NavMeshAgent>();
    public void GetToPos(Vector3 pos, Action callback = null)
    {
        CancelMoveRoutine();
        _moveRoutine = StartCoroutine(GetToPosCoroutine(pos, callback));
    }

    public IEnumerator CallGetToPosCoroutine(Vector3 pos, Action callback = null)
    {
        CancelMoveRoutine();
        _moveRoutine = StartCoroutine(GetToPosCoroutine(pos, callback));
        yield return _moveRoutine;
    }

    protected IEnumerator GetToPosCoroutine(Vector3 pos, Action callback = null)
    {
        yield return new WaitForSeconds(.1f);

        Vector3 tarxz = new Vector3(pos.x, 0, pos.z);
        _agent.destination = tarxz;

        for (int i = 0; i < Mathf.Infinity; i++)
        {
            yield return new WaitForSeconds(.25f);
            if (_agent == null) { yield break; }

            Vector3 posxz = new Vector3(transform.position.x, 0f, transform.position.z);
            //print(Vector3.Distance(posxz, tarxz));
            if (Vector3.Distance(posxz, tarxz) < 0.25f)
            {
                if (callback != null)
                    callback();

                yield break;
            }
        }
    }

    protected void CancelMoveRoutine()
    {
        if (_moveRoutine != null)
            StopCoroutine(_moveRoutine);
    }

    IEnumerator FollowLoop()
    {
        for (int i = 0; i < Mathf.Infinity; i++)
        {
            if (_agent == null) yield break;

            _agent.destination = Target.transform.position;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void FollowPlayer(bool isInQueue = false)
    {
        StopAllCoroutines();
        ResetValues();
        Inventory inventory = GameManager.Instance.References.PlayerInventory;

        inventory.AddFollowerToList(this);
        Target = inventory.transform;
        _followLoop = StartCoroutine(FollowLoop());
    }
    public void StopFollow()
    {
        StopCoroutine(_followLoop);
        _agent.isStopped = true;

    }

    protected void ResetValues()
    {
        _agent.stoppingDistance = 1.5f;
        _agent.radius = 0.5f;
        GetComponentInChildren<Animator>().SetTrigger("MoveEmpty");
    }

    virtual public void ReturnToQueue()
    {
        StopAllCoroutines();
    }
}
