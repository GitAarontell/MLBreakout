using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Parts of this script and the main menu were created while following this tutorial: https://www.youtube.com/watch?v=76WOa6IU_s8

public class SceneSwap : MonoBehaviour
{

    //private int current_level = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Returns to main menu
    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Launches normal breakout
    public void PlayNormalBreakout()
    {
        SceneManager.LoadScene("Level1");
    }

    // Increment Current Level
    public void IncrementLevel()
    {
        // build index should be the scene in the order theyre created, so adding 1 should switch to the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Changes to Player vs ML version of breakout
    public void PlayMLBreakout()
    {
        
        SceneManager.LoadScene("MLVsPlayer");
        
    }
    //Quits the game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("In non-editor mode the game would've quit now");
    }
}
