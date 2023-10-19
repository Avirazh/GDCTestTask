using UnityEngine;

public class UnitMovement
{
    private Transform _transform;
    private Rigidbody2D _rigidbody;
   
    public UnitMovement(Transform transform, Rigidbody2D rigidbody)
    {
        _transform = transform;
        _rigidbody = rigidbody;
    }
    public void Move(Vector2 inputDirection, float speed)
    {
        _rigidbody.velocity =  inputDirection.normalized * speed;
    }
}
