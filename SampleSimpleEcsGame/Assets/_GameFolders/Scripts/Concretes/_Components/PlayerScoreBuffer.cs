using Unity.Entities;

namespace EcsGame.Components
{
    public struct PlayerScoreBuffer : IBufferElementData
    {
        public int Value;
    }
}