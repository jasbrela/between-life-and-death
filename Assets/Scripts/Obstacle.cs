using UnityEngine;

public class Obstacle : BaseSpawnable
{
    private int _startPosIndex;

    new void Start()
    {
        base.Start();
        _startPosIndex = Random.Range(0, pos.positions.Length);

        if (!PlayerStatus.GhostMode && _startPosIndex == CandySpawner.Index)
        {
            switch (_startPosIndex)
            {
                case 0:
                    _startPosIndex += Random.Range(1, 3); // entre 1 e 2
                    break;
                case 1:
                    _startPosIndex += Random.Range(0, 2); // entre 0 e 1
                    break;
                case 2:
                    _startPosIndex -= Random.Range(1, 3); // entre 1 e 2
                    break;
            }
        }
        
        RandomizeSprite();
        
        transform.position = new Vector3(pos.positions[_startPosIndex].position.x,
            pos.positions[_startPosIndex].position.y, transform.position.z);
    }
    
    void Update()
    {
        Move(_startPosIndex);

        FixCollider();

        DestroySpawnable();
    }
}
