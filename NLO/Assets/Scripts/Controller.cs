using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject BlueRay;
    [SerializeField] private GameObject RedRay;
    [SerializeField] private float secondRay;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(OnBlueRayDown());
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit2D hit;
            Debug.DrawRay(transform.position, transform.forward * 100f);
            if (Physics.Raycast(ray, out hit))
            {
                RaycastHit2D hit;
                if (hit.collider != null)
                {
                    print(hit.transform.gameObject.name);
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
