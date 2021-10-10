using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Nome da cena de gameplay")]
    [SerializeField] private string StartGameSceneName;
    public void StartGame()
    {
        Reset();
        SceneManager.LoadScene(StartGameSceneName);
        PlayerPrefs.SetInt("score", 0);

    }

    private static void Reset()
    {
        PlayerStatus.CurrentDebuff = Debuff.None;
        PlayerStatus.GhostMode = false;
        PlayerStatus.GameOver = false;
    }

    private bool credits;
    [Header("Painel dos creditos")]
    public GameObject panelCredits;
    public void CreditsPanel()
    {
        credits =! credits;
        if (credits)
        {
            panelCredits.SetActive(true);
        }
        else
        {
            panelCredits.SetActive(false);
        }
    }
}
