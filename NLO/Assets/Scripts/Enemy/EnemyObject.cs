using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : BaseEnemyObject
{
    private Rigidbody2D rigidbody;

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
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
        }
    }
    public void Death()
    {
        animator.SetBool("Death", true);
    }
    private void EventDeath()
    {
        Destroy(gameObject);
    }
    public void SetSpeed(float speedNew)
    {
        speed = speedNew;
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
