using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackItemTween : MonoBehaviour, IStackTween
{
    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Action callback = null)
    {
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };
        item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);

        Vector3 baseScale = item.transform.localScale;
        item.transform.localScale = Vector3.one * 0.0001f;
        item.transform.DOScale(baseScale, 0.5f);

        item.transform.DOLocalRotate(Vector3.zero, 0.5f);

        yield return new WaitForSeconds(0.51f);

        Sequence seq = DOTween.Sequence();
        Vector3 endBaseScale = item.transform.localScale;
        seq.Append(item.transform.DOScale(endBaseScale * 1.2f, .1f));
        seq.Append(item.transform.DOScale(endBaseScale, .1f));
    }

    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Vector3 endRot, Action callback = null)
    {
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };
        item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);

        Vector3 baseScale = item.transform.localScale;
        item.transform.localScale = Vector3.one * 0.0001f;
        item.transform.DOScale(baseScale, 0.5f);

        item.transform.DOLocalRotate(endRot, 0.5f);

        yield return new WaitForSeconds(0.51f);

        Sequence seq = DOTween.Sequence();
        Vector3 endBaseScale = item.transform.localScale;
        seq.Append(item.transform.DOScale(endBaseScale * 1.2f, .1f));
        seq.Append(item.transform.DOScale(endBaseScale, .1f));
    }
}
