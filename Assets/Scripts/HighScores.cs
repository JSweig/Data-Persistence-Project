using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScores : MonoBehaviour
{
    public GameObject firstobj;
    public GameObject secondobj;
    public GameObject thirdobj;

     string score1;
     string score2;
     string score3;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI first = firstobj.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI second = secondobj.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI third = thirdobj.GetComponent<TextMeshProUGUI>();

        score1 = DataManager.Instance.playerNames[0] + ": " + DataManager.Instance.highScores[0];
        score2 = DataManager.Instance.playerNames[1] + ": " + DataManager.Instance.highScores[1];
        score3 = DataManager.Instance.playerNames[2] + ": " + DataManager.Instance.highScores[2];

        TextWriter.AddWriter_Static(first, score1, 0.1f, 1);
        TextWriter.AddWriter_Static(second, score2, 0.1f, 2);
        TextWriter.AddWriter_Static(third, score3, 0.1f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
