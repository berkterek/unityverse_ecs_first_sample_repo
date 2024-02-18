using UnityEngine;

namespace EcsGame.Abstracts.Inputs
{
    public interface IInputReader
    {
        Vector2 Direction { get; }
    }
}