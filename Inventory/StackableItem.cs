using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItem : MonoBehaviour
{
    public float ItemHeight;
    Collider _collider;
    public Enums.StackableItemType ItemType;
    private void Awake() => _collider = GetComponent<Collider>();
}
