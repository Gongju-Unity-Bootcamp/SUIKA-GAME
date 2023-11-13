using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public bool stopState;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.collider.CompareTag("Attach") || hit.collider.CompareTag("Planet"))
        {
            OnAttach(true);
        }
    }

    private void OnAttach(bool isAttached)
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        stopState = true;
    }
}
