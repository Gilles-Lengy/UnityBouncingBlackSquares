﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour {

    public Text allTimeHighText;
    public Text sessionHighText;
    private int allTimeHighScore;
    private int sessionHighScore;

    // Use this for initialization
    void Start () {

        allTimeHighScore = PlayerPrefs.GetInt("allTimeHighScore", allTimeHighScore);
        allTimeHighText.text = allTimeHighScore.ToString();

        sessionHighScore = PlayerPrefs.GetInt("sessionHighScore", sessionHighScore);
        sessionHighText.text = sessionHighScore.ToString();

    }
	

}