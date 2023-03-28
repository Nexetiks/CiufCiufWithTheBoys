using UnityEngine;

namespace EarthEater.Components
{
    public interface IAmPickable
    {
        public Rigidbody2D Rb { get; }
        public void OnPickUp(object picker);
    }
}
