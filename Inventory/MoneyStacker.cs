using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyStacker : MonoBehaviour
{
    [SerializeField] Vector3 _moneyDimensions = new Vector3(.55f, .20f, .75f);
    Vector3 _lastPos;
    Transform _player;
    Stack<Transform> _moneyStack = new Stack<Transform>();
    [SerializeField] int _rows, _columns;
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _spawnPos;
    int _currentRow, _currentColumn, _currentAisle = 0;
    public static event Action<float> MoneyPickupHandler;

    private void Awake()
    {
        _lastPos = new Vector3(_moneyDimensions.x / 2, 0, _moneyDimensions.z / 2);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void StackMoney(int count) => StartCoroutine(StackItemRoutine(count, _spawnPos));
    public void StackMoney(int count, Transform trans) => StartCoroutine(StackItemRoutine(count, trans));
    public void StartEmptyStack() => StartCoroutine(EmptyStack());
    IEnumerator StackItemRoutine(int count, Transform trans)
    {
        if (count == 0)
            yield break;

        Transform money = GameObject.Instantiate(_prefab,
                                                trans.position,
                                                _prefab.transform.localRotation,
                                                transform).transform;

        Vector3 endPos = CalculatePos(_currentRow, _currentColumn, _currentAisle);
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 2f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };

        money.transform.DOLocalPath(path, .3f, PathType.CatmullRom, PathMode.Full3D);

        _moneyStack.Push(money);
        IncreaseStackDimensions();

        yield return new WaitForSeconds(.05f);
        StackMoney(count - 1);
    }
    IEnumerator EmptyStack()
    {
        _currentRow = 0;
        _currentAisle = 0;
        _currentColumn = 0;

        Transform money;
        while (_moneyStack.TryPop(out money))
        {
            money.parent = null;
            Vector3 endPos = _player.position;
            Vector3 intermediatePos = new Vector3((endPos.x + _player.transform.position.x) / 2f, endPos.y + 2f, (endPos.z + _player.transform.position.z) / 2f);
            Vector3[] path = { intermediatePos, endPos };
            money.transform.DOPath(path, .1f, PathType.CatmullRom, PathMode.Full3D);

            yield return new WaitForSeconds(.05f);

            MoneyPickupHandler.Invoke(GameManager.Instance.References.GameConfig.MoneyPerStack);
            Destroy(money.transform.gameObject, 0.1f);
        }
    }

    Vector3 CalculatePos(int row, int column, int aisle)
    {
        return new Vector3((_moneyDimensions.x / 2) + ((row - 1) * _moneyDimensions.x),
                            _moneyDimensions.y * (aisle - 1),
                           (_moneyDimensions.z / 2) + ((column - 1) * _moneyDimensions.z)
                          );
    }

    void IncreaseStackDimensions()
    {
        if (_currentRow < _rows)
            _currentRow++;

        else if (_currentColumn < _columns)
        {
            _currentRow = 0;
            _currentColumn++;
        }
        else
        {
            _currentRow = 0;
            _currentColumn = 0;
            _currentAisle++;
        }
    }
}
