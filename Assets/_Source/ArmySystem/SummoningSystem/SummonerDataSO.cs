using UnityEngine;

namespace ArmySystem
{
    [CreateAssetMenu(menuName = "SummonerData", fileName = "SO/SummonerData")]
    public class SummonerDataSO: ScriptableObject
    {
        [field: SerializeField] public MinionSummonData[] Minions { get; private set; }
    }
}