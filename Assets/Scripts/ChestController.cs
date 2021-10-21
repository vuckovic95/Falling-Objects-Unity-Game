using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityStandardAssets.CrossPlatformInput;

public class ChestController : MonoBehaviour
{
    [BoxGroup("Speed")]
    [SerializeField]
    private float _speed;

    private Transform _transform;
    private int _leftBoundarie;
    private int _rightBoundarie;

    private float RESOLUTION_FACTOR = 2.38f;

    private void Start()
    {
        SubscribeToActions();

        _transform = this.transform;
        _rightBoundarie = (int)(Screen.width / RESOLUTION_FACTOR);
        _leftBoundarie = -(int)(Screen.width / RESOLUTION_FACTOR);
        
    }

    private void SubscribeToActions()
    {
        Actions.HoldLeftArrowAction += MoveLeftOnArrows;
        Actions.HoldRightArrowAction += MoveRightOnArrows;
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    case TouchPhase.Stationary:
                        Touch current = Input.GetTouch(0);
                        break;

                    case TouchPhase.Moved:
                        _transform.position = new Vector3(_transform.position.x + touch.deltaPosition.x * _speed, _transform.position.y, _transform.position.z);
                        break;
                }

                if (_transform.position.x > _rightBoundarie)
                {
                    _transform.position = new Vector3(_rightBoundarie, _transform.position.y, _transform.position.z);
                }
                if (_transform.position.x <= _leftBoundarie)
                {
                    _transform.position = new Vector3(_leftBoundarie, _transform.position.y, _transform.position.z);
                }
            }
#endif


#if UNITY_EDITOR
        _transform.Translate(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, 0, 0);
        if (_transform.localPosition.x > _rightBoundarie)
        {
            _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
        }
        if (_transform.localPosition.x <= _leftBoundarie)
        {
            _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
        }
#endif
    }

    private void MoveRightOnArrows()
    {
        _transform.Translate(CrossPlatformInputManager.GetAxis("Horizontal") * _speed * Time.deltaTime, 0, 0);
        if (_transform.localPosition.x > _rightBoundarie)
        {
            _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
        }
        if (_transform.localPosition.x <= _leftBoundarie)
        {
            _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
        }
    }
    private void MoveLeftOnArrows()
    {
        _transform.Translate(-_speed * Time.deltaTime, 0, 0);
        if (_transform.localPosition.x > _rightBoundarie)
        {
            _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
        }
        if (_transform.localPosition.x <= _leftBoundarie)
        {
            _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
        }
    }
}
