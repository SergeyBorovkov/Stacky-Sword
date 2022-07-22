using System.Collections.Generic;
using UnityEngine;

public class ScorePointShower : MonoBehaviour
{
    [SerializeField] private ScorePoint _prefab;

    private List<ScorePoint> _scorePointsPool;
    private int _poolCount = 100;

    private void Start()
    {
        _scorePointsPool = new List<ScorePoint>();

        for (int i = 0; i < _poolCount; i++)
        {
            var newScorePoint = Instantiate(_prefab, transform);

            newScorePoint.Deactivate();

            _scorePointsPool.Add(newScorePoint);
        }
    }

    public void Show(Vector3 position)
    {
        foreach (var scorePoint in _scorePointsPool)
        {
            if (scorePoint.IsActive == false)
            {
                scorePoint.Activate(position);
                return;
            }
        }
    }
}