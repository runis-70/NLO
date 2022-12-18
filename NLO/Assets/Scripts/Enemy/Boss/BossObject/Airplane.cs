using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Airplane : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bullet;
    private BossObject bossObject;

    private void Start()
    {
        bossObject = GetComponent<BossObject>();
        StartCoroutine(OneShotIE());
    }
    private void Update()
    {
        var dir = bossObject.GetPlayerTransform().position - firePoint.position;
        var euler = firePoint.eulerAngles;
        euler.z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firePoint.eulerAngles = euler;
    }
    private IEnumerator OneShotIE()
    {
        Bullet newBullet = bullet;
        newBullet.target = bossObject.GetPlayerTransform().position;
        Instantiate(newBullet, firePoint.position, bullet.transform.rotation);
        yield return new WaitForSeconds(1f);
        StartCoroutine(OneShotIE());
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

