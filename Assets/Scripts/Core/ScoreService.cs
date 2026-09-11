public class ScoreService
{
    public int CalculateScore(int obstaclesPassed, int pointsPerObstacle = 10)
    {
        return obstaclesPassed * pointsPerObstacle;
    }
}