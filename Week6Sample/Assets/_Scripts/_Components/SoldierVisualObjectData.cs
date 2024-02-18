using Unity.Entities;
using UnityEngine;

namespace Sample1
{
    public class SoldierVisualObjectData : IComponentData
    {
        public GameObject PrefabObject;
    }

    public class SoldierVisualReferenceData : IComponentData
    {
        public SoldierVisualController Reference;
    }
}