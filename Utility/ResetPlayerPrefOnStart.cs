using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefOnStart : MonoBehaviour
{
    [SerializeField] bool _clearPrefs;

    private void Start()
    {
        if (!_clearPrefs) { return; }

        PlayerPrefs.DeleteAll();
    }
}
