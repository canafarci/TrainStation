using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float Money { get { return currentMoney; } }
    public event Action<float> MoneyChangeHandler;
    float currentMoney;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", GameManager.Instance.References.GameConfig.StartMoney);
            currentMoney = PlayerPrefs.GetInt("Money");
        }
        else if (PlayerPrefs.GetInt("TutorialCompleted") == 1)
        {
            PlayerPrefs.SetInt("Money", GameManager.Instance.References.GameConfig.StartMoney);
            currentMoney = PlayerPrefs.GetInt("Money");
            PlayerPrefs.SetInt("TutorialCompleted", 2);
        }
        else
            currentMoney = PlayerPrefs.GetInt("Money");
    }
    private void OnEnable()
    {
        MoneyStacker.MoneyPickupHandler += OnMoneyChange;
        UnlockLoop.MoneyPayHandler += OnMoneyChange;
    }
    private void OnDisable()
    {
        MoneyStacker.MoneyPickupHandler -= OnMoneyChange;
        UnlockLoop.MoneyPayHandler -= OnMoneyChange;
    }
    public void ZeroMoney()
    {
        PlayerPrefs.SetInt("Money", 0);
        currentMoney = 0;
        MoneyChangeHandler?.Invoke(currentMoney);
    }
    void OnMoneyChange(float change)
    {
        currentMoney += change;
        PlayerPrefs.SetInt("Money", (int)currentMoney);
        MoneyChangeHandler?.Invoke(currentMoney);
    }
}
