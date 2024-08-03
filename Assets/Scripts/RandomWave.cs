using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Interface;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWave : IWave
{
    private readonly List<float> _spawnXPositions;
    private readonly int _minCount;
    private readonly int _maxCount;
    private readonly int _minWidth;
    
    public RandomWave(float[] spawnXPositions, int minCount, int maxCount, int minWidth)
    {
        _spawnXPositions = spawnXPositions.ToList();
        _minCount = minCount;
        _maxCount = maxCount;
        _minWidth = minWidth;
    }

    public List<ObstacleTransform> GetObstacleSettings()
    {
        var obstacleCount = Random.Range(_minCount, _maxCount);

        var xPositions = new List<float>();

        while (xPositions.Count < obstacleCount && _spawnXPositions.Any())
        {
            var index = Random.Range(0, _spawnXPositions.Count - 1);
            xPositions.Add(_spawnXPositions[index]);
            _spawnXPositions.RemoveAt(index);
        }

        xPositions.Sort();

        var obstacleTransforms = new List<ObstacleTransform>();
        foreach (var x in xPositions)
        {
            var o = obstacleTransforms.FirstOrDefault(_ => Math.Abs(_.XPosition + _.Width - x) < 0.2);
            if (o == null)
            {
                obstacleTransforms.Add(new ObstacleTransform(x, _minWidth));
            }
            else
            {
                o.Width += _minWidth;
            }
        }

        foreach (var obstacleTransform in obstacleTransforms)
        {
            obstacleTransform.CorrectPosition();
        }

        return obstacleTransforms;
    }

    public float Wait()
    {
        return 0.7f;
    }
}
