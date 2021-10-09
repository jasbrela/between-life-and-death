using UnityEngine;

public class Obstacle : BaseSpawnable
{
    private int _startPosIndex;

    new void Start()
    {
        base.Start();
        _startPosIndex = Random.Range(0, pos.positions.Length);
        
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
