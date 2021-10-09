using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private Position pos;
    private int _startPosIndex;
    [SerializeField] private float speed;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _startPosIndex = Random.Range(0, pos.positions.Length);
        
        transform.position = new Vector3(pos.positions[_startPosIndex].position.x,
            pos.positions[_startPosIndex].position.y, transform.position.z);
    }
    
    void Update()
    {
        switch (_startPosIndex)
        {
            case 0:
                transform.Translate(Vector3.left * speed/3.5f * Time.deltaTime);
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                transform.localScale += Vector3.one * Time.deltaTime * 0.08f;
                break;
            case 1:
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                transform.localScale += Vector3.one * Time.deltaTime * 0.15f;
                break;
            case 2:
                transform.Translate(Vector3.right * speed/3.5f * Time.deltaTime);
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                transform.localScale += Vector3.one * Time.deltaTime * 0.08f;
                break;
        }

        if (transform.position.y < _player.transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "ObstacleFront";
            GetComponent<BoxCollider2D>().enabled = false;

        }

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
    
}
