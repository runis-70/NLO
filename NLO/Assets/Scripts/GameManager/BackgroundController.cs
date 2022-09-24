using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float acceleration;
    private Transform back_Tranform;
    private float back_Size;
    private float back_pos;
    private void Start()
    {
        back_Tranform = GetComponent<Transform>();
        back_Size = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void FixedUpdate()
    {
        if (speed < MaxSpeed)
            speed += acceleration * Time.fixedDeltaTime;
        back_pos += -speed * Time.deltaTime;
        back_pos = Mathf.Repeat(back_pos, back_Size);
        back_Tranform.position = new Vector3(back_pos, 0, 0);
    }
}

