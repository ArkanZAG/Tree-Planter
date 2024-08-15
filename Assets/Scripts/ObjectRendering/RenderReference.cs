using System;
using UnityEngine;

namespace ObjectRendering
{
    public class RenderReference : MonoBehaviour
    {
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 rotationOffset;

        private RenderTexture currentTexture;

        public Vector3 PositionOffset => positionOffset;
        public Vector3 RotationOffset => rotationOffset;
        public RenderTexture CurrentTexture => currentTexture;

        public void SetTexture(RenderTexture tex)
        {
            currentTexture = tex;
        }
    }
}