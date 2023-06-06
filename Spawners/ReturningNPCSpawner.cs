using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningNPCSpawner : MonoBehaviour, ISpawner
{
    [SerializeField] GameObject _prefabToSpawn;
    [SerializeField] Transform _spawnTransform;
    [SerializeField] float _spawnRate;
    public IEnumerator SpawnLoop()
    {
        int returningCount = ConstantValues.PASSENGER_PER_CARRIAGE;

        while (returningCount > 0)
        {
            NavMeshNPC npc = GameObject.Instantiate(_prefabToSpawn, _spawnTransform.position, transform.rotation).GetComponent<NavMeshNPC>();
            yield return new WaitForSeconds(_spawnRate);
            returningCount--;
        }
    }
}
