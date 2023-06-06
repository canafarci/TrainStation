using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PayLoop : WaitingLoop
{
    [SerializeField] float _maxMoney;
    [SerializeField] TextMeshProUGUI _text;
    IFillable _fillable;
    ITween _tweener;
    float _currentMoney, _moneyPerStep;
    public static event Action<float> MoneyPayHandler;

    protected override void Awake()
    {
        base.Awake();

        _fillable = GetComponent<IFillable>();
        _tweener = GetComponent<ITween>();
        _currentMoney = _maxMoney;
        _moneyPerStep = _maxMoney / _maxTime * ConstantValues.WAIT_ZONES_TIME_STEP;
        FormatText((int)_maxMoney);
    }

    protected override void LoopCycle(float step, Action failCallback)
    {
        base.LoopCycle(step, failCallback);
        _fillable.SetFill(_maxTime, _maxTime - _currentTime);

        Resource resource = GameManager.Instance.Resources;

        float currentPlayerMoney = resource.Money;
        float precalculatedPlayerMoneyAfterStep = currentPlayerMoney - _moneyPerStep;
        float remainingPayMoney = _currentMoney - _moneyPerStep;

        if (remainingPayMoney <= 0)
        {
            MoneyPayHandler.Invoke(-1f * _currentMoney);
            _currentMoney = 0;
            FormatText(_currentMoney);
            _text.DOColor(Color.white, 0.01f);
            return;
        }

        if (precalculatedPlayerMoneyAfterStep < 0)
        {
            _currentMoney -= currentPlayerMoney;
            FormatText(_currentMoney);

            resource.ZeroMoney();
            GameManager.Instance.References.Money.ResetPosition();
            _text.DOColor(Color.white, 0.01f);
            failCallback();
            return;
        }

        MoneyPayHandler.Invoke(-1f * _moneyPerStep);
        _currentMoney -= _moneyPerStep;
        _text.DOColor(Color.green, 0.01f);
        FormatText(_currentMoney);
        _tweener.Tween();
    }

    public override void ResetLoop()
    {
        base.ResetLoop();
        _currentMoney = _maxMoney;
        FormatText(_maxMoney);
        GetComponent<IFillable>().SetFill(1, 0);
    }

    void FormatText(float value)
    {
        if (value >= 1000)
        {
            if (value % 1000 == 0)
                _text.text = (value / 1000).ToString("F0") + "K";
            else
                _text.text = (value / 1000).ToString("F1") + "K";
        }
        else
            _text.text = value.ToString("F0");
    }
}
