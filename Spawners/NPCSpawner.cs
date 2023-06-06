using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawn NPC by checking the size limit
public class NPCSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] NPCWaitQueue _queue;
    [SerializeField] GameObject _prefabToSpawn;
    [SerializeField] float _spawnRate;
    public bool TaxiInPlace = false;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    //continiously check and spawn NPCs if there is room avaliable
    public void OnTaxiInPlace()
    {


        TaxiInPlace = true;

    }
    public void OnTaxiLeave()
    {

        TaxiInPlace = false;

    }
    public IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (!_queue.QueueIsFull && TaxiInPlace)
            {
                NavMeshNPC npc = GameObject.Instantiate(_prefabToSpawn, transform.position, transform.rotation).GetComponent<NavMeshNPC>();
                _queue.AddToQueue(npc);
            }
            yield return new WaitForSeconds(_spawnRate);
        }
    }

}
