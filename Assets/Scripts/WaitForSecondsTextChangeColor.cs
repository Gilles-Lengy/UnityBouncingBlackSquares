using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitForSecondsTextChangeColor : MonoBehaviour {

    public Text displayedText;

    public float waitDuration = 5;


    public byte displayedTextColorR0 = 255;
    public byte displayedTextColorG0 = 255;
    public byte displayedTextColorB0 = 255;
    public byte displayedTextColorA0 = 255;

    public byte displayedTextColorR1 = 0;
    public byte displayedTextColorG1 = 0;
    public byte displayedTextColorB1 = 0;
    public byte displayedTextColorA1 = 255;

    IEnumerator ChangeColor()
    {
        // suspend execution for "waitDuration" seconds
        yield return new WaitForSeconds(waitDuration);
        displayedText.color = new Color32(displayedTextColorR1, displayedTextColorG1, displayedTextColorB1, displayedTextColorA1);
        print("ChangeColor : " + Time.time);
        StopAllCoroutines();
    }
    IEnumerator Start()
    {
        Debug.Log("Enter start IEnum WaitForSecondsTextChangeColor");
        Time.timeScale = 1F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html // You might have to remove or change this line...
        print("Starting IEnumerator :: " + Time.time);
        displayedText.color = new Color32(displayedTextColorR0, displayedTextColorG0, displayedTextColorB0, displayedTextColorA0);
        // Start function WaitAndPrint as a coroutine
        yield return StartCoroutine("ChangeColor");
        print("Done IEnumerator" + Time.time);
    }
}
