using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningNPC : NavMeshNPC, INPCInit
{
    [SerializeField] Transform _tshirtHand, _suitHand;
    Transform _handTransform;

    public void InitFollowerType(int index) => _handTransform = index == 0 ? _suitHand : _tshirtHand;
    private void Start()
    {
        if (IsItemNPC())
            StartCoroutine(GetLuggageAndExit());
        else
            StartCoroutine(ExitAndDestroy());
    }


    IEnumerator ExitAndDestroy()
    {
        Transform exitNode = FindObjectOfType<Nodes>().ExitNode;
        yield return StartCoroutine(GetToPosCoroutine(exitNode.position));
        Destroy(gameObject, 0.2f);
    }

    IEnumerator GetLuggageAndExit()
    {
        LockerTween tweener = FindObjectOfType<LockerTween>();
        yield return StartCoroutine(GetToPosCoroutine(tweener.LockerTransform.position));
        yield return StartCoroutine(tweener.GetLuggage(_handTransform));
        StartCoroutine(ExitAndDestroy());
    }

    bool IsItemNPC()
    {
        if (Random.Range(0, 1f) <= ConstantValues.DIFFERENT_NPC_CHANCE)
            return true;
        else
            return false;
    }


}
