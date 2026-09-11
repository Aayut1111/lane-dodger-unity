using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<ObstacleData> obstaclePool;
    [SerializeField] private ObstaclePool pool;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float[] laneXPositions = { -3f, 0f, 3f };
    [SerializeField] private float spawnZ = 40f;
    [SerializeField] private float recycleZ = -10f;
    [SerializeField] private float baseSpawnInterval = 1.2f;

    private readonly RngService _rng = new RngService();
    private Coroutine _spawnRoutine;

    private void OnEnable()
    {
        _spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if (_spawnRoutine != null) StopCoroutine(_spawnRoutine);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnOne();

            float speedFactor = Mathf.Max(gameManager.CurrentSpeed, 1f);
            float interval = baseSpawnInterval * (8f / speedFactor);
            yield return new WaitForSeconds(interval);
        }
    }

    private void SpawnOne()
    {
        ObstacleData chosen = _rng.GetRandomObstacle(obstaclePool);

        int lane = UnityEngine.Random.Range(0, laneXPositions.Length);
        Vector3 position = new Vector3(laneXPositions[lane], 0f, spawnZ);

        GameObject instance = pool.Get(chosen.prefab, position, Quaternion.identity);
        ObstacleMover mover = instance.GetComponent<ObstacleMover>();
        mover.Init(pool, chosen.prefab, gameManager, recycleZ, gameManager.RegisterObstaclePassed);
    }
}