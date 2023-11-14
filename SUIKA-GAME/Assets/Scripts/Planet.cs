using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int level;
    public bool isMerged;
    public GameObject nextLevelObject;

    private void Start()
    {
        
    }

    private void Update()
    {
        OnMerge(isMerged);
    }

    private void OnCollisionStay2D(Collision2D _hit)
    {
        if (_hit.collider.CompareTag("Planet"))
        {
            Planet _other = _hit.gameObject.GetComponent<Planet>();
            Vector3 _otherPosition = _other.transform.position;
            Quaternion _otherRotation = _other.transform.rotation;

            if (nextLevelObject != null && _other.isMerged == false && _other.level == level)
            {
                _other.isMerged = isMerged = true;
                Destroy(_other.gameObject);
                GameObject _nextLevelObject = Instantiate(_other.nextLevelObject, _otherPosition, _otherRotation);
            }
        }
    }

    private void OnMerge(bool _isMerged)
    {
        if (_isMerged)
        {
            Destroy(gameObject);
        }
    }
}
