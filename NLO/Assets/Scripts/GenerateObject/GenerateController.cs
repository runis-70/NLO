using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateController : MonoBehaviour
{
    [HideInInspector][SerializeField] private protected List<string> tagObjects;
    [HideInInspector][SerializeField] private protected List<int> scoreObjects;
    private protected int lastRandomNumber = -1;
    private protected int penultimateRandomNumber = -1;

    public void GetTagObjects(ref List<string> newTagObjects) // Передача массива тегов
    {
        newTagObjects = tagObjects;
    }
    public void GetScoreObjects(ref List<int> newScoreObjects) // Передача массива очков обьектов
    {
        newScoreObjects = scoreObjects;
    }
}
