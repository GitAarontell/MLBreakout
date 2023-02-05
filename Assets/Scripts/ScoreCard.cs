using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI textObj;
    public int score = 0000;
    void Start()
    {
        // D means decimal and 4 means number of digits to include
        textObj.text = 0000.ToString("D4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore(int points) 
    {
        score += points;
        textObj.text = score.ToString("D4");
    }
}
