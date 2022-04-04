using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float endX;
    private Vector2 startPos;
    private void FixedUpdate()
    {
        if (speed < MaxSpeed)
        {
            speed += acceleration * Time.fixedDeltaTime;
        }
    }
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x <= endX )
        {
            transform.position = startPos;
        }
    }
 
}
