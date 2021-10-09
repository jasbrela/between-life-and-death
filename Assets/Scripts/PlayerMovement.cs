using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float swipeThreshold = 80f;
    [SerializeField] private Position pos;
    [SerializeField] private GameObject scenario;
    private int _currentPos = 1;
    private Vector3 _destination;
    private Vector2 _fingerDown;
    private Vector2 _fingerUp;

    void Update()
    {
        if (!PlayerStatus.GameOver)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                if (touch.phase == TouchPhase.Began)
                {
                    _fingerUp = touch.position;
                    _fingerDown = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    _fingerDown = touch.position;
                    CheckSwipe();
                }
                
                // TODO: MOVE ONLY ONE POSITION PER TOUCH
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A");
                OnSwipeLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D");
                OnSwipeRight();
            }
        }
    }

    void CheckSwipe()
    {
        if (VerticalMove() > swipeThreshold && VerticalMove() > HorizontalValMove())
        {
            if (_fingerDown.y - _fingerUp.y > 0)
            {
                OnSwipeUp();
            }
            else if (_fingerDown.y - _fingerUp.y < 0)
            {
                OnSwipeDown();
            }

            _fingerUp = _fingerDown;
        }
        else if (HorizontalValMove() > swipeThreshold && HorizontalValMove() > VerticalMove())
        {
            if (_fingerDown.x - _fingerUp.x > 0)
            {
                OnSwipeRight();
            }
            else if (_fingerDown.x - _fingerUp.x < 0)
            {
                OnSwipeLeft();
            }

            _fingerUp = _fingerDown;
        }
    }

    float VerticalMove()
    {
        return Mathf.Abs(_fingerDown.y - _fingerUp.y);
    }

    float HorizontalValMove()
    {
        return Mathf.Abs(_fingerDown.x - _fingerUp.x);
    }

    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }

    void OnSwipeLeft()
    {
        if (_currentPos > 0)
        {
            _currentPos--;
            scenario. transform.position =new Vector3(scenario.transform.position.x + 0.2f,
                scenario.transform.position.y, scenario.transform.position.z);
            _destination = new Vector2(pos.positions[_currentPos].position.x, transform.position.y);
            Move();
        }
    }

    void OnSwipeRight()
    {
        if (_currentPos < 2)
        {
            _currentPos++;
            scenario.transform.position = new Vector3(scenario.transform.position.x - 0.2f,
                scenario.transform.position.y, scenario.transform.position.z);
            _destination = new Vector2(pos.positions[_currentPos].position.x, transform.position.y);
            Move();
        }
    }

    void Move()
    {
        transform.position = new Vector3(_destination.x, transform.position.y, transform.position.z);
    }
}