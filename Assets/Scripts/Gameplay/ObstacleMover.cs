using System;
using UnityEngine;

public interface ISpeedProvider
{
    float CurrentSpeed { get; }
}

public class ObstacleMover : MonoBehaviour
{
    private ISpeedProvider _speedProvider;
    private float _recycleZ;
    private ObstaclePool _pool;
    private GameObject _prefabReference;
    private Action _onPassed;

    public void Init(ObstaclePool pool, GameObject prefabReference, ISpeedProvider speedProvider, float recycleZ, Action onPassed)
    {
        _pool = pool;
        _prefabReference = prefabReference;
        _speedProvider = speedProvider;
        _recycleZ = recycleZ;
        _onPassed = onPassed;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * _speedProvider.CurrentSpeed * Time.deltaTime, Space.World);

        if (transform.position.z < _recycleZ)
        {
            _onPassed?.Invoke();
            _pool.Return(gameObject, _prefabReference);
        }
    }
}