using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    private  Rigidbody2D rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float accelaration;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (speed < MaxSpeed)
        {
            speed += accelaration * Time.fixedDeltaTime;
        }
    }
    private void Update()
    {
        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
    }
 
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetMaxSpeed()
    {
        return MaxSpeed;
    }
    public float GetAccelaration()
    {
        return accelaration; 
    }
}
