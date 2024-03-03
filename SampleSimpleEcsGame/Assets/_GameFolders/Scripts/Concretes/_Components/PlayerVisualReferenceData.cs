using EcsGame.Controllers;
using Unity.Entities;

namespace EcsGame.Components
{
    public class PlayerVisualReferenceData : ICleanupComponentData
    {
        public PlayerVisualController Reference;
    }
}