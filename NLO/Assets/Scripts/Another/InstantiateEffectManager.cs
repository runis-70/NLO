using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEffectManager : MonoBehaviour
{

    private void InstantiateEffect(GameObject effect)
    {
        Instantiate(effect, transform.position, transform.rotation);
    }
}
