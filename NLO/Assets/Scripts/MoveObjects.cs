using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private float speed;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);
    }
}
