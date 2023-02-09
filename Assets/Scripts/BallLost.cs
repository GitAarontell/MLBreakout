using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLost : MonoBehaviour
{
    public BallCounter ballCtr;

    // Start is called before the first frame update
    void Start()
    {

        ballCtr = GameObject.FindGameObjectWithTag("BallCounter").GetComponent<BallCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Debug.Log("Score should be decreasing");
        ballCtr.decBallCtr();
    }

}
