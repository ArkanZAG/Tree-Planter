using System;
using Data;
using DG.Tweening;
using Plants;
using UnityEngine;

namespace GridSystem
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Outline outline;
        [SerializeField] private GameObject visual;

        private int x, y;

        public int X => x;
        public int Y => y;
    
        private Tween selectTween;
        private IPlant currentPlant = null;

        public event Action OnTileUpdated;

        public IPlant CurrentPlant => currentPlant;

        public void SetHighlight(bool isHighlighted)
        {
            outline.enabled = isHighlighted;
        }

        public void AnimatePop()
        {
            if (selectTween != null) DOTween.Kill(selectTween);

            var originalY = visual.transform.localPosition.y;
        
            var pos = visual.transform.localPosition;
            pos.y = originalY + 0.2f;
            visual.transform.localPosition = pos;
        
            selectTween = visual.transform.DOLocalMoveY(originalY, 0.15f).SetEase(Ease.OutCirc);
        }

        public void Initialize(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        public PlantData GetPlantData(int version) => new(x, y, currentPlant.Id, currentPlant.GetValues(version));

        public void SetPlant(IPlant plant, bool invokeUpdate = true)
        {
            currentPlant = plant;
            SetBiome(currentPlant.Biome);
            if (invokeUpdate) OnTileUpdated?.Invoke();
        }

        public void RemovePlant(bool invokeUpdate = true)
        {
            Destroy(currentPlant.GameObject);
            currentPlant = null;
            
            if (invokeUpdate) OnTileUpdated?.Invoke();
        }

        public void SetBiome(BiomeType biome)
        {
            //TODO: CHANGE VISUAL HERE BASED ON BIOME
        }
    }
}