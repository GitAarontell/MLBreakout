using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KIllPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision occurred");
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            //***** I experimented with resetting the scene, but the progress with destorying bricks is erased.
                // Scene currentScene = SceneManager.GetActiveScene();
                // SceneManager.LoadScene(currentScene.name);
            Player.transform.position = respawnPoint.position;
            // random direction for x with vector y always going down. COPIED FROM BallMovement Script
            float randX = Random.Range(-1.0f, 1.0f);
            Vector2 startForce = new Vector2(randX, -1);
            Player.GetComponent<Rigidbody2D>().velocity = startForce;
            Player.GetComponent<Rigidbody2D>().AddForce(startForce.normalized * 400f);
        }
    }
}
