using UnityEngine;

namespace SampleScripts
{
    public interface IInputReader
    {
        public Vector2 Direction { get; }
    }
}