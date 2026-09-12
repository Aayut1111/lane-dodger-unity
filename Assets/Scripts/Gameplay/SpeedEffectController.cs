using UnityEngine;

public class SpeedEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedParticles;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float minEmissionRate = 5f;
    [SerializeField] private float maxEmissionRate = 60f;
    [SerializeField] private float maxExpectedSpeed = 20f;

    private ParticleSystem.EmissionModule _emission;

    private void Awake()
    {
        _emission = speedParticles.emission;
    }

    private void Update()
    {
        float speedFraction = Mathf.Clamp01(gameManager.CurrentSpeed / maxExpectedSpeed);
        float rate = Mathf.Lerp(minEmissionRate, maxEmissionRate, speedFraction);
        _emission.rateOverTime = rate;
    }
}
