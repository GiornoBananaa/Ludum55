using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    public class MinionSummoner
    {
        private Dictionary<MinionType, int> _summoningClicks;
        private UnitsPool<Minion> _unitsPool;
        private Dictionary<MinionType, MinionSummonData> _minionSummoningData;
        private MinionsArmy _minionsArmy;
        private Transform _spawnPosition;
        
        public Action<Minion> OnMinionSummoned;
        
        public MinionSummoner(UnitsPool<Minion> unitsPool, MinionsArmy minionsArmy, SummonerDataSO minionSummoningDataSo, Transform spawnPosition)
        {
            _unitsPool = unitsPool;
            _minionsArmy = minionsArmy;
            _spawnPosition = spawnPosition;
            _summoningClicks = new Dictionary<MinionType, int>();
            _minionSummoningData = new Dictionary<MinionType, MinionSummonData>();
            foreach (var minionData in minionSummoningDataSo.Minions)
            {
                _minionSummoningData.Add(minionData.MinionType,minionData);
                _summoningClicks.Add(minionData.MinionType, 0);
            }
        }
        
        public bool AddSummoningClick(MinionType minionType)
        {
            MinionSummonData summoningData = _minionSummoningData[minionType];
            _summoningClicks[minionType] += 1;
            if (_summoningClicks[minionType] >= summoningData.ClicksCountForSummon)
            {
                Minion newMinion = _unitsPool.GetObject(minionType);
                newMinion.transform.position = _spawnPosition.position;
                _minionsArmy.AddMinion(newMinion);
                OnMinionSummoned?.Invoke(newMinion);
                _summoningClicks[minionType] = 0;
                return true;
            }
            return false;
        } 
    }
}
