using Unity.Entities;
using UnityEngine;

namespace FirstSampleProject.Sample_01_Scripts
{
    public class EntityCubeAuthoring : MonoBehaviour
    {
        [SerializeField] float _moveSpeed = 5f;

        class EntityCubeBaker : Baker<EntityCubeAuthoring>
        {
            public override void Bake(EntityCubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MoveData()
                {
                    MoveSpeed = authoring._moveSpeed
                });
            }
        }
    }    
}

