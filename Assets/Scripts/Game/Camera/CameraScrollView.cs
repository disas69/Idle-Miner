using Framework.Input;
using Game.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraScrollView : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        private float _screenToWorldScaleFactor;
        private float _screenToCanvasScaleFactor;
        private bool _dragging;
        private Vector2 _lastDragDelta;
        private Vector2 _currentVelocity;
        private VectorAverager _dragSpeedAverage;
        private int _pointersCount;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private InputEventProvider _inputEventProvider;
        [SerializeField] private float _topLimit;
        [SerializeField] private float _bottomLimit;
        [SerializeField] private float _maxReleaseSpeed = 20f;
        [SerializeField] private float _slideDecelerationFactor = 1.5f;
        [SerializeField] private float _slideStopThreshold = 1f;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _dragSpeedAverage = new VectorAverager(0.1f);

            var canvasRect = _canvas.GetComponent<RectTransform>().rect;
            _screenToCanvasScaleFactor = canvasRect.width / _camera.pixelWidth;
            _screenToWorldScaleFactor = 2 * _camera.orthographicSize / _camera.pixelHeight;

            _inputEventProvider.PointerDown += OnPointerDown;
            _inputEventProvider.BeginDrag += OnBeginDrag;
            _inputEventProvider.Drag += OnDrag;
            _inputEventProvider.PointerUp += OnPointerUp;
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            _pointersCount++;
        }

        private void OnBeginDrag(PointerEventData eventData)
        {
            _dragging = true;
            _dragSpeedAverage.Clear();
        }

        private void OnDrag(PointerEventData eventData)
        {
            _lastDragDelta = eventData.delta;
        }

        private void OnPointerUp(PointerEventData eventData)
        {
            _pointersCount--;
            if (_pointersCount == 0 && _dragging)
            {
                _dragging = false;

                var maxReleaseSpeed = _maxReleaseSpeed / _screenToCanvasScaleFactor;
                var releaseVelocity = _dragSpeedAverage.Value;
                if (releaseVelocity.magnitude > maxReleaseSpeed)
                {
                    releaseVelocity = releaseVelocity.normalized * maxReleaseSpeed;
                }

                _currentVelocity = releaseVelocity;
            }
        }

        private void Update()
        {
            if (_dragging)
            {
                _currentVelocity = -_lastDragDelta;
                _dragSpeedAverage.AddSample(_currentVelocity);
            }
            else
            {
                var currentSpeed = _currentVelocity.magnitude;
                if (currentSpeed > _slideStopThreshold / _screenToCanvasScaleFactor)
                {
                    _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero,
                        _slideDecelerationFactor * currentSpeed * Time.deltaTime);
                }
                else
                {
                    _currentVelocity = Vector2.zero;
                }
            }

            if (_currentVelocity.magnitude > 0.01f)
            {
                Vector3 worldspaceDelta = _currentVelocity * _screenToWorldScaleFactor;
                var newPosition = _camera.transform.position += worldspaceDelta;
                newPosition.x = 0f;
                newPosition.y = Mathf.Clamp(newPosition.y, _bottomLimit, _topLimit);
                _camera.transform.position = newPosition;
            }

            _lastDragDelta = Vector2.zero;
        }

        private void OnDestroy()
        {
            _inputEventProvider.PointerDown -= OnPointerDown;
            _inputEventProvider.BeginDrag -= OnBeginDrag;
            _inputEventProvider.Drag -= OnDrag;
            _inputEventProvider.PointerUp -= OnPointerUp;
        }
    }
}