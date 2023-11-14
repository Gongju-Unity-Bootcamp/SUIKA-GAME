using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLineManager : MonoBehaviour
{
    private SpriteRenderer spriteRederer;
    private float overTime;


    private void Start()
    {
        spriteRederer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        Debug.Log(overTime);
    }

    private void OnTriggerStay2D(Collider2D _hit)
    {
        if (_hit.transform.CompareTag("Planet"))
        {
            overTime += Time.deltaTime;

            if (overTime > 5f)
            {
                OnGameOver(true);
            }
            if (overTime > 2f)
            {
                spriteRederer.color = Color.red;
            }

            Debug.Log("호출");
        }
    }
    private void OnTriggerExit2D(Collider2D _hit)
    {
        if (_hit.transform.CompareTag("Planet"))
        {
            overTime = 0;
            spriteRederer.color = Color.white;
            Debug.Log("해제");
        }
    }

    private void OnGameOver(bool _isOvered)
    {
        if (_isOvered)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
