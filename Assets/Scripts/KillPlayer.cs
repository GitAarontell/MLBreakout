using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillPlayer : MonoBehaviour
{
    //public GameObject Player;
    //public Transform respawnPoint;

    // make scoreScript available to all script functions
    private ScoreCard scoreScript;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        Debug.Log("Collision occurred");
        if(collision.gameObject.name == "Ball")
        {
            Debug.Log("Player Detected");
            //***** I experimented with resetting the scene, but the progress with destorying bricks is erased.
                // Scene currentScene = SceneManager.GetActiveScene();
                // SceneManager.LoadScene(currentScene.name);
            //Player.transform.position = respawnPoint.position;
            // random direction for x with vector y always going down. COPIED FROM BallMovement Script
            float randX = Random.Range(-1.0f, 1.0f);
            Vector2 startForce = new Vector2(randX, -1);
            //Player.GetComponent<Rigidbody2D>().velocity = startForce;
            //Player.GetComponent<Rigidbody2D>().AddForce(startForce.normalized * 400f);

        }
    }
}
