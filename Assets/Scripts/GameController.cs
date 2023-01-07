using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField]
    private TextMeshProUGUI gameRuslt;
    [SerializeField]
    private TextMeshProUGUI pointsText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        gameRuslt.text = "";
        SetPoints(0);
        GameStarted = true;

        Field.Instance.GenerateField();
    }

    public void Win()
    {
        GameStarted = false;
        gameRuslt.text = "You Win!";
    }

    public void Lose()
    {
        GameStarted = false;
        gameRuslt.text = "You Lose!";
    }
    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    public void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }
}
