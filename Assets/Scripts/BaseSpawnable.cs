using UnityEngine;

public abstract class BaseSpawnable : MonoBehaviour
{
    [SerializeField] protected Position pos;
    [SerializeField] protected float speed = 2f;
    private GameObject Player;
    
    protected void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    protected void Move(int index)
    {
        if (!PlayerStatus.GameOver)
        {
            switch (index)
            {
                case 0:
                    transform.Translate(Vector3.left * speed / 3.5f * Time.deltaTime);
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    transform.localScale += Vector3.one * Time.deltaTime * 0.08f;
                    break;
                case 1:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    transform.localScale += Vector3.one * Time.deltaTime * 0.15f;
                    break;
                case 2:
                    transform.Translate(Vector3.right * speed / 3.5f * Time.deltaTime);
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    transform.localScale += Vector3.one * Time.deltaTime * 0.08f;
                    break;
            }
        }
    }
    
    protected void FixCollider(bool isBox = true)
    {
        if (transform.position.y < Player.transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "SpawnableFront";
            if (isBox)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
    
    protected void DestroySpawnable()
    {
        if (transform.position.y < -6f || transform.position.x < -5f || transform.position.x > 5f)
        {
            Destroy(gameObject);
        }
    }
}
