using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interface;
using UnityEngine;

public class MenuObstacleSpawner : MonoBehaviour {

    public float StartZ = 30;
    public GameObject ObstaclePrefab;
    public int ObsctaleMinWidth = 1;
    public int MinSpaceBetweenObstacles = 2;

    public float ObstacleLifespan = 2f;

    private float xOffset;
    private float[] _spawnPositions;
    private float _spawnY;
    private Queue<IWave> _waves;

    void Awake()
    {
        _spawnPositions = new float[(int)transform.localScale.x / ObsctaleMinWidth];

        var firstSpawn = -((int)transform.localScale.x - 1) / 2f;
        for (var i = 0; i < _spawnPositions.Length; i++)
        {
            _spawnPositions[i] = firstSpawn + ObsctaleMinWidth * i;
        }

        _spawnY = 1;
        xOffset = transform.position.x;

        _waves = new Queue<IWave>();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            if (_waves.Count <= 0)
            {
                _waves.Enqueue(new RandomWave(_spawnPositions, 1, _spawnPositions.Length / 2, ObsctaleMinWidth));
            }

            var wave = _waves.Dequeue();

            var obstacleTransforms = wave.GetObstacleSettings();
            foreach (var obstacleTransform in obstacleTransforms)
            {
                var obstacle = Instantiate(ObstaclePrefab,
                    new Vector3(obstacleTransform.XPosition + xOffset, _spawnY, StartZ), Quaternion.identity);
                obstacle.transform.localScale = new Vector3(obstacleTransform.Width, 1, 1);
                Destroy(obstacle, ObstacleLifespan);
            }

            yield return new WaitForSeconds(wave.Wait());
        }
    }
}
