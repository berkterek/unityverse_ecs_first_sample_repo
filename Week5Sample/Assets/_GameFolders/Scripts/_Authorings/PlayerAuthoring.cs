using Unity.Entities;
using UnityEngine;

namespace SampleScripts
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public GameObject VisualPrefab;
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
                
                AddComponent(entity, new DamageData()
                {
                    Damage = authoring.Stats.Damage
                });

                AddComponentObject(entity, new PlayerVisualObjectData()
                {
                    VisualObject = authoring.VisualPrefab
                });
            }
        }
    }
}