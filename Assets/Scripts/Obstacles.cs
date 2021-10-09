using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private Position _pos;
    private int startPosIndex;
    [SerializeField] private float speed;
    void Start()
    {
        startPosIndex = Random.Range(0, _pos.positions.Length);
        
        transform.position = new Vector3(_pos.positions[startPosIndex].position.x,
            _pos.positions[startPosIndex].position.y, transform.position.z);
    }
    
    void Update()
    {
        switch (startPosIndex)
        {
            case 0:
                transform.Translate(Vector3.left * speed/3.5f * Time.deltaTime);
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
                break;
            case 1:
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                transform.localScale += Vector3.one * Time.deltaTime * 0.15f;
                break;
            case 2:
                transform.Translate(Vector3.right * speed/3.5f * Time.deltaTime);
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                transform.localScale += Vector3.one * Time.deltaTime * 0.1f;
                break;
        }
    }
    
}
