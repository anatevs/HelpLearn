using UnityEngine;

namespace GameCore
{
    public sealed class Target : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 100f;

        [SerializeField]
        private float _scaleSpeed = 1f;

        [SerializeField]
        private float _shiftSpeed = 4f;

        private float[] _scaleFactor = { 1f, 2f };

        private float _shiftFactor = 1f;

        private float _destroyTime = 3f;

        private int _scaleSign = 1;

        private Vector3[] _scaleFactorVec = new Vector3[2];

        private int _shiftSign = 1;

        private Vector3[] _shiftPosX = new Vector3[2];

        private bool _isRotatingX = false;
        private bool _isScaling = false;
        private bool _isShiftingX = false;

        private void Start()
        {
            _scaleFactor[0] *= transform.localScale.x;
            _scaleFactor[1] *= transform.localScale.x;

            _scaleFactorVec[0] = Vector3.one * _scaleFactor[0];
            _scaleFactorVec[1] = Vector3.one * _scaleFactor[1];

            _shiftPosX[0] = transform.position + new Vector3(_shiftFactor, 0, 0);
            _shiftPosX[1] = transform.position - new Vector3(_shiftFactor, 0, 0);

            Debug.Log($"Target created");
            Destroy(gameObject, _destroyTime);

            int randomIndex = Random.Range(0, 3);

            switch (randomIndex)
            {
                case 0:
                    {
                        _isRotatingX = true;
                        break;
                    }
                case 1:
                    {
                        _isScaling = true;
                        break;
                    }
                case 2:
                    {
                        _isShiftingX = true;
                        break;
                    }
            }
        }

        private void Update()
        {
            //Debug.Log($"Target still alive");

            RotateX();

            Scale();

            ShiftX();
        }

        private void OnDestroy()
        {
            Debug.Log($"Target destroyed");
        }

        private void RotateX()
        {
            if (_isRotatingX)
            {
                transform.Rotate(transform.right, _rotationSpeed * Time.deltaTime);
            }
        }

        private void Scale()
        {
            if (_isScaling)
            {
                var newScale = transform.localScale.x + _scaleSign * _scaleSpeed * Time.deltaTime;

                if (_scaleSign > 0 && newScale >= _scaleFactor[1])
                {
                    transform.localScale = _scaleFactorVec[1];
                    _scaleSign = -1;
                    return;
                }
                else if (_scaleSign < 0 && newScale <= _scaleFactor[0])
                {
                    transform.localScale = _scaleFactorVec[0];
                    _scaleSign = 1;
                    return;
                }

                transform.localScale = Vector3.one * newScale;
            }
        }

        private void ShiftX()
        {
            if (_isShiftingX)
            {
                var nextX = transform.position.x + _shiftSign * _shiftSpeed * Time.deltaTime;

                if (_shiftSign > 0 && nextX >= _shiftPosX[0].x)
                {
                    transform.position = _shiftPosX[0];
                    _shiftSign = -1;
                    return;
                }
                else if (_shiftSign < 0 && nextX <= _shiftPosX[1].x)
                {
                    transform.position = _shiftPosX[1];
                    _shiftSign = 1;
                    return;
                }

                transform.position = new Vector3(nextX, transform.position.y, transform.position.z);
            }
        }
    }
}