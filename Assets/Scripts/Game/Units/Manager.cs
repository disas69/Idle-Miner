using UnityEngine;

namespace Game.Units
{
    [RequireComponent(typeof(Animator))]
    public class Manager : MonoBehaviour
    {
        private Animator _animator;
        
        public bool IsAssigned { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetActive(bool isActive)
        {
            IsAssigned = isActive;
            gameObject.SetActive(isActive);
        }

        public void SetTriggered()
        {
            _animator.SetTrigger("Work");
        }
    }
}