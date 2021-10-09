using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    [SerializeField] private Position pos;
    [SerializeField] private float speed = 2;
    private GameObject _player;
    private int _index;
    
    public void Init(int index)
    {
        this._index = index;
        _player = GameObject.FindGameObjectWithTag("Player");

        transform.position = new Vector3(pos.positions[index].position.x,
            pos.positions[index].position.y, transform.position.z);
    }

    private void Update()
    {
        switch (_index)
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
            GetComponent<CircleCollider2D>().enabled = false;

        }

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
