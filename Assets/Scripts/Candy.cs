using UnityEngine;

public class Candy : BaseSpawnable
{
    private int _index;

    private StoreManager _storeManager;

    new void Start()
    {
        base.Start();
        _storeManager = GameObject.FindWithTag("storeManager").GetComponent<StoreManager>();
    }
    
    public void Init(int index)
    {
        _index = index;

        transform.position = new Vector3(pos.positions[index].position.x,
            pos.positions[index].position.y, transform.position.z);
    }

    private void Update()
    {
        if (_storeManager.item != 2) // se nao usar o magnet
        {
            Move(_index);
        }
        
        FixCollider(false);
        DestroySpawnable();
    }
}
