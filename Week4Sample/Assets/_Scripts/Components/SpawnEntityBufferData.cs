using Unity.Entities;

namespace Sample1
{
    //IBufferElementData bizim DynamicBuffer'lari kullanmamiza yarar Dynamic Buffer'lar collection yapisidir ecs dots icin kullandimgiz ozel yapidir calisma mantigi bildiginiz list yapisi gibidir read ve write amacli kullanilan yapilardir
    public struct SpawnEntityBufferData : IBufferElementData
    {
        public Entity Entity;
    }
}