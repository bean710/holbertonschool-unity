using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public GameObject TextGO;
    public GameObject LevelNumGO;
    public GameObject GameOverGO;

    private static Text text;
    private static Text levelNumText;
    private static Text gameOverText;
    private static int totalPoints = 0;
    private static int levelNo = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = TextGO.GetComponent<Text>();
        levelNumText = LevelNumGO.GetComponent<Text>();
        gameOverText = GameOverGO.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddPoints(int points)
    {
        totalPoints += points;
        text.text = $"Score: {totalPoints}";
    }

    public static void IncLevel()
    {
        levelNo += 1;
        levelNumText.text = $"Level: {levelNo}";
    }

    public static void GameOver()
    {
        gameOverText.enabled = true;
    }
}
