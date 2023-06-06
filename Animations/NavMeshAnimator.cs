using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshAnimator : MonoBehaviour, IChangeableIdleAnimation
{
    [SerializeField] AnimationClip _idle, _holdingIdle, _running, _runningFullHands;
    NavMeshAgent _agent;
    Animator _animator;
    AnimatorOverrideController _animatorOverrideController;
    int _moveEmptyHash = Animator.StringToHash("MoveEmptyHands");
    int _idleHash = Animator.StringToHash("Idle");
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;
    }
    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", Vector3.Magnitude(_agent.velocity) / _agent.speed);
    }
    public void SetHoldingIdle()
    {
        _animatorOverrideController["IDLE"] = _holdingIdle;
        _animatorOverrideController["Running"] = _runningFullHands;

    }
    public void ResetIdle()
    {
        _animatorOverrideController["IDLE"] = _idle;
        _animatorOverrideController["Running"] = _running;
    }

}
