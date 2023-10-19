using UnityEngine;

public class UnitMovement
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private const float MOVE_TO_POINT_ERROR = 0.4f;
    public UnitMovement(Transform transform, Rigidbody2D rigidbody)
    {
        _transform = transform;
        _rigidbody = rigidbody;
    }

    public void Move(Vector2 inputDirection, float speed)
    {
        _rigidbody.velocity = inputDirection.normalized * speed;
    }

    public void MoveToPoint(Vector2 directionPoint, float speed)
    {
        if (Vector2.Distance((Vector2)_transform.position, directionPoint) <= MOVE_TO_POINT_ERROR)
        {
            Stop();
            return;
        }
            _rigidbody.velocity = directionPoint.normalized * speed;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
