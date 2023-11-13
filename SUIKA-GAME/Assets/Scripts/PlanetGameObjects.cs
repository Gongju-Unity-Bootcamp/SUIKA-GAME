using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGameObjects : MonoBehaviour
{
    public List<GameObject> planetPrefab;
    public int planetPrefabCount;

    private void Start()
    {
        planetPrefabCount = planetPrefab.Count;
    }
}
