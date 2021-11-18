using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI txtHighScore;
    
    void Start()
    {
        txtHighScore.text = PlayerPrefs.GetInt(PlayerScore.HighScoreKey).ToString();
    }
}
