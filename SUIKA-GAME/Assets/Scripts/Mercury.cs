using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercury : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.01f); 
    }
}
