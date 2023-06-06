using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStart : MonoBehaviour
{
    public static event Action OnGameStart;

    private void Update()
    {
        if (Input.anyKeyDown)
            StartGame();
    }

    private void StartGame()
    {
        OnGameStart?.Invoke();
        this.enabled = false;
    }
}
