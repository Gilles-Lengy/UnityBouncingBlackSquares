using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour {

    private Text highScoreText;
    public int highScore;

    // Use this for initialization
    void Start () {
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        highScoreText.text = highScore.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
