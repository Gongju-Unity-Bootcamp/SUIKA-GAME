using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public NextPlanetManager nextPlanetManager;
    public GameObject[] planetPrefab;
    public int currentNumber;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float shooterSpeed = 30f;
    [SerializeField] private float shootPower = 500f;

    private float shooterRange = 4.3f;

    private void Start()
    {

    }

    private void Update()
    {
        OnMouseCursor();
        OnMouseInput();
    }

    private void OnMouseCursor()
    {
        float _horizontal = Input.GetAxis("Mouse X");
        float _speed = _horizontal * Time.deltaTime * shooterSpeed;

        transform.Translate(new Vector3(_speed, 0, 0));

        if (transform.position.x > shooterRange)
        {
            transform.position = new Vector3(shooterRange, transform.position.y, 0);
        }
        if (transform.position.x < -shooterRange)
        {
            transform.position = new Vector3(-shooterRange, transform.position.y, 0);
        }
    }

    private void OnMouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnShoot();
        }
    }

    private void OnShoot()
    {
        GameObject _planet = Instantiate(planetPrefab[currentNumber], spawnPoint.position, spawnPoint.rotation);
        _planet.tag = "Untagged";
        _planet.GetComponent<Rigidbody2D>().AddForce(_planet.transform.up * shootPower);
        nextPlanetManager.isSpriteChanged = true;
        currentNumber = UnityEngine.Random.Range(0, planetPrefab.Length);
    }
}
