using UnityEngine;using UnityEngine.SceneManagement;using UnityEngine.UI;public class OnClickSetOptions : MonoBehaviour {    private Text textSoundOnOff;
    private Text textVisualFxOnOff;

    private string playerPrefsSoundOnOff;
    private string playerPrefsVisualFXOnOff;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start OnClickSetOptions");
        textSoundOnOff = GameObject.Find("TextSoundOnOff").GetComponent<Text>();
        textVisualFxOnOff = GameObject.Find("TextVisualFxOnOff").GetComponent<Text>();
        playerPrefsSoundOnOff = PlayerPrefs.GetString("soundOnOff","ON");
        playerPrefsVisualFXOnOff = PlayerPrefs.GetString("visualFxOnOff", "ON");
        textSoundOnOff.text = playerPrefsSoundOnOff;
        textVisualFxOnOff.text = playerPrefsVisualFXOnOff;
    }    public void SetOption(string option)    {        switch (option)        {
            case "Sound":
                playerPrefsSoundOnOff = PlayerPrefs.GetString("soundOnOff", "ONF");                if (playerPrefsSoundOnOff == "OFF")
                {
                    textSoundOnOff.text = "ON";
                    PlayerPrefs.SetString("soundOnOff", "ON");
                }
                else
                {
                    textSoundOnOff.text = "OFF";
                    PlayerPrefs.SetString("soundOnOff", "OFF");
                }                break;            case "VisualFX":                playerPrefsVisualFXOnOff = PlayerPrefs.GetString("visualFxOnOff", "ONF");                if (playerPrefsVisualFXOnOff == "OFF") {
                    textVisualFxOnOff.text = "ON";
                    PlayerPrefs.SetString("visualFxOnOff", "ON");
                } else {
                    textVisualFxOnOff.text = "OFF";
                    PlayerPrefs.SetString("visualFxOnOff", "OFF");
                }                break;            default:                GoToScene("Options");                break;        }    }    public void GoToScene(string SceneToGoTo)    {        Debug.Log("SceneToGoTo " + SceneToGoTo + " from SetOptions ");        SceneManager.LoadScene(SceneToGoTo);    }}