using System;
using Effects;
using UnityEngine;
using UnityEngine.Pool;

namespace Controller
{
    public class EffectsController : MonoBehaviour
    {
        [SerializeField] private FlyingTextHandler flyingTextHandler;

        public void ShowFlyingText(string text, Vector3 position, float duration = 1f)
            => flyingTextHandler.Create(text, position, duration);
    }
}