using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyObject : MonoBehaviour
{
    private protected Animator animator;
    [SerializeField] private protected int score;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public int GetScore()
    {
        return score;
    }
}
