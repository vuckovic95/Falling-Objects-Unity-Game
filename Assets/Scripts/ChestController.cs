using UnityEngine;
using NaughtyAttributes;
using Zenject;
using MoreMountains.NiceVibrations;

public class ChestController : MonoBehaviour
{
    [BoxGroup("Speed")]
    [SerializeField]
    private float _speed;

    [BoxGroup("Arrows")]
    [SerializeField]
    private Arrow _leftArrow;
    [BoxGroup("Arrows")]
    [SerializeField]
    private Arrow _rightArrow;

    private Transform _transform;
    private int _leftBoundarie;
    private int _rightBoundarie;
    private bool _movingChest;

    private float RESOLUTION_FACTOR = 2.3f;

    private void Start()
    {
        SubscribeToActions();

        _transform = this.transform;
        _rightBoundarie = (int)(Screen.width / RESOLUTION_FACTOR);
        _leftBoundarie = -(int)(Screen.width / RESOLUTION_FACTOR);
        
    }
    private void OnDestroy()
    {
        UnsubscribeToActions();
    }

    private void SubscribeToActions()
    {
        Actions.StartGameAction += ResetPosition;
        Actions.HoldLeftArrowAction += MoveLeftOnArrows;
        Actions.HoldRightArrowAction += MoveRightOnArrows;
    }

    private void UnsubscribeToActions()
    {
        Actions.StartGameAction -= ResetPosition;
        Actions.HoldLeftArrowAction -= MoveLeftOnArrows;
        Actions.HoldRightArrowAction -= MoveRightOnArrows;
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            _movingChest = true;
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

            if (_transform.localPosition.x > _rightBoundarie)
            {
                _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
            }
            if (_transform.localPosition.x <= _leftBoundarie)
            {
                _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
            }
        }
        else
        {
            _movingChest = false;
        }

        if (_leftArrow.HasClicked && !_rightArrow.HasClicked && !_movingChest)
        {
            _transform.Translate(-_speed * Time.deltaTime * 600, 0, 0);
            if (_transform.localPosition.x > _rightBoundarie)
            {
                _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
            }
            if (_transform.localPosition.x <= _leftBoundarie)
            {
                _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
            }
        }
        else if (!_leftArrow.HasClicked && _rightArrow.HasClicked && _movingChest)
        {
            _transform.Translate(_speed * Time.deltaTime * 600, 0, 0);
            if (_transform.localPosition.x > _rightBoundarie)
            {
                _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
            }
            if (_transform.localPosition.x <= _leftBoundarie)
            {
                _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
            }
        }
#endif


#if UNITY_EDITOR

        if (_leftArrow.HasClicked && !_rightArrow.HasClicked)
        {
            _transform.Translate(-_speed * Time.deltaTime * 600, 0, 0);
            if (_transform.localPosition.x > _rightBoundarie)
            {
                _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
            }
            if (_transform.localPosition.x <= _leftBoundarie)
            {
                _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
            }
        }
        else if(!_leftArrow.HasClicked && _rightArrow.HasClicked)
        {
            _transform.Translate(_speed * Time.deltaTime * 600, 0, 0);
            if (_transform.localPosition.x > _rightBoundarie)
            {
                _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
            }
            if (_transform.localPosition.x <= _leftBoundarie)
            {
                _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
            }
        }
#endif
    }

    private void MoveRightOnArrows()
    {
        _transform.Translate(_speed * Time.deltaTime * 200, 0, 0);
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
        _transform.Translate(-_speed * Time.deltaTime * 200, 0, 0);
        if (_transform.localPosition.x > _rightBoundarie)
        {
            _transform.localPosition = new Vector3(_rightBoundarie, _transform.localPosition.y, 0);
        }
        if (_transform.localPosition.x <= _leftBoundarie)
        {
            _transform.localPosition = new Vector3(_leftBoundarie, _transform.localPosition.y, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            Item itemComponent = collision.GetComponent<Item>();

            Actions.ItemPickedAction?.Invoke(itemComponent);

            collision.gameObject.SetActive(false);
            itemComponent.CanMove = false;
        }
    }

    private void ResetPosition()
    {
        _transform.localPosition = new Vector3(0, _transform.localPosition.y, 0);
    }
}
