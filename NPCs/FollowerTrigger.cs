using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowerTrigger : MonoBehaviour
{
    Follower _follower;
    FollowerState _state;
    FollowerUI _ui;
    int _sceneIndex;

    private void Awake()
    {
        _follower = GetComponent<Follower>();
        _state = GetComponent<FollowerState>();
        _ui = GetComponent<FollowerUI>();
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    //add make follower follower Player after the trigger condition
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_state.FollowingPlayer) { return; }
            if (!_state.HasBeenAcceptedInside) { return; }
            if (_state.HasItem) { return; }
            if (!_state.IsSeated) { return; }

            Inventory inventory = GameManager.Instance.References.PlayerInventory;
            if (inventory.ItemCount != 0) { return; }
            if (inventory.FollowerCount >= inventory.MaxFollowerSize) { return; }
            if (_sceneIndex == 0 && !_state.IsSeated) { return; }

            FindObjectOfType<NPCSeatingArea>().Dequeue(_follower);
            _state.IsSeated = false;
            _state.FollowingPlayer = true;
            _ui.ConsumableUIActive = false;
            _follower.FollowPlayer();
        }
    }
}