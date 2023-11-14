using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLineManager : MonoBehaviour
{
    [SerializeField] private float endTime = 5f;
    [SerializeField] private float warnTime = 2f;

    private SpriteRenderer spriteRederer;
    private bool isChecked;
    private float overTime = 0;

    private void Start()
    {
        spriteRederer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        OnTimeSchedule(isChecked, endTime, warnTime);
    }

    private void OnTriggerEnter2D(Collider2D _hit)
    {
        if (_hit.transform.CompareTag("Planet"))
        {
            isChecked = true;
        }
    }
    private void OnTriggerExit2D(Collider2D _hit)
    {
        if (_hit.transform.CompareTag("Planet"))
        {
            isChecked = false;
        }
    }

    private void OnTimeSchedule(bool _isStabled, float _endTime, float _warnTime)
    {
        if (_isStabled)
        {
            overTime += Time.deltaTime;

            if (overTime > _endTime)
            {
                OnGameOver();
            }
            else if (overTime > _warnTime)
            {
                spriteRederer.color = Color.red;
            }
            else
            {
                spriteRederer.color = Color.white;
            }
        }
        else
        {
            overTime = 0;
        }
    }

    private void OnGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
