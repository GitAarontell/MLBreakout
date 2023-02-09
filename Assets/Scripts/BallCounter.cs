using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallCounter: MonoBehaviour
{
    // Start is called before the first frame update
    public int ballCtr;
    public TMPro.TextMeshProUGUI liveCtr;

    [ContextMenu("Decrement Score")]
    public void decBallCtr()
    {
        if (ballCtr > 0)
            ballCtr--;
        // Need to add condition to trigger game over here
        liveCtr.text = "Lives - " + ballCtr.ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
