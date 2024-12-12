using _ProjectTemplate.Scripts.Managers;
using _ProjectTemplate.Scripts.UI;
using DG.Tweening;
using GameTool.Audio.Scripts;
using GameToolSample.Audio;
using GameToolSample.Scripts.Layers_Tags;
using UnityEngine;
using Color = System.Drawing.Color;

namespace _ProjectTemplate.Scripts.GamePlay.Player
{
    public enum CharacterState
    {
        Idle,
        Running,
        Jumping,
        Die,
    }

    public class PlayerController : MonoBehaviour
    {
        #region Variable

        public CharacterState _characterState = CharacterState.Idle;

        [Header("SPRITE")] [SerializeField] private Animator _smallAnimator;
        [SerializeField] private Animator _bigAnimator;

        [Header("HEALTH")] [SerializeField] private PlayerHealth _playerHealth;

        [Header("MOVEMENT")] [SerializeField] private Rigidbody2D _rb2D;
        [SerializeField] private CapsuleCollider2D _capsuleCollider2D;

        private float inputAxis;

        private int currentCountJump;

        [Header("CONFIG")] [SerializeField] private int maxCountJump = 2;

        public float moveSpeed = 8f;
        public float maxJumpHeight = 5f;
        public float maxJumpTime = 1f;

        public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
        public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

        public bool IsOnGrounded { get; private set; }
        public bool Running => IsOnGrounded && (Mathf.Abs(Rb2D.velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f);

        private bool _isBig;

        private bool _isCollectedStar;

        private static readonly int State = Animator.StringToHash("State");

        #endregion

        #region Properties

        public Rigidbody2D Rb2D
        {
            get => _rb2D;
            private set => _rb2D = value;
        }

        public CapsuleCollider2D CapsuleCollider2D
        {
            get => _capsuleCollider2D;
            private set => _capsuleCollider2D = value;
        }

        public CharacterState CharacterState
        {
            get => _characterState;
            set => _characterState = value;
        }

        public Animator SmallAnimator => _smallAnimator;

        public Animator BigAnimator => _bigAnimator;

        public PlayerHealth PlayerHealth => _playerHealth;

        public bool IsBig
        {
            get => _isBig;
            private set => _isBig = value;
        }

        public bool IsCollectedStar => _isCollectedStar;

        #endregion


        #region Unity Function

        private void Awake()
        {
            if (!Rb2D)
            {
                Rb2D = GetComponent<Rigidbody2D>();
            }

            if (!CapsuleCollider2D)
            {
                CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
            }
        }

        private void Update()
        {
            if (!GameController.Instance.IsPlayingGame)
            {
                return;
            }

            if (PlayerHealth.IsDie)
            {
                return;
            }

            IsOnGrounded = Rb2D.Raycast(Vector2.down);

            if (IsOnGrounded)
            {
                ResetJump();
            }

            CheckMovement();
            CheckJump();
            CheckState();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!GameController.Instance.IsPlayingGame)
            {
                return;
            }

            if (PlayerHealth.IsDie)
            {
                return;
            }
            
            if (collision.gameObject.CompareTag(TagName.Mushroom))
            {
                Grow();
            }

            if (collision.gameObject.CompareTag(TagName.Star))
            {
                StarPower();
            }

            if (collision.gameObject.CompareTag(TagName.NextLevel))
            {
                GameController.Instance.NextLevel();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GameController.Instance.IsPlayingGame)
            {
                return;
            }

            if (PlayerHealth.IsDie)
            {
                return;
            }

            if (other.CompareTag(TagName.DieRange))
            {
                Die();
            }
        }

        #endregion

        #region API

        private void ResetJump()
        {
            currentCountJump = maxCountJump;
        }

        public void SetCollider(bool isBig)
        {
            Vector2 bigSize = new Vector2(0.8f, 2.1f);
            Vector2 bigOffset = new Vector2(0f, 1f);

            Vector2 smallSize = new Vector2(0.6f, 1.1f);
            Vector2 smallOffset = new Vector2(0f, 0.5f);

            if (isBig)
            {
                CapsuleCollider2D.size = bigSize;
                CapsuleCollider2D.offset = bigOffset;
            }
            else
            {
                CapsuleCollider2D.size = smallSize;
                CapsuleCollider2D.offset = smallOffset;
            }
        }

        public void Init()
        {
            PlayerHealth.Init();
            transform.position = GameController.Instance.StartPosition.position;
            Revive();
            SetCollider(false);
            _isBig = false;
            _isCollectedStar = false;
            SetState(CharacterState.Idle);
            ResetJump();
        }

        private void Revive()
        {
            Rb2D.bodyType = RigidbodyType2D.Dynamic;
            CapsuleCollider2D.enabled = true;
        }

        /// Sau khi ăn nấm
        public void Grow()
        {
            if (IsBig)
            {
                return;
            }

            _isBig = true;
            CheckBigOrSmall();
        }

        private void CheckBigOrSmall()
        {
            SetSpriteBigOrSmall(IsBig);
            SetCollider(IsBig);
        }

        private void SetSpriteBigOrSmall(bool isBig)
        {
            BigAnimator.gameObject.SetActive(isBig);
            SmallAnimator.gameObject.SetActive(!isBig);
        }

        public void Hit()
        {
            if (!PlayerHealth.IsDie && !IsCollectedStar)
            {
                if (IsBig)
                {
                    IsBig = false;
                    CheckBigOrSmall();
                }
                else
                {
                    Die();
                }
            }
        }

        /// Sau khi ăn ngôi sao
        public void StarPower()
        {
            if (!IsCollectedStar)
            {
                Animator animator = IsBig ? BigAnimator : SmallAnimator;
                var r = Random.Range(0, 1f);
                var g = Random.Range(0, 1f);
                var b = Random.Range(0, 1f);
                animator.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(r, g, b, 1);
                _isCollectedStar = true;
                DOVirtual.DelayedCall(10f, () =>
                {
                    _isCollectedStar = false;
                    BigAnimator.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 1, 1, 1);
                    SmallAnimator.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 1, 1, 1);
                });
            }
        }


        private void CheckState()
        {
            if (!IsOnGrounded)
            {
                SetState(CharacterState.Jumping);
            }
            else
            {
                if (Running)
                {
                    SetState(CharacterState.Running);
                }
                else
                {
                    SetState(CharacterState.Idle);
                }
            }
        }

        private void Die()
        {
            Rb2D.velocity = Vector2.zero;
            Rb2D.bodyType = RigidbodyType2D.Kinematic;
            CapsuleCollider2D.enabled = false;
            PlayerHealth.Die();
            SetState(CharacterState.Die);
            MenuGameplay.Instance.UpdateLives(PlayerHealth.Lives);
            AudioManager.Instance.Shot(eSoundName.PlayerDie);
        }

        public void SetState(CharacterState state)
        {
            CharacterState = state;

            CheckBigOrSmall();
            UpdateState();
        }

        private void UpdateState()
        {
            Animator animator = IsBig ? BigAnimator : SmallAnimator;
            switch (CharacterState)
            {
                case CharacterState.Idle:
                    animator.SetInteger(State, 0);
                    break;
                case CharacterState.Running:
                    animator.SetInteger(State, 1);
                    break;
                case CharacterState.Jumping:
                    animator.SetInteger(State, 2);
                    break;
                case CharacterState.Die:
                    _isBig = false;
                    CheckBigOrSmall();
                    SmallAnimator.SetInteger(State, 3);
                    break;
            }
        }

        public void AddLife()
        {
            PlayerHealth.AddLife();
        }

        #endregion


        #region Movement

        private void CheckMovement()
        {
            inputAxis = Input.GetAxis("Horizontal");
            Rb2D.velocity = new Vector2(inputAxis * moveSpeed * Time.deltaTime, Rb2D.velocity.y);

            if (Rb2D.velocity.x > 0f)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (Rb2D.velocity.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }

        private void CheckJump()
        {
            if (currentCountJump > 0 && Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        private void Jump()
        {
            currentCountJump--;
            Vector2 jumpDir = new Vector2(0, jumpForce);
            Rb2D.AddForce(jumpDir, ForceMode2D.Impulse);
            SetState(CharacterState.Jumping);
            AudioManager.Instance.Shot(eSoundName.Jump);
        }

        #endregion
    }
}