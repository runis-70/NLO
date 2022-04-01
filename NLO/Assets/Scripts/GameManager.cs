using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float speed;
    private float positionMinX;
    private Vector2 restartPosition;
    void Start()
    {
        restartPosition = transform.position;
        positionMinX = sprite.bounds.size.x * 2 - restartPosition.x;
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.x <= positionMinX)
        {
            transform.position = restartPosition;
        }
    }
}
