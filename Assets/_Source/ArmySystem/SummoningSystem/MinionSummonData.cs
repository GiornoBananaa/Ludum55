using System;
using UnityEngine;

namespace ArmySystem
{
    [Serializable]
    public class MinionSummonData
    {
        public MinionType MinionType;
        public KeyCode Key;
        public Minion Prefab;
        public int ClicksCountForSummon;

        public MinionSummonData(MinionType MinionType, KeyCode key, Minion Prefab, int ClicksCountForSummon)
        {
            this.MinionType = MinionType;
            this.Prefab = Prefab;
            this.ClicksCountForSummon = ClicksCountForSummon;
            Key = key;
        }
    }
}