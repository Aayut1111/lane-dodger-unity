using UnityEngine;

public class ScoreService
{
    public int CalculateScore(float distanceTraveled)
    {
        return Mathf.FloorToInt(distanceTraveled);
    }
}
