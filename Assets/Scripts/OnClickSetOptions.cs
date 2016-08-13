using UnityEngine;using UnityEngine.SceneManagement;
public class OnClickSetOptions : MonoBehaviour {

    public void SetOption(string option)
    {

        switch (option)
        {

            case "VisualFX":
                SceneManager.LoadScene("Level_1");
                break;
            case "SoundOnOff":
                SceneManager.LoadScene("Level_1");
                break;
            default:
                GoToScene("Options");
                break;
        }
    }

    public void GoToScene(string SceneToGoTo)
    {
        Debug.Log("SceneToGoTo " + SceneToGoTo + " from SetOptions ");
        SceneManager.LoadScene(SceneToGoTo);

    }
}