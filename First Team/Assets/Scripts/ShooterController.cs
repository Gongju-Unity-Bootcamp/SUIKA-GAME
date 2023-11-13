using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private GameObject planet;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float tiltAngle = 30f;
    [SerializeField] private float angleRange = 0.5f;
    [SerializeField] private float shootPower = 1500f;

    private GameObject stuff;

    private void Start()
    {
        stuff = new GameObject("Planet");
    }

    private void Update()
    {
        OnKeyPress();
    }

    private void OnKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnShoot();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnLeftRotate();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            OnRightRotate();
        }
    }

    private void OnShoot()
    {
        GameObject _planet = Instantiate(planet, spawnPoint.position, spawnPoint.rotation);
        _planet.GetComponent<Rigidbody2D>().AddForce(_planet.transform.up * shootPower);
        _planet.transform.parent = stuff.transform;
    }

    private void OnLeftRotate()
    {
        if (transform.rotation.z < angleRange)
        {
            transform.Rotate(new Vector3(0, 0, tiltAngle * Time.deltaTime));
        }
    }

    private void OnRightRotate()
    {
        if (transform.rotation.z > -angleRange)
        {
            transform.Rotate(new Vector3(0, 0, -tiltAngle * Time.deltaTime));
        }
    }
}
