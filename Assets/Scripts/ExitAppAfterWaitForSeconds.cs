﻿using UnityEngine;
using System.Collections;

public class ExitAppAfterWaitForSeconds : MonoBehaviour {

    public float waitDuration = 3;

    IEnumerator Start()
    {
        print("Starting Exit" + Time.time);
        yield return StartCoroutine(WaitAndExit(waitDuration));

        print("Done Exit App" + Time.time);
    }
    IEnumerator WaitAndExit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("Exit App " + Time.time);
        Application.Quit();
    }
}
