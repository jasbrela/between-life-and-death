using System.Collections;
using UnityEngine;

public class LifeDeathController : MonoBehaviour
{
    private GameObject _scriptScoreController;
    
    [Header("O jogo virou txt xD")] 
    public  GameObject txtPhase;

    [Header("Sprites do player (0 - humano, 1 - fantasma)")]
    public Sprite[] playerSprite;
    
    void Awake()
    {
        _scriptScoreController = GameObject.FindWithTag("scoreManager");
    }

    void Update()
    {
        PhaseManager();
    }

    void PhaseManager()
    {
        if (PlayerStatus.GhostMode)
        {
            if (GameObject.FindWithTag("doce"))
            {
                Destroy(GameObject.FindWithTag("doce"));
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[1];
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[0];
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            if (!PlayerStatus.GhostMode)
            {
                txtPhase.SetActive(true);
                StartCoroutine("DelayPhase", 2f);
            }
            else
            {
                DontDestroyOnLoad(_scriptScoreController);
                // SceneManager.LoadScene("Game Over");
            }
        }
    }

    IEnumerator DelayPhase()
    {
        yield return new WaitForSeconds(2f);
        txtPhase.SetActive(false);
    }
}
