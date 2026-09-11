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

    public void Init(ObstaclePool pool, GameObject prefabReference, ISpeedProvider speedProvider, float recycleZ)
    {
        _pool = pool;
        _prefabReference = prefabReference;
        _speedProvider = speedProvider;
        _recycleZ = recycleZ;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * _speedProvider.CurrentSpeed * Time.deltaTime, Space.World);

        if (transform.position.z < _recycleZ)
        {
            _pool.Return(gameObject, _prefabReference);
        }
    }
}