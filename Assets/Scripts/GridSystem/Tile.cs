using System;
using Biomes;
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
        [SerializeField] private MeshRenderer mesh;

        private int x, y;

        public int X => x;
        public int Y => y;
    
        private Tween selectTween;
        private IPlant currentPlant = null;
        private BiomeDatabase biomeDatabase;
        private Outline[] plantOutlines;
        
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        public event Action OnTileUpdated;

        public IPlant CurrentPlant => currentPlant;

        public void SetHighlight(bool isHighlighted)
        {
            outline.enabled = isHighlighted;

            if (plantOutlines == null) return;
            foreach (var plantOutline in plantOutlines)
                plantOutline.enabled = isHighlighted;
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

        public void Initialize(BiomeDatabase biomeDb, int newX, int newY)
        {
            biomeDatabase = biomeDb;
            x = newX;
            y = newY;
        }

        public PlantData GetPlantData(int version) => new(x, y, currentPlant.Id, currentPlant.GetValues(version));

        public void SetPlant(IPlant plant, bool invokeUpdate = true)
        {
            currentPlant = plant;

            plantOutlines = plant.GameObject.GetComponentsInChildren<Outline>(true);
            
            currentPlant.OnPlantUpdated += OnCurrentPlantUpdated;
            if (invokeUpdate) OnTileUpdated?.Invoke();
        }

        private void OnCurrentPlantUpdated()
        {
            SetBiomeProgression(currentPlant.Biome, currentPlant.GetNormalizedVisualProgress());
            
            OnTileUpdated?.Invoke();
        }

        public void RemovePlant(bool invokeUpdate = true)
        {
            currentPlant.OnPlantUpdated -= OnCurrentPlantUpdated;
            Destroy(currentPlant.GameObject);
            currentPlant = null;
            plantOutlines = Array.Empty<Outline>();
            
            if (invokeUpdate) OnTileUpdated?.Invoke();
        }

        public void SetBiomeProgression(BiomeType biome, float normalizedProgress)
        {
            var data = biomeDatabase.Get(biome);
            var color = data.TileGradient.Evaluate(normalizedProgress);
            
            mesh.material.SetColor(BaseColor, color);   
        }
    }
}