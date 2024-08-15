using Controller;
using Data;
using GridSystem;

namespace Plants
{
    public interface IPlant
    {
        public Tile Tile { get; }
        public string Id { get; }
        
        public void Initialize(Tile tile, Tile[] neighbours, GameController gameController, GridController gridController);
        public void OnClick();
    }
}