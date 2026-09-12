using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float[] laneXPositions = { -3f, 0f, 3f };
    [SerializeField] private float laneChangeSpeed = 10f;
    [SerializeField] private float maxLeanAngle = 15f;
    [SerializeField] private float leanSmoothing = 8f;

    private int _currentLane = 1;
    private float _targetX;

    public event Action OnPlayerHit;

    private void Awake()
    {
        _targetX = laneXPositions[_currentLane];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(1);
        }

        Vector3 position = transform.position;
        float previousX = position.x;
        position.x = Mathf.MoveTowards(position.x, _targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = position;

        float horizontalDelta = position.x - previousX;
        float targetLean = Mathf.Clamp(-horizontalDelta * 40f, -maxLeanAngle, maxLeanAngle);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetLean);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, leanSmoothing * Time.deltaTime);
    }

    private void MoveLane(int direction)
    {
        int newLane = Mathf.Clamp(_currentLane + direction, 0, laneXPositions.Length - 1);
        _currentLane = newLane;
        _targetX = laneXPositions[_currentLane];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnPlayerHit?.Invoke();
        }
    }
}