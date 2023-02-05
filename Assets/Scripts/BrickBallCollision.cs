using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BrickBallCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // collison information found at https://docs.unity3d.com/Manual/CollidersOverview.html
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // print(collision.contacts);
        if (collision.gameObject.name == "Ball")
        {
            this.gameObject.SetActive(false);
        }
    }
}
