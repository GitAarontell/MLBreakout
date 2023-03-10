using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MLScoreCard : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI livesText;
    private int score = 0000;
    public int lives = 3;
    public GameObject gameOver;
    public GameObject ball;
    public GameObject gameWon;
    public GameObject bricksLevel2;
    public mlBallMovementVsPlayer mlBallMovementScript;
    public PlayerBallMovement playerBallMovementScript;
    public GameObject bricksLevel1;
    public TMP_Text levelName;
    // blue is (0.13, 0.69, 0.90, 1.00)
    // 
    Dictionary<string, int> colors = new Dictionary<string, int>() {
        { "RGBA(0.129, 0.691, 0.898, 1.000)", 1000},
        { "RGBA(0.054, 0.513, 0.887, 1.000)", 2000},
        { "RGBA(0.621, 0.363, 0.906, 1.000)", 3000},
        { "RGBA(0.945, 0.475, 0.881, 1.000)", 4000},
        { "RGBA(0.943, 0.476, 0.476, 1.000)", 5000}
    };
    void Start()
    {
        // D means decimal and 4 means number of digits to include
        scoreText.text = 0000.ToString("D4");
        // set lives to lives variable at start of game
        livesText.text = "Lives - " + this.lives.ToString();
        //setting sceneSwap

    }

    // called by 
    public void increaseScore(SpriteRenderer obj) 
    {

        // get the rgba values from obj.color, then specify which values with .r, .g, .b, .a, then convert them to float values the thousands, turn them to string
        // and use dictionary to add points that way
        string convertedColor = string.Format("RGBA({0}, {1}, {2}, {3})", obj.color.r.ToString("0.000"), obj.color.g.ToString("0.000"), obj.color.b.ToString("0.000"), obj.color.a.ToString("0.000"));
        // update score
        score += colors[convertedColor];
        // update text component to show new score
        scoreText.text = score.ToString("D4");


        //checking for change in game state
        if (score == 135000)
        {
            mlBallMovementScript.killBall();
            bricksLevel1.SetActive(false);
            bricksLevel2.SetActive(true);
            levelName.text = "ML Level 2";
            //mlBallMovementScript.InvokeMLStartMovement();
        }
        if (score == 90000 + 135000)
        {
         
            gameWon.SetActive(true);
            playerBallMovementScript.endGame();
        }


    }

    // public method called to decrease lives, and set if game over screen should be displayed. Also teleports the ball offscreen
    public void decreaseLives() {
        lives -= 1;
        if (lives == 0)
        {
            // commented out for training ml agent
            gameOver.SetActive(true);
            playerBallMovementScript.endGame();
        }
        livesText.text = "Lives - " + lives.ToString();
    }
    //getter for lives
    public int getLives()
    {
        return lives;
    }

}
