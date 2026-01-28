using UnityEngine;

namespace DevAssets.Interfaces
{
    public interface ITarget
    {
        public Transform Transform {get; }
        public bool IsActive {get; }
    }
}