using Unity.Entities;
using UnityEngine;

namespace Sample1
{
    public class SoldierAuthoring : MonoBehaviour
    {
        public GameObject VisualPrefab;
        public float MoveSpeed = 5f;
        
        private class SoldierAuthoringBaker : Baker<SoldierAuthoring>
        {
            public override void Bake(SoldierAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<InputData>(entity);

                AddComponent<MoveData>(entity, new()
                {
                    MoveSpeed = authoring.MoveSpeed
                });
                // AddComponent(entity, new MoveData()
                // {
                //     MoveSpeed = authoring.MoveSpeed
                // });

                AddComponentObject(entity, new SoldierVisualObjectData()
                {
                    PrefabObject = authoring.VisualPrefab
                });
            }
        }
    }
}