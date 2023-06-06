using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyTween : MonoBehaviour, ITween
{
    int _counter;
    public void Tween()
    {
        _counter++;
        if (_counter < 3) { return; }

        Vector3 playerPos = GameManager.Instance.References.PlayerInventory.transform.position;
        Transform trans = GameManager.Instance.References.Money.transform;
        trans.position = playerPos;

        Vector3[] path = new Vector3[] { playerPos,
                                        (playerPos + transform.position) / 2f + new Vector3(0f, 1f, 0f),
                                        transform.position
                                        };
        Tween tween = trans.DOPath(path, ConstantValues.WAIT_ZONES_TIME_STEP * 4f);

        tween.onComplete = () => GameManager.Instance.References.Money.ResetPosition();
        _counter = 0;

    }
}
