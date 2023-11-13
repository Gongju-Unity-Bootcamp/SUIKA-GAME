using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public GameObject planet;
    public Transform spawnPoint;
    public float tiltAngle = 30f;
    public float angleRange = 0.5f;
    public float shootPower = 1500f;

    private void Start()
    {

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
