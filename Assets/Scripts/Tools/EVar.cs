using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    // Event variable
    [System.Serializable]
    public class EVar<T>
    {
        private T _value;

        [field: SerializeField] public UnityEvent<T> OnChanged { get; private set; } = new();

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged.Invoke(Value);
            }
        }

        public EVar(T defaultValue = default)
        {
            _value = defaultValue;
        }

        public static implicit operator T(EVar<T> eVar) => eVar.Value;
    }
}