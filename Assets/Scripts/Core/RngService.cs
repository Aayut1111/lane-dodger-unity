using System;
using System.Collections.Generic;

public class RngService
{
    private readonly Random _random = new Random();

    public ObstacleData GetRandomObstacle(List<ObstacleData> pool)
    {
        int totalWeight = 0;
        foreach (var obstacle in pool)
        {
            totalWeight += obstacle.weight;
        }

        int roll = _random.Next(0, totalWeight);
        int cumulative = 0;

        foreach (var obstacle in pool)
        {
            cumulative += obstacle.weight;
            if (roll < cumulative)
            {
                return obstacle;
            }
        }

        // fallback safety net — should only trigger on a weight/roll edge case,
        // but guarantees this never silently returns null
        return pool[pool.Count - 1];
    }
}