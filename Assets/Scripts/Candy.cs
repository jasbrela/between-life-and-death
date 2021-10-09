using UnityEngine;

public class Candy : BaseSpawnable
{
    private int _index;
    
    public void Init(int index)
    {
        _index = index;

        transform.position = new Vector3(pos.positions[index].position.x,
            pos.positions[index].position.y, transform.position.z);
    }

    private void Update()
    {
        Move(_index);
        FixCollider(false);
        DestroySpawnable();
    }

}
