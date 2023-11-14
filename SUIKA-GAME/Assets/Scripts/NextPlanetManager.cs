using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPlanetManager : MonoBehaviour
{
    public Sprite[] planetSprites;
    public ShooterController shooter;
    public Image preview;
    public bool isSpriteChanged = true;
    private Text flashingText;

    private void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    private void Update()
    {
        OnSpriteChange(isSpriteChanged);
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "P      ";
            yield return new WaitForSeconds(0.3f);
            
            flashingText.text = "  R    ";
            yield return new WaitForSeconds(0.3f);
            
            flashingText.text = "    E  ";
            yield return new WaitForSeconds(0.3f);
            
            flashingText.text = "      V";
            yield return new WaitForSeconds(0.3f);
            
            flashingText.text = "PREV";
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnSpriteChange(bool _isSpriteChanged)
    {
        if (_isSpriteChanged)
        {
            isSpriteChanged = false;
            preview.sprite = planetSprites[shooter.currentNumber];
        }
    }
}
