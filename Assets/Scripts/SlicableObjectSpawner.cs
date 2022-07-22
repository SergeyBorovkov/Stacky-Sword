using System.Collections.Generic;
using UnityEngine;

public class SlicableObjectSpawner : MonoBehaviour
{
    [SerializeField] private PreslicedObject _preslicedPrefab;    
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<int> _counts;
    [SerializeField] private float _distanceOnZ;
    [SerializeField] private bool IsColorChanging;
    [SerializeField] private List<Material> _randomColorMaterials;
    
    private Material _last;
    private List<Vector3> _spawnPositions = new List<Vector3>();
    
    public IReadOnlyList<Vector3> SpawnPositions => _spawnPositions;          

    private void Awake()
    {
        if (IsColorChanging == false)        
            _randomColorMaterials = null;        

        Spawn();        
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            for (int j = 0; j < _counts[i]; j++)
            {
                var newSpawnPosition = new Vector3(_spawnPoints[i].position.x, _spawnPoints[i].position.y, _spawnPoints[i].position.z + j * _distanceOnZ);

                _spawnPositions.Add(newSpawnPosition);

                var newPreslicedObject = Instantiate(_preslicedPrefab, newSpawnPosition, Quaternion.identity, transform);

                if (IsColorChanging)
                    newPreslicedObject.ChangeMaterial(GetNonrepeatingRandomMaterial());                
            }            
        }
    }

    private Material GetNonrepeatingRandomMaterial()
    {
        int randomIndex = Random.Range(0, _randomColorMaterials.Count);

        Material material = _randomColorMaterials[randomIndex];

        if (material == _last)
        {
            if (randomIndex == _randomColorMaterials.Count - 1)
            {
                material = _randomColorMaterials[0];
            }
            else
            {
                material = _randomColorMaterials[randomIndex + 1];
            }
        }

        _last = material;

        return material;
    }
}