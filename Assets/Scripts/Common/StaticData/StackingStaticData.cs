using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "StackingStaticData", menuName = "Static Data/StackingStaticData")]
    public class StackingStaticData : ScriptableObject
    {
        [field: SerializeField, Range(0.1f, 10.0f)] public float TimeBtwInteractivity { get; private set; }
    }
}