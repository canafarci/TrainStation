using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SequentialUnlock : Unlocker
{
    [SerializeField] GameObject[] _sequenceToUnlock;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _activationDelay = 1f;
    IUnlockSequence _unlockSequence;

    int _currentStage;

    protected override void Awake()
    {
        base.Awake();
        _unlockSequence = GetComponent<IUnlockSequence>();
    }
    public override void Unlock()
    {
        StartCoroutine(DelayedUnlock(_activationDelay, _currentStage));
        _unlockSequence.UnlockSequence(_currentStage);
        _currentStage++;
        _saver.Save(_currentStage);

        if (_currentStage == _sequenceToUnlock.Length)
        {
            Invoke("EndUnlock", 0.1f);
        }
    }

    private void EndUnlock()
    {
        _text.text = "MAX";
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator DelayedUnlock(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        _sequenceToUnlock[index].SetActive(true);
    }
}
