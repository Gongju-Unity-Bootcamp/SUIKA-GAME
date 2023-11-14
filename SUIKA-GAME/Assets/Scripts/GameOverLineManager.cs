using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLineManager : MonoBehaviour
{
    [SerializeField] private float endTime = 6f;
    [SerializeField] private float warnTime = 3f;
    [SerializeField] private float alertTime = 0.5f;

    private SpriteRenderer spriteRederer;
    private bool isChecked;
    private float overTime = 0;

    private void Start()
    {
        spriteRederer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        OnTimeSchedule(isChecked, endTime, warnTime, alertTime);
    }

    private void OnTriggerStay2D(Collider2D _hit)
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

    private void OnTimeSchedule(bool _isStabled, float _endTime, float _warnTime, float _alertTime)
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
                spriteRederer.color = new Color(1f, 0f, 0f, 1f);
            }
            else if (overTime > _alertTime)
            {
                spriteRederer.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                spriteRederer.color = new Color(0f, 0f, 0f, 0f);
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
