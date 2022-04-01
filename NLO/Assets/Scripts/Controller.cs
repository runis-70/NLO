using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject BlueRay;
    [SerializeField] private GameObject RedRay;
    [SerializeField] private float secondRay;
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Mine")
                {
                    StartCoroutine(OnBlueRayDown());
                }
                else
                {
                    StartCoroutine(OnRedRayDown());
                }
            }
        }
    }
    private IEnumerator OnRedRayDown()
    {
        RedRay.SetActive(true);
        yield return new WaitForSeconds(secondRay);
        RedRay.SetActive(false);
    }
    private IEnumerator OnBlueRayDown()
    {
        BlueRay.SetActive(true);
        yield return new WaitForSeconds(secondRay);
        BlueRay.SetActive(false);
    }
}
