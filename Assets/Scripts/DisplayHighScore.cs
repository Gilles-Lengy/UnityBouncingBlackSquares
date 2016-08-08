﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour {

    public Text allTimeHighText;
    private int allTimeHighScore;

    // Use this for initialization
    void Start () {

        allTimeHighScore = PlayerPrefs.GetInt("allTimeHighScore", allTimeHighScore);
        allTimeHighText.text = allTimeHighScore.ToString();

    }
	

}
