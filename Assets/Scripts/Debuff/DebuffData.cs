using UnityEngine;

namespace Debuff
{
    [CreateAssetMenu(fileName = "New Debuff Data", menuName = "Debuff Data")]
    public class DebuffData : ScriptableObject
    {
        public string title;
        public string description;
    }
}
