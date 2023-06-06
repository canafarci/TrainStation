using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMoveFX : MonoBehaviour
{
    [SerializeField] Animator[] _animators;
    Animator _animator;
    [SerializeField] Animator _animator2;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveToNewStartPosFX()
    {
        _animator.Play("LocoAnim", -1, 0f);
    }

    public void TakeOffFX()
    {
        _animator.Play("SpeedUpFX", -1, 0f);
        _animator2.Play("SpeedUpFX2", -1, 0f);
       
        foreach (Animator an in _animators)
        {
            an.Play("Moving");
        }
    }
    public void ReturnFX()
    {
        foreach (Animator an in _animators)
        {
            an.Play("Idle");
        }
    }
}
