using Unity.Entities;
using UnityEngine;

namespace Sample2
{
    public class CapsuleSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public int Count = 1;
        
        private class CapsuleSpawnerBaker : Baker<CapsuleSpawnerAuthoring>
        {
            public override void Bake(CapsuleSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new SpawnData()
                {
                    Entity = GetEntity(authoring.Prefab,TransformUsageFlags.Dynamic),
                    SpawnCount = authoring.Count
                });
            }
        }
    }    
}

