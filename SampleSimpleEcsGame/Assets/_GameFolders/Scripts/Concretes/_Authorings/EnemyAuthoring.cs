using EcsGame.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsGame.Authorings
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        public float Damage;
        public float MaxDamageTime = 2f;
        
        class EnemyBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<EnemyTag>(entity);
                AddComponent<InputData>(entity);
                AddComponent<TransformMoverTag>(entity);
                AddComponent<RotationTag>(entity);
                
                AddComponent<MoveData>(entity, new ()
                {
                    MoveSpeed = authoring.MoveSpeed
                });

                AddComponent<DamageData>(entity, new()
                {
                    Damage = authoring.Damage,
                    CurrentTime = authoring.MaxDamageTime,
                    MaxTime = authoring.MaxDamageTime
                });
            }
        }
    }    
}