using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _speed = 10f;
    [SerializeField] private Transform _target;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
    }
    private void FixedUpdate()
    {
        Vector3 position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, position, _speed * Time.deltaTime);
    }
}
