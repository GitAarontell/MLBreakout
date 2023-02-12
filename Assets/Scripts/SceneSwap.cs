using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Parts of this script and the main menu were created while following this tutorial: https://www.youtube.com/watch?v=76WOa6IU_s8

public class SceneSwap : MonoBehaviour
{

    private int current_level = 1;

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
        if (current_level == 3)
        {
            SceneManager.LoadScene("Menu");
        }
        SceneManager.LoadScene("Level"+current_level.ToString());
    }

    // Increment Current Level
    public void IncrementLevel()
    {
        current_level += 1;
    }

    // Placeholder Function, will need to be implemented when we figure out ML
    public void PlayMLBreakout()
    {

    }
    //Quits the game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("In non-editor mode the game would've quit now");
    }
}
