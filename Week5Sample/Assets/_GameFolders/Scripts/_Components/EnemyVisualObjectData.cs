using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    public class EnemyVisualObjectData : IComponentData
    {
        public GameObject VisualObject;
    }

    public class EnemyVisualObjectReference : ICleanupComponentData
    {
        public EnemyVisualController EnemyVisualController;
    }
}