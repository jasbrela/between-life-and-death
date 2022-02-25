using UnityEngine;

namespace Spawners
{
    public class CloudController : SpawnableController
    {
        protected override void Move()
        {
            switch (index)
            {
                case 1:
                    transform.Translate(Vector3.left * (speed * Time.deltaTime));
                    break;
                case 0:
                    transform.Translate(Vector3.right * (speed * Time.deltaTime));
                    break;
            }
        }
    }
}