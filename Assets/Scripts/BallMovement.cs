using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public AudioSource source;
    // Start is called before the first frame update

    private void Awake()
    {
        // required if we do not drag in rigid body into the reference in the ballmovement script in UI
        // so this grabs the rigidbody of this object without the dragging required
        // this.rigidBody = GetComponent<Rigidbody2D>();
        this.source = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Invoke takes in string of function name and an integer representing seconds to delay
        // so this delays ball movement start by 1 seconds
        Invoke("startingForce", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startingForce()
    {
        // random direction for x with vector y always going down
        float randX = Random.Range(-1.0f, 1.0f);
        Vector2 startForce = new Vector2(randX, -1);

        this.rigidBody.AddForce(startForce.normalized * 300f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Brick")
        {
            Destroy(collision.gameObject);
            FindObjectOfType<ScoreCard>().increaseScore(1000);
        }
        this.source.Play();
        
    }
}
