using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public bool planetCursor;
    private GameObject planet;

    private void Start()
    {
        
    }

    private void Update()
    {
        OnCheckCursor();
    }

    private void OnCheckCursor()
    {
        if (planet != null && planet.GetComponent<PlanetController>().stopState)
        {
            planet.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Planet"))
        {
            planet = hit.gameObject;
            Debug.Log(hit.gameObject);
            planetCursor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Planet"))
        {
            planet = null;
            planetCursor = false;
        }
    }
}
