using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private float speed;
    [SerializeField] private int score;
    [SerializeField] private float speedFixed;
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
    public void SetSpeedFixed(float speedNew)
    {
        speed = speedNew;
    }
    public float GetSpeed()
    {
        return speedFixed;
    }
    public float GetMaxSpeed()
    {
        return MaxSpeed;
    }
    public float GetAccelaration()
    {
        return accelaration;
    }
    public int GetScore()
    {
        return score;
    }
}
