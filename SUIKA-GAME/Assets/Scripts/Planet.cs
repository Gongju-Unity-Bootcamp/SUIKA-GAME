using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int level = 1;
    public bool isMerged = false;

    [SerializeField] private PlanetGameObjects planetList;
    [SerializeField] private float destroyTime = 1.0f;
    [SerializeField] private GameObject planetStorage;

    private int planetListCount;

    private void Start()
    {
        planetList = GameObject.Find("PlanetList").GetComponent<PlanetGameObjects>();
        planetStorage = GameObject.Find("Planet");
        planetListCount = planetList.planetPrefabCount;
    }

    private void Update()
    {
        OnMerge(isMerged);
    }

    private void OnCollisionEnter2D(Collision2D _hit)
    {
        if (_hit.collider.CompareTag("Planet"))
        {
            Planet _other = _hit.gameObject.GetComponent<Planet>();

            if (_other.level == level)
            {
                _other.isMerged = true;
                isMerged = true;

                for (int index = 0; index < planetListCount; ++index)
                {
                    if (planetList.planetPrefab[index].GetComponent<Planet>().level == level + 1)
                    {
                        GameObject _planet = Instantiate(planetList.planetPrefab[index], transform.position, transform.rotation);
                        _planet.transform.parent = planetStorage.transform;
                        break;
                    }
                }
            }
        }
    }

    private void OnMerge(bool _isMerged)
    {
        if (_isMerged)
        {
            Destroy(gameObject, destroyTime);
        }
    }
}
