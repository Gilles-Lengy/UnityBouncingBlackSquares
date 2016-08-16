using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerGameHandler : MonoBehaviour
{

    public float moveSpeed = 0.1f;
    public float squareGravityScale = 1.3f;


    public float timeLeft = 300.0f;

    public Text scoreText;
    public Text instructionsText;
    public Text congratulationText;

    public byte scoreTextColorR0 = 0;
    public byte scoreTextColorG0 = 0;
    public byte scoreTextColorB0 = 0;
    public byte scoreTextColorA0 = 18;

    public byte scoreTextColorR1 = 234;
    public byte scoreTextColorG1 = 173;
    public byte scoreTextColorB1 = 0;
    public byte scoreTextColorA1 = 255;

    public GameObject gameObjectTodrag; //refer to GO that being dragged





    private Vector3 mousePosition;
    private int gameState;// 0 = Place the big square, 1 = Squares are bouncing, 3 = Displaying the score
    private GameObject[] topBouncingSquares;
    private GameObject[] bottomBouncingSquares;
    private int bigBlackSquareOnMouse;// On the big black square :  0 = no mouse event, 1 OnMouseDown, 2 OnMouseUp
    private int allTimeHighScore;
    private int sessionHighScore;
    private int congratulationTextDisplay;// 0 nothing, 1 new session high score, 3 new all time high score
    private int score;
    private string countDownString;
    private string playerPrefsSoundOnOff;
    private string playerPrefsVisualFXOnOff;

    private float minutes;
    private float seconds;

    public string level = "Level_1";



    // Use this for initialization
    void Start()
    {
        Debug.Log("Start !!!!");
        Time.timeScale = 1F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html
        GetComponent<SpriteRenderer>().color = Color.black;
        gameState = 0;
        score = -777;
        congratulationTextDisplay = 0;
        setScoretext();
        congratulationText.color = new Color32(scoreTextColorR1, scoreTextColorG1, scoreTextColorB1, 0);
        bigBlackSquareOnMouse = 0;
        allTimeHighScore = PlayerPrefs.GetInt("allTimeHighScore", allTimeHighScore);
        sessionHighScore = PlayerPrefs.GetInt("sessionHighScore", sessionHighScore);
        playerPrefsSoundOnOff = PlayerPrefs.GetString("soundOnOff", "ON");
        playerPrefsVisualFXOnOff = PlayerPrefs.GetString("visualFxOnOff", "ON");
        if (playerPrefsSoundOnOff == "ON")
        {
            AudioListener.volume = 1;
        }
        else {
            AudioListener.volume = 0;
        }

    }

    public void startTimer(float from)
    {

        timeLeft = from;
        Update();
        StartCoroutine(updateCoroutine());
    }

    void Update()
    {
        //instructionsText.text = gameState.ToString();
        Debug.Log("Game state : " + gameState.ToString());



        // Handled the launch of the little squares when the big black square cease to be touched
        if ((bigBlackSquareOnMouse == 0 || bigBlackSquareOnMouse == 0) && gameState == 0) { }
        foreach (Touch touch in Input.touches)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);// Changed to conserv 2d stuff related like rigidbody2d

            if (hit2D.collider != null && hit2D.collider.gameObject.tag == "Player")
            {
                Debug.Log("hit2D Player");

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && bigBlackSquareOnMouse == 0)
                {
                    Debug.Log("hit2D Player Began");
                    bigBlackSquareOnMouse = 1;

                }
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && bigBlackSquareOnMouse == 1)
                {
                    Debug.Log("hit2D Player Ended");
                    bigBlackSquareOnMouse = 2;
                }


            }
        }

        // Set the Gravity Scale of the little squares and the, the funny part of the game starts !!!
        if (bigBlackSquareOnMouse == 2 && gameState == 0)
        {

            Debug.Log("Let's BOUNCE !!!!");
            
                GetComponent<AudioSource>().Play();

            Destroy(GetComponent<DragingScript>());// Destroy the script DragingScript attached to the big black square
            instructionsText.color = new Color32(0, 0, 0, 0);
            score = 0;

            topBouncingSquares = GameObject.FindGameObjectsWithTag("BouncingSquareTop");// Si on le fait dans le start, ça marche pas...
            int maxTop = topBouncingSquares.Length;
            for (int i = 0; i < maxTop; i++)
            {

                topBouncingSquares[i].GetComponent<Rigidbody2D>().gravityScale = squareGravityScale;
                Debug.Log("topBouncingSquares scale gravity");

            }
            bottomBouncingSquares = GameObject.FindGameObjectsWithTag("BouncingSquareBottom");// Si on le fait dans le start, ça marche pas...
            int maxBottom = bottomBouncingSquares.Length;
            for (int i = 0; i < maxBottom; i++)
            {

                bottomBouncingSquares[i].GetComponent<Rigidbody2D>().gravityScale = -squareGravityScale;
                Debug.Log("bottomBouncingSquares scale gravity");

            }
            Debug.Log("Après set scale gravity");
            gameState = 1;

        }

        if (gameState == 1)
        {
            timeLeft -= Time.deltaTime;

            minutes = Mathf.Floor(timeLeft / 60);// /60 pour une minute
            seconds = timeLeft % 60;// /60 pour une minute

            Debug.Log("aprés minutes et secondes");


            if (seconds > 59)
            {
                seconds = 59;
            }

            if (minutes < 0)
            {

                Debug.Log("game state 1 et minute<0");
                gameState = 3;
                minutes = 0;
                seconds = 0;

                setScoretext();

                instructionsText.text = "Tap on the top of the screen to play again \n";
                instructionsText.text += "Tap on the bottom of the screen to go to the main menu";
                instructionsText.color = new Color32(scoreTextColorR1, scoreTextColorG1, scoreTextColorB1, scoreTextColorA1);





                Time.timeScale = 0.0F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html

                StopAllCoroutines();



            }
            // instructionsText.text = "after if minutes < 0";
        }
        if (gameState == 3)
        {
            Debug.Log("game state 3");
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("game state 3 et button up" + Input.mousePosition.y);
                if (Input.mousePosition.y >= Screen.height / 2)
                {
                    Debug.Log("game state 3 et mpy>screen / 2");
                    Time.timeScale = 1F;// http://docs.unity3d.com/ScriptReference/Time-timeScale.html
                    SceneManager.LoadScene(level);
                }
                else if (Input.mousePosition.y < Screen.height / 2)
                {
                    Debug.Log("game state 3 et mpy else");
                    SceneManager.LoadScene("MainMenu");
                    //Application.Quit();
                }
            }
        };
    }

    /*************************************** 
     *  Change the color of the player
     *  Launch the flash FX 
     *  Scoring as well
     * ************************************/

    void OnCollisionEnter2D(Collision2D collission)
    {
        if (collission.gameObject.tag == "BouncingSquareTop" || collission.gameObject.tag == "BouncingSquareBottom")
        {

            if (gameState == 1)
            {
                if(playerPrefsVisualFXOnOff == "ON") { 
                FlashFX(true);
                }                

                    GetComponent<AudioSource>().Play();

                score = score + 1;
                setScoretext();
                Debug.Log(score);
                startTimer(timeLeft);

            }

        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            if (gameState == 1)
            {

                    collission.gameObject.GetComponent<AudioSource>().Play();

                score = score - 3;
                setScoretext();
                Debug.Log(score);
            }
        }
    }
    void OnCollisionExit2D(Collision2D collission)
    {
        FlashFX(false);
    }

    void setScoretext()
    {
        Debug.Log("Début setScoretext");

        if (gameState == 3)
        {
            scoreText.color = new Color32(scoreTextColorR1, scoreTextColorG1, scoreTextColorB1, scoreTextColorA1);
            if (score > sessionHighScore)
            {
                sessionHighScore = score;
                PlayerPrefs.SetInt("sessionHighScore", sessionHighScore);
                congratulationTextDisplay = 1;
            }
            if (score > allTimeHighScore)
            {
                allTimeHighScore = score;
                PlayerPrefs.SetInt("allTimeHighScore", allTimeHighScore);
                congratulationTextDisplay = 2;
            }

            if (congratulationTextDisplay > 0) {
                congratulationText.color = new Color32(scoreTextColorR1, scoreTextColorG1, scoreTextColorB1, scoreTextColorA1);
            }

            switch (congratulationTextDisplay)
            {
                case 1:
                    congratulationText.text = "New Session High Score !!!";
                    break;
                case 2:
                    congratulationText.text = "New All Time High Score !!!";
                    break;
                case 0:
                default:
                    congratulationText.text = "";
                    break;
            }
        }

    

        if (score != -777) { scoreText.text = score.ToString() + '/' + countDownString; }

        else
        {
            scoreText.text = "";
        }

        Debug.Log("Fin setScoretext");
    }

    /*****************************
    * Flash FX
    ****************************/

    private void FlashFX(bool fxState = false)
    {
        GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);

    scoreText.color = new Color32((fxState ? scoreTextColorR1 : scoreTextColorR0), (fxState ? scoreTextColorG1 : scoreTextColorG0), (fxState ? scoreTextColorB1 : scoreTextColorB0), (fxState ? scoreTextColorA1 : scoreTextColorA0));

        Camera.main.backgroundColor = (fxState ? Color.black : Color.white);

        int maxTop = topBouncingSquares.Length;
        /*
        for (int i = 0; i < maxTop; i++)
        {

            topBouncingSquares[i].GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);

        }*/
        foreach (GameObject littleSquare in topBouncingSquares)
        {
            littleSquare.GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);
        }

        foreach (GameObject littleSquare in bottomBouncingSquares)
        {
            littleSquare.GetComponent<SpriteRenderer>().color = (fxState ? Color.white : Color.black);
        }

    }

    /*****************************
     * Timer
     * ***************************/



    private IEnumerator updateCoroutine()
    {
        while (gameState == 1)
        {
            countDownString = string.Format("{0:0}:{1:00}", minutes, seconds);
            setScoretext();
            yield return new WaitForSeconds(0.2f);
        }
        if (gameState == 3)
        {
            StopAllCoroutines();
        }
    }
}
