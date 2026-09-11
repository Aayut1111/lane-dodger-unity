using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    private readonly Dictionary<GameObject, Queue<GameObject>> _pools = new();

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!_pools.TryGetValue(prefab, out var queue))
        {
            queue = new Queue<GameObject>();
            _pools[prefab] = queue;
        }

        GameObject instance;
        if (queue.Count > 0)
        {
            instance = queue.Dequeue();
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.SetActive(true);
        }
        else
        {
            instance = Instantiate(prefab, position, rotation);
        }

        return instance;
    }

    public void Return(GameObject instance, GameObject prefab)
    {
        instance.SetActive(false);
        if (!_pools.TryGetValue(prefab, out var queue))
        {
            queue = new Queue<GameObject>();
            _pools[prefab] = queue;
        }
        queue.Enqueue(instance);
    }
}