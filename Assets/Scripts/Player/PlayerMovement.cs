using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float swipeThreshold = 80f;
        [SerializeField] private Position pos;
        
        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip swipe;
        
        private int _currentPos = 1;
        private Vector3 _destination;
        private Vector2 _fingerDown;
        private Vector2 _fingerUp;
        private int _moved;
        private float _moveSpeed = 5f;
        
        private void Awake()
        {
            // reset destination
            _destination = transform.position;
        }

        void Update()
        {
            if (PlayerStatus.isGameOver || PlayerStatus.isPaused) return;

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
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (PlayerStatus.currentDebuff != DebuffType.ReversedControls)
                {
                    OnSwipeLeft();
                }
                else
                {
                    OnSwipeRight();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (PlayerStatus.currentDebuff != DebuffType.ReversedControls)
                {
                    OnSwipeRight();
                }
                else
                {
                    OnSwipeLeft();
                }
            }
            
            transform.position = Vector3.MoveTowards(transform.position,
                _destination,
                _moveSpeed * Time.deltaTime);
        }

        void CheckSwipe()
        {
            if (PlayerStatus.isGameOver || PlayerStatus.isPaused) return;

            if (HorizontalValMove() > swipeThreshold && HorizontalValMove() > VerticalMove())
            {
                if (_fingerDown.x - _fingerUp.x > 0)
                {
                    if (PlayerStatus.currentDebuff != DebuffType.ReversedControls)
                    {
                        OnSwipeRight();
                    }
                    else
                    {
                        OnSwipeLeft();
                    }
                }
                else if (_fingerDown.x - _fingerUp.x < 0)
                {
                    if (PlayerStatus.currentDebuff != DebuffType.ReversedControls)
                    {
                        OnSwipeLeft();
                    }
                    else
                    {
                        OnSwipeRight();
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

        void OnSwipeLeft()
        {
            if (_currentPos > 0 && _moved == 0)
            {
                _moved = 1;
                _currentPos--;

                MovePlayer();
            }
        }

        void OnSwipeRight()
        {
            if (_currentPos < 2 && _moved == 0)
            {
                _moved = 1;
                _currentPos++;

                MovePlayer();
            }
        }

        void MovePlayer()
        {
            _destination = new Vector2(pos.positions[_currentPos].position.x, transform.position.y);

            audioSource.clip = swipe;
            audioSource.Play();
            Invoke(nameof(MovedStatus), 0.2f);
        }

        void MovedStatus()
        {
            _moved = 0;
        }
    }
}