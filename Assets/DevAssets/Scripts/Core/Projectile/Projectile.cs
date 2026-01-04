using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private const int SECOND_IN_MILLISECONDS = 1000;
        [SerializeField] private int _timeToDeactivate = 3;

        private IObjectPool<Projectile> _objectPool;
        private Rigidbody2D _rigidBody2D;

        public IObjectPool<Projectile> ObjectPool { set => _objectPool = value; }
        public Rigidbody2D RigidBody => _rigidBody2D;

        private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }
        public async void Deactivate()
        {
            await Task.Delay(_timeToDeactivate * SECOND_IN_MILLISECONDS);
            _objectPool.Release(this);
        }
    }
}
