using Controller;
using Data;
using GridSystem;

namespace Plants
{
    public interface IPlant
    {
        public Tile Tile { get; }
        public string Id { get; }
        
        public void Initialize(PlantData data, Tile tile, GameController gameController, GridController gridController);
        public void OnClick();
    }
}