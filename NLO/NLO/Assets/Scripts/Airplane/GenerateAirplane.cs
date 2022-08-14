using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAirplane : MonoBehaviour
{
    public GameObject airplane;
    [Header("Настройка скорости")]
    [Header("Враг")]
    [SerializeField] private float accelerationAirplane;
    [SerializeField] private float MaxSpeedAirplane;
    [SerializeField] private float speedAirplane;
}
