using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] private List<Garbage> _garbagePrefabs;
    [SerializeField] private SlicableObjectSpawner _basketSpawner;
    [SerializeField] private int _minCount;
    [SerializeField] private int _maxCount;
        
    private void Start()
    {
        RandomSpawn();        
    }

    private void RandomSpawn()
    {
        foreach (var spawnerPosition in _basketSpawner.SpawnPositions)
        {
            foreach (var garbage in _garbagePrefabs)
            {
                var count = Random.Range(_minCount, _maxCount);

                for (int i = 0; i < count; i++)
                {
                    Instantiate(garbage, spawnerPosition, Quaternion.identity, transform);                    
                }
            }
        }
    }
}