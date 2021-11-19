using Player;
using TMPro;
using UnityEngine;

public class HighScoreObject : MonoBehaviour
{
    public TextMeshProUGUI txtHighScore;
    
    void Start()
    {
        txtHighScore.text = PlayerPrefs.GetInt(PlayerScore.HighScoreKey).ToString();
    }
}
