using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    private Rigidbody2D _rigidbody;

    private Joystick _joystick;


    public float Speed { get; private set; }

    public Transform Transform => transform;

    [Inject]
    public void Construct(Joystick joystick, PlayerConfig playerConfig)
    {
        _joystick = joystick;
        Speed = playerConfig.Speed;
    }


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _rigidbody.velocity = new Vector2(_joystick.Horizontal, _joystick.Vertical).normalized * Speed;
    }
}
