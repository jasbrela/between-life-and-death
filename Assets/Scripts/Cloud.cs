using UnityEngine;
using Random = UnityEngine.Random;

public class Cloud : BaseSpawnable
{
    [SerializeField] private Sprite[] _sprites;
    private int _startPosIndex;

    private new void Start()
    {
        base.Start();
        
        GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length)];
        _startPosIndex = Random.Range(0, pos.positions.Length);
        
        transform.position = new Vector3(pos.positions[_startPosIndex].position.x,
            RandomizeYPos(), transform.position.z);
    }

    private void Update()
    {
        Move();
        FixCollider();
        DestroySpawnable();
    }

    private void Move()
    {
        switch (_startPosIndex)
        {
            case 1:
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
            case 0:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
        }
    }

    private float RandomizeYPos()
    {
        return pos.positions[_startPosIndex].position.y + Random.Range(-1.5f, 1.0f);
    }
}
