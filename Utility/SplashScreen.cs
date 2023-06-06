using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("TutorialComplete"))
            GameManager.Instance.SceneLoader.LoadScene(2);
        else
        {
            GameManager.Instance.SceneLoader.LoadScene(1);
        }
    }
}
