using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwap : MonoBehaviour
{
    //returns player to main menu   
    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
