using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Follower : NavMeshNPC
{
    public void GetToPosAndSit(Transform transform, Action callback = null)
    {
        CancelMoveRoutine();
        _moveRoutine = StartCoroutine(GetToPosAndSitRoutine(transform, callback));
    }
    public IEnumerator OpenDoorAndGetInRoutine(Transform trans, Action callback)
    {
        StopCoroutine(_followLoop);
        _agent.isStopped = false;
        CancelMoveRoutine();
        _agent.radius = 0.05f;
        _agent.stoppingDistance = 0f;
        yield return StartCoroutine(GetToPosCoroutine(trans.position));
        transform.DORotate(trans.rotation.eulerAngles, 0.1f);

        GetComponentInChildren<Animator>().Play("Car Door Open");
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(Vector3.one * 0.0001f, .5f);
        if (callback != null)
            callback();
        Destroy(gameObject, 0.6f);
    }
    IEnumerator GetToPosAndSitRoutine(Transform trans, Action callback)
    {
        yield return StartCoroutine(GetToPosCoroutine(trans.position));

        Tween move = transform.DOMove(trans.position, .5f);
        Tween rot = transform.DORotate(trans.rotation.eulerAngles, .5f);

        yield return move.WaitForCompletion();
        GetComponentInChildren<Animator>().Play("NPC Standing to Sit");
        GetComponent<FollowerUI>().ConsumableUIActive = true;
        if (callback != null)
            callback();
    }
    public override void ReturnToQueue()
    {
        base.ReturnToQueue();
        FindObjectOfType<NPCSeatingArea>().AddToQueue(this);
        GetComponent<FollowerState>().FollowingPlayer = false;
    }

}
