using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float accelaration;
    private void FixedUpdate()
    {
        if (speed < MaxSpeed)
        {
            speed += accelaration * Time.fixedDeltaTime;
        }
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
    }
}
