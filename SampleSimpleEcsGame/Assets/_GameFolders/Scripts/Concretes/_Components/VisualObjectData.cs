using Unity.Entities;
using UnityEngine;

namespace EcsGame.Components
{
    public class VisualObjectData : IComponentData,IEnableableComponent
    {
        public GameObject VisualPrefab;
    }
}