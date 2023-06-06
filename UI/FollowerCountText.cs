using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FollowerCountText : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        GameManager.Instance.References.PlayerInventory.FollowerCountChangeHandler += OnFollowerCountChange;
    }
    private void OnDisable()
    {
        GameManager.Instance.References.PlayerInventory.FollowerCountChangeHandler -= OnFollowerCountChange;
    }

    private void OnFollowerCountChange(int count)
    {
        if (count == 0)
            _text.enabled = false;
        else
        {
            _text.enabled = true;
            _text.text = count.ToString() + "/" + GameManager.Instance.References.PlayerInventory.MaxFollowerSize.ToString();
        }
    }
}
