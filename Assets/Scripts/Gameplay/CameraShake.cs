using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.3f;

    private Vector3 _originalLocalPosition;

    private void Awake()
    {
        _originalLocalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        gameManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        gameManager.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            Vector2 offset = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = _originalLocalPosition + new Vector3(offset.x, offset.y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = _originalLocalPosition;
    }
}
