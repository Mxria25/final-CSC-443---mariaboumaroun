using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Lanes")]
    [SerializeField] private float laneOffset = 2f;
    [SerializeField, Min(1)] private int laneCount = 3;
    [SerializeField] private float laneSwitchSpeed = 14f;

    [Header("Jump")]
    [SerializeField] private float jumpVelocity = 13f;
    [SerializeField] private float gravity = -30f;
    [SerializeField] private float trainRoofHeight = 2f;

    private int _laneIndex;
    private float _y;
    private float _yVel;
    private bool _isOnTrain;
    private int _walkableContacts;
    private Vector2 _prevMove;
    private AudioSource _audioSource;

    void Awake()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;
        if (GameManager.Instance != null && GameManager.Instance.IsPaused) return;

        Vector2 v = ctx.ReadValue<Vector2>();

        if (v.x > 0.5f && _prevMove.x <= 0.5f) ChangeLane(+1);
        else if (v.x < -0.5f && _prevMove.x >= -0.5f) ChangeLane(-1);

        if (v.y > 0.5f && _prevMove.y <= 0.5f && (_y <= 0f || _isOnTrain))
        {
            _yVel = jumpVelocity;
            _isOnTrain = false;
            _walkableContacts = 0;

            if (_audioSource != null)
                _audioSource.Play();
        }

        _prevMove = v;
    }

    private void ChangeLane(int delta)
    {
        int half = laneCount / 2;
        _laneIndex = Mathf.Clamp(_laneIndex + delta, -half, half);
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;
        if (GameManager.Instance != null && GameManager.Instance.IsPaused) return;

        _yVel += gravity * Time.deltaTime;
        _y += _yVel * Time.deltaTime;

        if (_y < 0f)
        {
            _y = 0f;
            _yVel = 0f;
        }

        if (_isOnTrain && _y < trainRoofHeight)
        {
            _y = trainRoofHeight;
            _yVel = 0f;
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, _laneIndex * laneOffset, laneSwitchSpeed * Time.deltaTime);
        pos.y = _y;
        pos.z = 0f;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Walkable"))
        {
            _walkableContacts++;
            _isOnTrain = true;
            return;
        }

        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Walkable"))
        {
            _walkableContacts--;

            if (_walkableContacts <= 0)
            {
                _walkableContacts = 0;
                _isOnTrain = false;
            }
        }
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            GameManager.Instance.TogglePause();
        }
    }
}