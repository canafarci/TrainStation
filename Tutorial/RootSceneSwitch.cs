using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootSceneSwitch : MonoBehaviour
{
    void Start()
    {
        int index;

        if (PlayerPrefs.HasKey("TutorialCompleted"))
        {
            //TinySauce.OnGameStarted("RootToMainLevel");
            index = 2;
            
        }
        
        else
        {
            //TinySauce.OnGameStarted("RootToTutoLevel");
            index = 1;
         
        }
            

        StartCoroutine(DelayedLoadScene(index));
    }


    IEnumerator DelayedLoadScene(int index)
    {



        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(index);

    }
}