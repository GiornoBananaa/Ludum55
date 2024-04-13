using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ServiceUpdater : MonoBehaviour
    {
        private class UpdaterData
        {
            public readonly IUpdatable Updatable;
            public readonly float Frequency;
            public float TimeBeforeUpdate;

            public UpdaterData(IUpdatable updatable, float frequency)
            {
                Updatable = updatable;
                Frequency = frequency;
                TimeBeforeUpdate = frequency;
            }
        }
        
        private List<UpdaterData> _updaters;

        private void Awake()
        {
            _updaters = new List<UpdaterData>();
        }

        public void AddUpdater(IUpdatable updatable, float frequency)
        {
            _updaters.Add(new UpdaterData(updatable, frequency));
        }
        
        private void Update()
        {
            foreach (var updaterData in _updaters)
            {
                updaterData.TimeBeforeUpdate -= Time.deltaTime;
                if(updaterData.TimeBeforeUpdate <= 0)
                {
                    updaterData.Updatable.Update();
                    updaterData.TimeBeforeUpdate = updaterData.Frequency;
                }
            }
        }
    }
}