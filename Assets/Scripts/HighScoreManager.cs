using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI _txtHighScore;

    // Start is called before the first frame update
    void Start()
    {
        _txtHighScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _txtHighScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }
}
