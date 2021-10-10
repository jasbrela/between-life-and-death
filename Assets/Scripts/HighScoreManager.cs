using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI txtHighScore;

    // Start is called before the first frame update
    void Start()
    {
        txtHighScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txtHighScore.text = PlayerPrefs.GetInt("highscore").ToString();
    }
}
