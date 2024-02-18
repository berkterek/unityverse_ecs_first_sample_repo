using EcsGame.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        
        class PlayerBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<PlayerTag>(entity);
                AddComponent<InputData>(entity);
                AddComponent<PlayerScoreData>(entity);

                AddBuffer<PlayerScoreBuffer>(entity);
                
                AddComponent<MoveData>(entity, new ()
                {
                    MoveSpeed = authoring.MoveSpeed
                });
            }
        }
    }    
}

