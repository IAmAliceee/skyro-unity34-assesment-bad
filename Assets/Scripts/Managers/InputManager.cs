using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [field: SerializeField] public InputActionReference PauseKey;
        [field: SerializeField] public InputActionReference MoveKey;
        [field: SerializeField] public InputActionReference ShootKey;

        private void OnEnable()
        {
            PauseKey.action.Enable();
            MoveKey.action.Enable();
            ShootKey.action.Enable();
        }

        private void OnDisable()
        {
            PauseKey.action.Disable();
            MoveKey.action.Disable();
            ShootKey.action.Disable();
        }
    }
}