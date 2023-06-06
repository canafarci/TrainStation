using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake() => _text = GetComponent<TextMeshProUGUI>();
    private void OnEnable() => GameManager.Instance.Resources.MoneyChangeHandler += OnMoneyChange;
    private void Start() => _text.text = GameManager.Instance.Resources.Money.ToString("F0");
    private void OnDisable() => GameManager.Instance.Resources.MoneyChangeHandler -= OnMoneyChange;
    void OnMoneyChange(float value) => _text.text = value.ToString("F0");
}
