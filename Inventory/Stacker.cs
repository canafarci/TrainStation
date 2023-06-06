using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(StackPositionCalculator))]
public class Stacker : MonoBehaviour
{
    public Stack<StackableItem> ItemStack { get { return _stack; } }
    public int MaxStackSize { get { return _maxStackSize; } }
    [SerializeField] int _maxStackSize;
    Stack<StackableItem> _stack = new Stack<StackableItem>();
    StackPositionCalculator _positionCalculator;

    private void Awake() => _positionCalculator = GetComponent<StackPositionCalculator>();

    public void StackItem(StackableItem item)
    {
        item.transform.parent = transform;

        Vector3 endPos = _positionCalculator.CalculatePosition(_stack, item);
        StartCoroutine(GetComponent<IStackTween>().StackTween(item, endPos));

        _stack.Push(item);
    }
}
