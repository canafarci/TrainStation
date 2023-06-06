using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic : NavMeshNPC
{
    [SerializeField] NPCWaitQueue _queue;
    [SerializeField] Transform _hand;
    [SerializeField] ToolboxSlot _slot;
    NavMeshAnimator _animator;
    public StackableItem Item { get { return _hand.GetComponentInChildren<StackableItem>(); } }
    public ToolboxSlot ToolboxSlot { get { return _slot; } }
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<NavMeshAnimator>();
    }
    private void Start() => _queue.AddToQueue(this);
    public void PickItemAndFollowPlayer()
    {
        CancelMoveRoutine();
        StartCoroutine(PickItemAndFollowPlayerRoutine());
    }
    public IEnumerator GetToPosForRepair(Vector3 pos)
    {
        StopAllCoroutines();
        _agent.stoppingDistance = 0.00001f;
        _agent.radius = 0.05f;
        _moveRoutine = StartCoroutine(GetToPosCoroutine(pos));
        yield return _moveRoutine;
    }

    IEnumerator PickItemAndFollowPlayerRoutine()
    {
        yield return StartCoroutine(CallGetToPosCoroutine(_slot.transform.position));
        yield return StartCoroutine(_slot.PickUpItem(_hand));
        _animator.SetHoldingIdle();
        FollowPlayer();
    }
    override public void ReturnToQueue()
    {
        base.ReturnToQueue();
        _animator.ResetIdle();
        _agent.stoppingDistance = 0.00001f;
        _queue.AddToQueue(this);
    }
}
