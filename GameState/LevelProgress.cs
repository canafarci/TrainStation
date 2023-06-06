using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    List<TrainState> _states = new List<TrainState>();
    StarTween _tweener;
    int _levelProgressValue, _currentLevel;
    public event Action<int> LevelIncreaseHandler;
    private void Awake()
    {
        _tweener = GetComponent<StarTween>();
    }
    public void AddStateToList(TrainState state)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) { return; }

        _states.Add(state);
        state.TrainTakeoffHandler += OnLevelProgress;
    }

    private void OnDisable()
    {
        foreach (TrainState state in _states)
        {
            state.TrainTakeoffHandler -= OnLevelProgress;
        }
    }
    private void Start() => InitValues();
    private void OnLevelProgress() => _tweener.OnLevelProgress(() => ProgressLevel());
    public void ProgressLevel()
    {
        _levelProgressValue += 1;
        PlayerPrefs.SetInt("LevelProgressValue", _levelProgressValue);

        if (_levelProgressValue % 10 == 0)
            OnLevelIncrease();

        _tweener.UpdateUI(_currentLevel, _levelProgressValue);
    }
    private void OnLevelIncrease()
    {
        //TinySauce.OnGameFinished(false, GameManager.Instance.Resources.Money, "LEVEL" + _currentLevel.ToString());
        _levelProgressValue = 1;
        _currentLevel += 1;
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        LevelIncreaseHandler?.Invoke(_currentLevel);
        //TinySauce.OnGameStarted("LEVEL" + _currentLevel.ToString());
    }
    void InitValues()
    {
        if (PlayerPrefs.HasKey("LevelProgressValue"))
            _levelProgressValue = PlayerPrefs.GetInt("LevelProgressValue");
        else
        {
            _levelProgressValue = 1;
            PlayerPrefs.SetInt("LevelProgressValue", _levelProgressValue);
        }

        if (PlayerPrefs.HasKey("CurrentLevel"))
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        else
        {
            _currentLevel = 1;
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        }

        //TinySauce.OnGameStarted("LEVEL" + _currentLevel.ToString());
        _tweener.UpdateUI(_currentLevel, _levelProgressValue);
        LevelIncreaseHandler?.Invoke(_currentLevel);
    }

    private void OnApplicationQuit()
    {
        //TinySauce.OnGameFinished(false, GameManager.Instance.Resources.Money, "LEVEL" + _currentLevel.ToString());
    }
}
