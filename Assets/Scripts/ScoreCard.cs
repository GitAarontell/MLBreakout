using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI textObj;
    public int score = 0000;
    // blue is (0.13, 0.69, 0.90, 1.00)
    // 
    Dictionary<string, int> colors = new Dictionary<string, int>() {
        { "RGBA(0.129, 0.691, 0.898, 1.000)", 1000},
        { "RGBA(0.054, 0.513, 0.887, 1.000)", 2000},
        { "RGBA(0.621, 0.363, 0.906, 1.000)", 3000},
        { "RGBA(0.945, 0.475, 0.881, 1.000)", 4000},
        { "RGBA(0.943, 0.476, 0.476, 1.000)", 5000}
    };
    void Start()
    {
        // D means decimal and 4 means number of digits to include
        textObj.text = 0000.ToString("D4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // called by 
    public void increaseScore(SpriteRenderer obj) 
    {

        // get the rgba values from obj.color, then specify which values with .r, .g, .b, .a, then convert them to float values the thousands, turn them to string
        // and use dictionary to add points that way
        string convertedColor = string.Format("RGBA({0}, {1}, {2}, {3})", obj.color.r.ToString("0.000"), obj.color.g.ToString("0.000"), obj.color.b.ToString("0.000"), obj.color.a.ToString("0.000"));
        // update score
        score += colors[convertedColor];
        // update text component to show new score
        textObj.text = score.ToString("D4");
        
    }
}
