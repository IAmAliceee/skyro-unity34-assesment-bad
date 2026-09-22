using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [field: SerializeField] public InputActionReference PauseKey;
        [field: SerializeField] public InputActionReference MoveKey;

        private void OnEnable()
        {
            PauseKey.action.Enable();
            MoveKey.action.Enable();
        }

        private void OnDisable()
        {
            PauseKey.action.Disable();
            MoveKey.action.Disable();
        }
    }
}