using Unity.Entities;
using Unity.Mathematics;

namespace Sample2
{
    //blob asset bir collection yapisidir ve bu yapilarda biz sadece readonly islem yapabilirz bir kere olusurlar ve o yapi hep ayni kalir
    
    //burda ise bunu Icompoennt data uzerinden entity'e veririz
    public struct SpawnPositionsReference : IComponentData
    {
        //burda ise bunun yani spawn position blob struct'in referencasini tutariz
        public BlobAssetReference<SpawnPositionBlob> BlobValueReference;
    }

    //blob asset olusturmamin yolu bir struct yapisi icinde kullanicagimiz blob seklini veririzi burdaki ornekte blob array seklinde
    public struct SpawnPositionBlob
    {
        public BlobArray<float3> Values;
    }
}