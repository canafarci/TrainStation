using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CapacityUI : MonoBehaviour
{
    TextMeshProUGUI _text;
    int _maxSize;
    int _activeNPCs = 0;
    QueueSize _qSize;
    NPCSeatingArea _seatingArea;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _qSize = FindObjectOfType<QueueSize>();
        _seatingArea = FindObjectOfType<NPCSeatingArea>();
    }
    private void OnEnable()
    {
        _qSize.ActiveNPCCountChangeHandler += OnNPCCountChange;
        _seatingArea.MaxSizeChangeHandler += OnMaxSizeChange;
    }
    private void OnDisable()
    {
        _qSize.ActiveNPCCountChangeHandler -= OnNPCCountChange;
        _seatingArea.MaxSizeChangeHandler -= OnMaxSizeChange;
    }
    private void UpdateText()
    {
        _text.text = _activeNPCs.ToString() + "/" + _maxSize.ToString();
    }

    private void OnMaxSizeChange(int maxSize)
    {
        _maxSize = maxSize;
        UpdateText();
    }
    private void OnNPCCountChange(int count)
    {
        _activeNPCs = count;
        UpdateText();
    }
}
