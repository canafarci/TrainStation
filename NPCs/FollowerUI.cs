using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerUI : MonoBehaviour
{
    [SerializeField] GameObject _consumableUI;
    public bool ConsumableUIActive { set { _consumableUI.SetActive(value); } }
}
