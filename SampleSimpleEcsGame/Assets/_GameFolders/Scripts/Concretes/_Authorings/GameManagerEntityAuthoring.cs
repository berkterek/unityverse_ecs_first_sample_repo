using EcsGame.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Authorings
{
    public class GameManagerEntityAuthoring : MonoBehaviour
    {
        private class GameManagerEntityBaker : Baker<GameManagerEntityAuthoring>
        {
            public override void Bake(GameManagerEntityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent<GameStatusData>(entity);
            }
        }
    }
}