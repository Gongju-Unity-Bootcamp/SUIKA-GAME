using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GAMEOVERText : MonoBehaviour
{
    Text flashingText;
    
    void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }
    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(.3f);
            flashingText.text = "GAME OVER";
            yield return new WaitForSeconds(.3f);
        }
    }
}
