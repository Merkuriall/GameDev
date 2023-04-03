using Core.Enums;
using UnityEngine;
using Core.Tools;


namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Direction _direction;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;

        [SerializeField] private DirectionalCameraPair _cameras;
        
        private Rigidbody2D _rigidbody;
        
        private bool _inAir;
        private Vector2 _force;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && !_inAir)
            {
                _force = new Vector2(0, 400);
                _inAir = true;
                _rigidbody.AddForce(_force);
            }
        }

        public void MoveHorizontally(float direction)
        {
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }

        private void SetDirection(float direction)
        {
            if ((_direction == Direction.Right && direction < 0) ||
                (_direction == Direction.Left && direction > 0))
                Flip();


        }

       
        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = cameraPair.Key == _direction;
            {
                
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                _inAir = false;
        }

        
    }  
}


