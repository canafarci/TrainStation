using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPCTrigger : MonoBehaviour
{
    static int ACTIVATED_COUNT;
    [SerializeField] GameObject _objectToDisable, _objectToEnable, _trigger;
    private void Awake()
    {
        ACTIVATED_COUNT = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        GetComponent<Collider>().enabled = false;
        _objectToDisable.SetActive(false);

        ACTIVATED_COUNT++;

        if (ACTIVATED_COUNT == 4)
        {
            _objectToEnable.SetActive(true);
            _trigger.SetActive(true);

        }
    }
}
