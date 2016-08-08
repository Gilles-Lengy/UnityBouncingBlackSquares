using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickGoToLevel : MonoBehaviour {

    public void GoToLevel(string level)
    {
        SceneManager.LoadScene(level);
        Debug.Log("Go to level " + level + " from OnClick ");
    }
}
