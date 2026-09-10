using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacle", menuName = "LaneDodger/Obstacle")]
public class ObstacleData : ScriptableObject
{
    public string obstacleId;
    public GameObject prefab;
    [Tooltip("Relative weight in the spawn pool — higher appears more often")]
    public int weight = 10;
}