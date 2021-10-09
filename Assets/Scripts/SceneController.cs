using UnityEngine;

public class SceneController : MonoBehaviour
{
    [Header("Nome da cena de gameplay")]
    [SerializeField] private string StartGameSceneName;
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(StartGameSceneName);
    }

    private bool credits = false;
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
