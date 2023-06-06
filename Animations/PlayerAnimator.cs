using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IChangeableIdleAnimation
{
    public bool IsMoving;
    [SerializeField] AnimationClip _idle, _holdingIdle, _walking, _holdingWalking;
    AnimatorOverrideController _animatorOverrideController;
    bool _playingMove, _handsFull, _playingWalking;
    Animator _animator;
    Inventory _inventory;
    string _idleTrigger = "Idle";
    string _moveTrigger = "MoveEmpty";
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void OnEnable() => _inventory.InventorySizeChangeHandler += OnStackSizeChange;
    private void Start()
    {
        _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverrideController;
    }
    public void SetHoldingIdle() => _animatorOverrideController["IDLE"] = _holdingIdle;
    public void ResetIdle() => _animatorOverrideController["IDLE"] = _idle;
    private void OnStackSizeChange(int size)
    {
        print("Inventory Size: " + size.ToString());

        if (size != 0)
        {
            _animatorOverrideController["Running"] = _holdingWalking;
        }
        else
        {
            _animatorOverrideController["Running"] = _walking;
        }
    }

    private void FixedUpdate()
    {
        if (!_playingMove && IsMoving)
        {
            _animator.SetTrigger(_moveTrigger);
            _playingMove = true;
        }
        else if (!IsMoving && _playingMove)
        {
            _animator.SetTrigger(_idleTrigger);
            _playingMove = false;
        }
    }
}
