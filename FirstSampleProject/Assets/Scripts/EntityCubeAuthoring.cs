using Unity.Entities;
using UnityEngine;

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
