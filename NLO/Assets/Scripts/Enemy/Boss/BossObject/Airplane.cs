using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : BossObject
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(OneShotIE());
        }       
    }
    private IEnumerator OneShotIE()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator TwoShotIE()
    {
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator ThreeShotIE()
    {
        yield return new WaitForSeconds(1f);
    }
}
