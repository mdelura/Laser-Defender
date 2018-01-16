using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private static int _score;
    private Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<Text>();
        Reset();
    }

    public void Score(int points)
    {
        _score += points;
        _scoreText.text = _score.ToString();
    }

    public void Reset()
    {
        _score = 0;
        Score(0);
    }

    public static int FinalScore => _score;

}
