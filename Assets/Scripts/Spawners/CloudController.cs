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
                    transform.Translate(Vector3.left * minSpeed * Time.deltaTime);
                    break;
                case 0:
                    transform.Translate(Vector3.right * minSpeed * Time.deltaTime);
                    break;
            }
        }
    }
}