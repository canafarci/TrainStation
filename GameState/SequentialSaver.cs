using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialSaver : MonoBehaviour, ISaveable
{
    [SerializeField] string _identifier;
    public void Load(Action onLoad)
    {
        if (_identifier == null) { return; }
        if (!PlayerPrefs.HasKey(_identifier)) { return; }
        int stage = PlayerPrefs.GetInt(_identifier);

        for (int i = 0; i < stage; i++)
            onLoad();
    }

    public void Save(int stage) => PlayerPrefs.SetInt(_identifier, stage);
}
