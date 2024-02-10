using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public EntityStats Stats;
        
        class PlayerBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<InputData>(entity);
                AddComponent<PlayerTag>(entity);

                AddComponent(entity, new MoveData()
                {
                    MoveSpeed = authoring.Stats.MoveSpeed
                });
            }
        }
    }
}