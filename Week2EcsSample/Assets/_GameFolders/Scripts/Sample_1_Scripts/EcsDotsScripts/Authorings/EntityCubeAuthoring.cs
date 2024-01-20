using Unity.Entities;
using UnityEngine;

namespace Sample_1_Scripts
{
    public class EntityCubeAuthoring : MonoBehaviour
    {
        //private field'lar snake case yazim kurali ile yazilir _speed;
        //public field veya property class method gibi yapilar pascal case ile yazlir Speed EntityCubeAuthoring
        //camel case bu ise local alanlarda isimlendirme kuralidir entity gibi myEntity gibi
        //[SerializeField] float _speed;
        // [SerializeField] float Speed;
        public float Speed;
        
        class EntityCubeBaker : Baker<EntityCubeAuthoring>
        {
            public override void Bake(EntityCubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<CubeTag>(entity);
                
                AddComponent(entity, new MoveData()
                {
                    Speed = authoring.Speed
                });
            }
        }
    }

    public class Araba
    {
        readonly Input _input = new Input();

        // public Araba()
        // {
        //     _input = new Input();
        // }

        // public void CreateInput()
        // {
        //     _input = new Input();
        // }
    }

    public class Input
    {
        
    }
}