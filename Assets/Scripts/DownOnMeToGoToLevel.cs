using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DownOnMeToGoToLevel : MonoBehaviour
{
   public string level = "Level_1";

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown on me");
        Time.timeScale = 1F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html
        SceneManager.LoadScene(level);
        Debug.Log("On Mouse Down click anywhere");
    }
}
