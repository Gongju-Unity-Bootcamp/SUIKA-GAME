using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private PlanetGameObjects planetList;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float shooterSpeed = 30f;
    [SerializeField] private float shootPower = 1500f;
    [SerializeField] private GameObject planetStorage;

    private float shooterRange = 4.3f;
    private int planetListCount;

    private void Start()
    {
        planetListCount = planetList.planetPrefabCount;
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
        int _randomNumber = UnityEngine.Random.Range(0, planetListCount);
        GameObject _planet = Instantiate(planetList.planetPrefab[_randomNumber], spawnPoint.position, spawnPoint.rotation);
        _planet.GetComponent<Rigidbody2D>().AddForce(_planet.transform.up * shootPower);
        _planet.transform.parent = planetStorage.transform;
    }
}
