using System.Collections.Generic;
using ArmySystem;
using ArmySystem.SummoningSystem;
using InputSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string SUMMONER_DATA_PATH = "SummonerData";
        
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private MinionsArmy _minionsArmy;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private SummonButton[] _summonButtons;

        private MinionSummoner _minionSummoner;
        private SummonerDataSO _summonerDataSO;
        private UnitsPool<Minion> _minionPool;
        
        private void Awake()
        {
            //-SO-
            _summonerDataSO = Resources.Load<SummonerDataSO>(SUMMONER_DATA_PATH);
            Dictionary<KeyCode, MinionType> minionTypesByKeyCode = new Dictionary<KeyCode, MinionType>();
            foreach (var minionByKeyCode in _summonerDataSO.Minions)
            {
                minionTypesByKeyCode.Add(minionByKeyCode.Key,minionByKeyCode.MinionType);
            }
            Dictionary<MinionType, Minion> minionPrefabsByType = new Dictionary<MinionType, Minion>();
            foreach (var prefabsByType in _summonerDataSO.Minions)
            {
                minionPrefabsByType.Add(prefabsByType.MinionType,prefabsByType.Prefab);
            }
            //--
            
            _minionPool = new UnitsPool<Minion>(minionPrefabsByType);
            _minionSummoner = new MinionSummoner(_minionPool,_minionsArmy,_summonerDataSO,_spawnPosition);
            
            _inputListener.Construct(_minionSummoner,minionTypesByKeyCode);
            
            foreach (var button in _summonButtons)
            {
                button.Construct(_minionSummoner);
            }
        }
    }
}
