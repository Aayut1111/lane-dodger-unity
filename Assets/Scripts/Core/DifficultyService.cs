using UnityEngine;

public class DifficultyService
{
    private readonly AnimationCurve _speedCurve;
    private readonly float _rampTime;

    public DifficultyService(AnimationCurve speedCurve, float rampTime)
    {
        _speedCurve = speedCurve;
        _rampTime = rampTime;
    }

    public float GetSpeedMultiplier(float elapsedTime)
    {
        float t = Mathf.Clamp01(elapsedTime / _rampTime);
        return _speedCurve.Evaluate(t);
    }
}