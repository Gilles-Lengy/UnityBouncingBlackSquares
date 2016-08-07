using UnityEngine;
using System.Collections;

public class SetScreenOrientationLandscape : MonoBehaviour {

    void Start()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Screen.autorotateToPortrait = false;

        Screen.autorotateToPortraitUpsideDown = false;

        Screen.autorotateToLandscapeLeft = true;

        Screen.autorotateToLandscapeRight = true;

       if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        Screen.orientation = ScreenOrientation.AutoRotation;

    }
}
