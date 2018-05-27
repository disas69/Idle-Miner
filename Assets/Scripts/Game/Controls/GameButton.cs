using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Controls
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class GameButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
        IPointerExitHandler
    {
        private SpriteRenderer _spriteRenderer;
        private bool _isPressed;
        private bool _isPointerEnter;

        [SerializeField] private bool _isActive;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _pressedSprite;
        [SerializeField] private Sprite _disabledSprite;
        [Space] [SerializeField] private UnityEvent _callback;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _isActive ? _enabledSprite : _disabledSprite;
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = _isActive ? _enabledSprite : _disabledSprite;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isActive) return;

            _isPressed = true;
            _spriteRenderer.sprite = _pressedSprite;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isActive) return;

            _isPressed = false;
            _spriteRenderer.sprite = _enabledSprite;

            if (_isPointerEnter)
            {
                _callback.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isActive) return;

            _isPointerEnter = true;
            if (_isPressed)
            {
                _spriteRenderer.sprite = _pressedSprite;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isActive) return;

            _isPointerEnter = false;
            if (_isPressed)
            {
                _spriteRenderer.sprite = _enabledSprite;
            }
        }
    }
}