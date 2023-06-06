using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] _objectsToSwitchActivation;
    [SerializeField] float _disableDuration;
    [SerializeField] bool _disablePlayer;
    InputReader _reader;
    private void Awake()
    {
        _reader = FindObjectOfType<InputReader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        foreach (GameObject go in _objectsToSwitchActivation)
            go.SetActive(!go.activeSelf);


        GetComponent<Collider>().enabled = false;
        if (_disablePlayer)
            StartCoroutine(DisableControl());
    }

    IEnumerator DisableControl()
    {
        _reader.Disable();
        yield return new WaitForSeconds(_disableDuration);
        _reader.Enable();
    }
}
