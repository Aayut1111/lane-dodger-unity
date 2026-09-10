using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float[] laneXPositions = { -3f, 0f, 3f };
    [SerializeField] private float laneChangeSpeed = 10f;

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
        position.x = Mathf.MoveTowards(position.x, _targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = position;
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