using UnityEngine;
using System.Collections;

public class SetScreenOrientationLandscape : MonoBehaviour {

    void Start()
    {
        Screen.autorotateToPortrait = false;

        Screen.autorotateToPortraitUpsideDown = false;

        Screen.autorotateToLandscapeLeft = true;

        Screen.autorotateToLandscapeRight = true;

        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
