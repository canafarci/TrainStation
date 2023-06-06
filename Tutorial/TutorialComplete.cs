using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComplete : MonoBehaviour
{
    TrainState _state;

    private void Awake()
    {
        _state = FindObjectOfType<TrainState>();
    }
    private void OnEnable()
    {
        _state.TrainTakeoffHandler += OnTrainTakeoff;
    }
    private void Start()
    {
        //TinySauce.OnGameStarted("TutorialScene");
    }
    private void OnDisable()
    {
        _state.TrainTakeoffHandler -= OnTrainTakeoff;
    }

    private void OnTrainTakeoff()
    {
        GameManager.Instance.SceneLoader.FadedLoadScene(2, 5f);
        PlayerPrefs.SetInt("TutorialCompleted", 1);
       // TinySauce.OnGameFinished(true, GameManager.Instance.Resources.Money, "TutorialScene");
    }
    private void OnApplicationQuit()
    {
       // TinySauce.OnGameFinished(false, GameManager.Instance.Resources.Money, "TutorialScene");
    }
}
