using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyObject : MonoBehaviour
{
    [SerializeField] private protected int score;
    public int GetScore()
    {
        return score;
    }
}
