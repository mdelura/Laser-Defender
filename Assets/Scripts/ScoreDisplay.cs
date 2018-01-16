using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    private Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<Text>();
        _scoreText.text = ScoreKeeper.FinalScore.ToString();
    }
}
