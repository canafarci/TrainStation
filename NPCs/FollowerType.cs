using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerType : MonoBehaviour, INPCInit
{
    public Transform Luggage { get { return _luggage; } }
    protected bool _itemNPC;
    [SerializeField] Transform _handTransformSuit, _handTransformTshirt;
    [SerializeField] GameObject _UICircle;
    Transform _luggage;

    public void InitFollowerType(int index)
    {
        Transform hand = index == 0 ? _handTransformSuit : _handTransformTshirt;
        SpawnLuggage(DecideFollowerType(), hand);
    }
    virtual protected bool DecideFollowerType()
    {
        if (Random.Range(0, 1f) <= ConstantValues.DIFFERENT_NPC_CHANCE)
            return true;
        else
            return false;
    }
    private void SpawnLuggage(bool isItemNPC, Transform trans)
    {
        if (!isItemNPC) { return; }

        GetComponent<FollowerState>().HasItem = true;

        GameObject luggage = GameManager.Instance.References.GameConfig.LuggagePrefab;
        _luggage = GameObject.Instantiate(luggage, trans).transform;
        _UICircle.SetActive(true);
    }
    public void DisableCircle() => _UICircle.SetActive(false);
}
