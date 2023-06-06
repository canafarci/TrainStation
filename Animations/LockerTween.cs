using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LockerTween : MonoBehaviour, IStackTween
{
    public Transform LockerTransform;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] Transform _lockerDoor;
    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Action callback = null)
    {
        item.transform.parent = null;

        Vector3 tarPos = _spawnPoint.transform.position;

        Vector3 targetRotation = new Vector3(0, UnityEngine.Random.Range(0f, 360f), 0f);
        yield return StartCoroutine(MoveTween(item.gameObject, tarPos, Vector3.one * 0.1f, targetRotation));
        Destroy(item.gameObject);
    }

    public IEnumerator GetLuggage(Transform hand)
    {
        GameObject luggage = GameManager.Instance.References.GameConfig.LuggagePrefab;
        GameObject item = GameObject.Instantiate(luggage, _spawnPoint.position, _spawnPoint.rotation);

        item.transform.parent = hand;
        item.transform.localScale = Vector3.one * 0.0001f;

        Vector3 endPos = new Vector3(-0.316000015f, 1.12399995f, 0.166999996f);
        Vector3 targetScale = new Vector3(0.589721501f, 0.589721501f, 0.589721501f);
        Vector3 targetRotation = new Vector3(337.397095f, 71.3773193f, 196.341583f);

        yield return StartCoroutine(MoveTween(item, endPos, targetScale, targetRotation));
    }

    IEnumerator MoveTween(GameObject item, Vector3 endPos, Vector3 targetScale, Vector3 targetRotation, Action callback = null)
    {
        Vector3 startPos = item.transform.localPosition;
        Vector3 intermediatePos = new Vector3((endPos.x + startPos.x) / 2f, endPos.y + 2f, (endPos.z + startPos.z) / 2f);
        Vector3[] path = { intermediatePos, endPos };

        item.transform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Full3D);
        item.transform.DOScale(targetScale, 1f);
        item.transform.DOLocalRotate(targetRotation, 1f);

        _lockerDoor.DOLocalRotate(new Vector3(0f, 45f, 0f), 0.2f);
        yield return new WaitForSeconds(.51f);
        _lockerDoor.DOLocalRotate(Vector3.zero, 0.2f);
    }
    public IEnumerator StackTween(StackableItem item, Vector3 endPos, Vector3 endRot, Action callback = null)
    {
        throw new System.NotImplementedException();
    }
}
