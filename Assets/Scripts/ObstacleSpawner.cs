using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interface;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float StartZ = 30;
    public GameObject ObstaclePrefab;
    public int ObsctaleMinWidth = 1;
    public int MinSpaceBetweenObstacles = 2;

    private float xOffset;
    private float[] _spawnPositions;
    private float _spawnY;
    private Queue<IWave> _waves;

    private Coroutine _spawnCoroutine;

    void Awake()
    {
        _spawnPositions = new float[(int) transform.localScale.x / ObsctaleMinWidth];

        var firstSpawn = -((int) transform.localScale.x - 1) / 2f;
        for (var i = 0; i < _spawnPositions.Length; i++)
        {
            _spawnPositions[i] = firstSpawn + ObsctaleMinWidth * i;
        }

        _spawnY = 1;
        xOffset = transform.position.x;

        _waves = new Queue<IWave>();
    }

    // Use this for initialization
    void Start ()
    {
        _spawnCoroutine = StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (!GameManager.Instance.CanSpawnObstacle)
        {
            yield return new WaitForSeconds(1f);
        }

        while (true)
        {
            if (_waves.Count <= 0)
            {
                _waves.Enqueue(
                    new RandomWave(_spawnPositions,
                        (int)GameManager.Instance.MinObstaclePerWave,
                        GameManager.Instance.MaxObstaclePerWave,
                        ObsctaleMinWidth));
            }

            var wave = _waves.Dequeue();

            var obstacleTransforms = wave.GetObstacleSettings();
            foreach (var obstacleTransform in obstacleTransforms)
            {
                var obstacle = Instantiate(ObstaclePrefab, new Vector3(obstacleTransform.XPosition + xOffset, _spawnY, StartZ), Quaternion.identity);
                obstacle.transform.localScale = new Vector3(obstacleTransform.Width, 1, 1);
            }
            
            yield return new WaitForSeconds(wave.Wait());
        }
    }
    
    void Update()
    {
        if(GameManager.Instance.IsGameEnded)
            StopCoroutine(_spawnCoroutine);
    }
}
