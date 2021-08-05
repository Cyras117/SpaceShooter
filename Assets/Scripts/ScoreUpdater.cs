using System;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    private static int scoreAmount;
    private Text scoreText;
    // Start is called before the first frame update

    public static int getCurrentScore()
    {
        return scoreAmount;
    }
    public static void setValueToCurrentScore(int value)
    {
        scoreAmount = value;
    }
    public static void addValueToCurrentScore(int value)
    {
        scoreAmount += value;
    }
    void Start()
    {
        scoreText = GetComponent<Text>();
        setValueToCurrentScore(0);
    }

    // Update is called once per frame  
    void Update()
    {
        scoreText.text = "Score: " + scoreAmount;
    }
}
