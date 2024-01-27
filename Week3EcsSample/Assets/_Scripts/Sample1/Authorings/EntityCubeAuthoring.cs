using UnityEngine;
using Unity.Entities;
using Random = Unity.Mathematics.Random;

namespace Sample1
{
    public class EntityCubeAuthoring : MonoBehaviour
    {
        public uint Seed
        {
            get
            {
                System.Random random = new System.Random();
                int randomSeed = random.Next(0,int.MaxValue);
                uint seed = (uint)randomSeed;
                Debug.Log($"<color=red>{seed}</color>");
                return seed;
            }
        }
        
        class EntityCubeBaker : Baker<EntityCubeAuthoring>
        {
            public override void Bake(EntityCubeAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<CubeEntityTag>(entity);

                AddComponent(entity, new MoveData()
                {
                    Speed = 1f,
                });

                //seed bize random bir sayi doner ama spawn islemlerinde ayni seed numarasi uzerinden bize donus olucaktir o yuzden her entity'inin unique bir seed olmasi icin Index + Version toplaimi her entity'inin id'sidir ve seed + id bize butun entity'lere unique bir seed vermemizi saglicaktir
                uint seed = authoring.Seed + (uint)entity.Index + (uint)entity.Version;
                AddComponent(entity, new RandomData()
                {
                    Random = Random.CreateFromIndex(seed)
                });
            }
        }
    }
}