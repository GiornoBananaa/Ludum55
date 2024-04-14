using InputSystem;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string SUMMONER_DATA_PATH = "SummonerData";
        
        [SerializeField] private InputListener _inputListener;
        
        private void Awake()
        {
            //-SO-
            
            //--
            
            _inputListener.Construct();
        }
    }
}
