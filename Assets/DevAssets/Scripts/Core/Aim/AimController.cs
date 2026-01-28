using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using DevAssets.Core.Characters.Player;

namespace DevAssets.Controllers
{
    public class AimController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _aimPoint;
        private Vector2 _mousePositionOnWorldPoint;

        [Inject] private readonly PlayerInput _playerInput;

        public Transform AimPoint => _aimPoint;

        private void Awake()
        {
            _mainCamera = _mainCamera ? _mainCamera : GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Update()
        {
            var mousePos = _playerInput.GetMousePos();
            _mousePositionOnWorldPoint = _mainCamera.ScreenToWorldPoint(mousePos);

            Vector2 rotationDirToMousPos = _mousePositionOnWorldPoint - transform.position.ConvertTo<Vector2>();
            float rotationZ = Mathf.Atan2(rotationDirToMousPos.y, rotationDirToMousPos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }
    }
}