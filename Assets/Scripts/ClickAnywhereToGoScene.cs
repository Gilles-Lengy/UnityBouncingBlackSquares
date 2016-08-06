using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickAnywhereToGoScene : MonoBehaviour {

    // Variables
    public string level = "Level_1";

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0)) {
            SceneManager.LoadScene(level);
        }

    }
}
