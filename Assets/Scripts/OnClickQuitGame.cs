using UnityEngine;using System.Collections;using UnityEngine.SceneManagement;public class OnClickQuitGame : MonoBehaviour {    public void QuitGame()    {
        //Application.Quit();
        SceneManager.LoadScene("Exit");    }}