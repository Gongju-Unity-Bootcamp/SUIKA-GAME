using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private GameObject planet;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float shooterSpeed = 30f;
    [SerializeField] private float shooterRange = 4.4f;
    [SerializeField] private float shootPower = 1500f;

    private GameObject stuff;

    private void Start()
    {
        stuff = new GameObject("Planet");
    }

    private void Update()
    {
        OnMouseInput();
    }

    private void OnMouseInput()
    {
        float horizontal = Input.GetAxis("Mouse X");
        transform.Translate(new Vector3(horizontal * Time.deltaTime * shooterSpeed, 0, 0));
    }

    private void OnShoot()
    {
        GameObject _planet = Instantiate(planet, spawnPoint.position, spawnPoint.rotation);
        _planet.GetComponent<Rigidbody2D>().AddForce(_planet.transform.up * shootPower);
        _planet.transform.parent = stuff.transform;
    }
}
