using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float swipeThreshold = 80f;
    [SerializeField] private Position pos;
    [SerializeField] private GameObject scenario;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip swipe;
    private int _currentPos = 1;
    private Vector3 _destination;
    private Vector2 _fingerDown;
    private Vector2 _fingerUp;
    private int moved;

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
                if (PlayerStatus.CurrentDebuff != Debuff.InvertedControllers)
                {
                    OnSwipeLeft();
                }
                else
                {
                    OnSwipeRight();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (PlayerStatus.CurrentDebuff != Debuff.InvertedControllers)
                {
                    OnSwipeRight();
                }
                else
                {
                    OnSwipeLeft();
                }
            }
        }
    }

    void CheckSwipe()
    {
        /*if (VerticalMove() > swipeThreshold && VerticalMove() > HorizontalValMove())
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
        else */
        if (HorizontalValMove() > swipeThreshold && HorizontalValMove() > VerticalMove())
        {
            if (_fingerDown.x - _fingerUp.x > 0)
            {
                if (PlayerStatus.CurrentDebuff != Debuff.InvertedControllers)
                {
                    OnSwipeLeft();
                }
                else
                {
                    OnSwipeRight();
                }
            }
            else if (_fingerDown.x - _fingerUp.x < 0)
            {
                if (PlayerStatus.CurrentDebuff != Debuff.InvertedControllers)
                {
                    OnSwipeRight();
                }
                else
                {
                    OnSwipeLeft();
                }
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
/*
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }
    */

    void OnSwipeLeft()
    {
        if (_currentPos > 0 && moved == 0)
        {
            moved = 1;
            _currentPos--;
            scenario.transform.position = new Vector3(scenario.transform.position.x + 0.2f,
                scenario.transform.position.y, scenario.transform.position.z);
            _destination = new Vector2(pos.positions[_currentPos].position.x, transform.position.y);
            Move();
            
        }
    }

    void OnSwipeRight()
    {
        if (_currentPos < 2 && moved == 0)
        {
            moved = 1;
            _currentPos++;
            scenario.transform.position = new Vector3(scenario.transform.position.x - 0.2f,
            scenario.transform.position.y, scenario.transform.position.z);
            _destination = new Vector2(pos.positions[_currentPos].position.x, transform.position.y);
            Move();
        }
    }

    void Move()
    {
        audioSource.clip = swipe;
        audioSource.Play();
        transform.position = new Vector3(_destination.x, transform.position.y, transform.position.z);
        Invoke("movedStatus", 0.2f);
    }

    void movedStatus()
    {
        moved = 0;
    }
}