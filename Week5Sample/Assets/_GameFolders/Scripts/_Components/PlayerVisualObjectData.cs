using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    public class PlayerVisualObjectData : IComponentData
    {
        public GameObject VisualObject;
    }

    public class PlayerVisualReference : IComponentData
    {
        public PlayerVisualController PlayerVisualController;
    }
}