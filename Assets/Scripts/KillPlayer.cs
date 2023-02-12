using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform respawnPoint;
    

    // make scoreScript available to all script functions
    public ScoreCard scoreScript;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            //***** I experimented with resetting the scene, but the progress with destorying bricks is erased.
                // Scene currentScene = SceneManager.GetActiveScene();
                // SceneManager.LoadScene(currentScene.name);
            Player.transform.position = respawnPoint.position;
            // Need to zero out only the velocity since there should be no force (at least I think Unity shouldnt think there is based on oncollision being called on frame of "impact")
            Vector2 zeroVelocity = new Vector2(0, 0);
            Player.GetComponent<Rigidbody2D>().velocity = zeroVelocity;
            //Did this to add the delay
            if (scoreScript.getLives()>0)
                Invoke("restartMovement", 2.0f);
            else
            {
                Vector3 offscreenPos = new Vector3(2000.0f, 2000.0f);
                Player.transform.position = offscreenPos;
            }
        }
    }

    //Restarts the balls movement
    private void restartMovement()
    {
        //random direction for x with vector y always going down. COPIED FROM BallMovement Script
        float randX = Random.Range(-1.0f, 1.0f);
        Vector2 startForce = new Vector2(randX, -1);
        Player.GetComponent<Rigidbody2D>().velocity = startForce;
        Player.GetComponent<Rigidbody2D>().AddForce(startForce.normalized * 400f);
    }
}
