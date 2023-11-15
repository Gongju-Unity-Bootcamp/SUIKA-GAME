using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public int level; // 레벨 변수지정
    public bool isMerged; // ismerged (행성을 합쳐졌는지 확인하기위해) 불리언으로 Y or N 
    public GameObject nextLevelObject; // 다음 행성을 호출하기 위해 변수생성

    private bool isTagged; // isTagged를 Y or N

    private void Start()
    {
        
    }

    private void Update()
    {
        OnMerge(isMerged);
    }

    private void OnCollisionEnter2D(Collision2D _hit) // rigidbody2D가 다른 rigidbody2D에 충돌하면 호출한다.
    {
        if (!isTagged) // isTagged == false
        {
            gameObject.tag = "Planet"; // 본인태그를 Planet로 바꾼다.
            isTagged = true; 
        }

        if (isMerged == false && _hit.collider.CompareTag("Planet")) 
        {
            Planet _other = _hit.gameObject.GetComponent<Planet>();
            Vector3 _otherPosition = _other.transform.position;
            Quaternion _otherRotation = _other.transform.rotation;

            if (nextLevelObject != null && _other.isMerged == false && _other.level == level)
            {
                _other.isMerged = isMerged = true;
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
