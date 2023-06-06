using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

//DOtween animations for picking up StackableItems
public class StackablePickupTween : MonoBehaviour, IStackTween
{
    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Action callback = null)
    {
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 2.5f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };
        item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);

        float targetRotation = UnityEngine.Random.Range(0f, 360f);
        item.transform.DOLocalRotate(new Vector3(0, targetRotation, 0f), 0.5f);

        yield return new WaitForSeconds(0.50f);

        Sequence seq = DOTween.Sequence();
        Vector3 baseScale = item.transform.localScale;
        seq.Append(item.transform.DOScale(baseScale * 1.2f, .1f));
        seq.Append(item.transform.DOScale(baseScale, .1f));

        if (callback != null)
            seq.onComplete = () => callback();
    }


    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Vector3 endRot, Action callback = null)
    {
        throw new NotImplementedException();
    }
}
