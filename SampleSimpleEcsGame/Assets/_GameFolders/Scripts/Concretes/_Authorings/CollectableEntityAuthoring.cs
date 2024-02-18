using EcsGame.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Authorings
{
    public class CollectableEntityAuthoring : MonoBehaviour
    {
        public int Score = 5;
        
        class CollectableEntityBaker : Baker<CollectableEntityAuthoring>
        {
            public override void Bake(CollectableEntityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<CollectableTag>(entity);

                AddComponent<CollectData>(entity, new()
                {
                    Score = authoring.Score
                });
            }
        }
    }    
}

