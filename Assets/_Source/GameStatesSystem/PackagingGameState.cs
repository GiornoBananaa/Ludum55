using Core;
using PackagingGame;

namespace GameStatesSystem
{
    public class PackagingGameState : GameState
    {
        private DraggableItem[] _draggableItems;

        public PackagingGameState(DraggableItem[] draggableItems)
        {
            _draggableItems = draggableItems;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }

        public override void Reset()
        {
            foreach (var item in _draggableItems)
            {
                item.ReturnToDefaultPosition();
                item.enabled = true;
            }
        }
    }
}