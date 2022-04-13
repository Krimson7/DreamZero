using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerStateMachine : MonoBehaviour
{
    //StateVariable
    public playerBaseState _currentState;
    playerStateFactory _states;

    [Header("Components")]
    public Rigidbody2D _rb;
    PlayerInput _playerInput;
    public Animator _animator;
    // public Animator _animator2;
    public GameObject _characterHolder;
    public BoxCollider2D _boxCollider2d;
    public GameObject _A1_Hitbox;
    public BoxCollider2D _A1_HitboxCollider;

    [Header("Inputs")]
    private Vector2 _currentMovementInput;
    [SerializeField]bool _isMovementPressed = false;
    [SerializeField]bool _isJumpHeld = false;
    // bool _jumpKeyDown = false;
    bool _isAttack1Held = false;
    public bool _Attack1KeyDown = false;
    [SerializeField]bool _requireNewJumpPress = false;


    [Header("Movement")]
    public Vector2 _currentMovement;
    public float _moveSpeed = 10f;
    public float _maxSpeed = 4f;
    private bool _facingRight = true;
    public float _jumpSpeed = 8f;
    public float _jumpDelay = 0.25f;
    private float _jumpTimer;

    [Header("Physics")]
    public float _linearDrag = 2f;
    public float _gravity = 1f;
    public float _fallMultiplier = 5f;

    [Header("Attacking")]
    // [SerializeField] private char attackKey = 'l';
    [SerializeField] private float _attackDelay = 0.3f;
    public float _atk = 10f;

    [Header("Collision")]
    public bool _onGround = false;
    public float _groundLength = 0.1f;
    public Vector3 _colliderOffset;
    public LayerMask _groundLayer;
    public LayerMask _enemyLayer;
    ContactFilter2D _enemyContactFilter = new ContactFilter2D();
    

    [Header("Animation")]
    // private bool _isFalling = false;
    private bool _isAttacking = false;
    // private bool _isAttackPressed = false;  
    [SerializeField] private string _currentAnimaton;
    public const string PLAYER_IDLE = "Player_Idle";
    public const string PLAYER_RUN = "Player_Running";
    // public const string PLAYER_JUMP = "Player_Jump";
    public const string PLAYER_FALL = "Player_Fall";
    public const string PLAYER_ATTACK = "Player_Attack";
    public const string PLAYER_AIR_ATTACK = "Player_Air_Attack";

    //state get
    public playerBaseState CurrentState {get {return _currentState; } set {_currentState = value; } }
    
    //Component gets 
    public Rigidbody2D rb {get {return _rb;} set {_rb = value;}}
    public PlayerInput playerInput {get {return _playerInput;}}
    public Animator animator {get {return _animator;}}
    // public Animator animator2 {get {return _animator2;}}
    public GameObject characterHolder {get {return _characterHolder;}}
    public BoxCollider2D boxCollider2d {get {return _boxCollider2d;}}
    public GameObject A1_HitBox {get {return _A1_Hitbox;}}

    //Input gets
    public bool isJumpHeld {get{return _isJumpHeld;}}
    public bool requireNewJumpPress {get{return _requireNewJumpPress;} set {_requireNewJumpPress = value;}}
    public bool isMovementPressed {get {return _isMovementPressed;}}
    // bool _jumpKeyDown = false;
    public bool isAttack1Held {get{return _isAttack1Held;}}
    public bool Attack1KeyDown {get{return _Attack1KeyDown;}}
    
    //Movement Gets
    public Vector2 currentMovement {get {return _currentMovement;}}
    public float moveSpeed {get{return _moveSpeed = 10f;}}
    public float maxSpeed {get{return _maxSpeed;}}
    public bool facingRight {get{return _facingRight;} set {_facingRight = value;}}
    public float jumpSpeed {get{return _jumpSpeed;}}
    public float jumpDelay {get{return _jumpDelay;}}

    //Physics gets
    public float linearDrag {get {return _linearDrag;}}
    public float gravity {get {return _gravity;}}
    public float fallMultiplier {get {return _fallMultiplier;}}

    //Collision gets
    public bool onGround {get {return _onGround;}}
    public LayerMask groundLayer {get{return _groundLayer;}}
    public LayerMask enemyLayer {get {return _enemyLayer;}}
    public ContactFilter2D enemyContactFilter {get {return _enemyContactFilter;}}

    //Attack gets
    public bool isAttacking {get{return _isAttacking;} set{_isAttacking = value;}}
    public float attackDelay {get{return _attackDelay;}}
    public float atk {get{return _atk;}}




    void onMovementInput (InputAction.CallbackContext context){
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.y = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x !=0 || _currentMovementInput.y !=0;
    }

    void onJumpKeyDown (InputAction.CallbackContext context){
        // _jumpKeyDown = true;
        _requireNewJumpPress = false;
        _isJumpHeld = context.ReadValueAsButton();
    }
    void onJumpKeyUp (InputAction.CallbackContext context){
        // _jumpKeyDown = false;
        _requireNewJumpPress = false;
        _isJumpHeld = context.ReadValueAsButton();
    }
    void onAttack1KeyDown (InputAction.CallbackContext context){
        _Attack1KeyDown = true;
        _isAttack1Held = context.ReadValueAsButton();
    }
    void onAttack1KeyUp (InputAction.CallbackContext context){
        _Attack1KeyDown = false;
        _isAttack1Held = context.ReadValueAsButton();
    }

    void Awake(){
        _playerInput = new PlayerInput();
        // 

        //setup states
        _states = new playerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Jump.started += onJumpKeyDown;
        _playerInput.CharacterControls.Jump.canceled += onJumpKeyUp;
        _playerInput.CharacterControls.Attack1.started += onAttack1KeyDown;
        _playerInput.CharacterControls.Attack1.canceled += onAttack1KeyUp;
    }
    // Start is called before the first frame update
    void Start()
    {
        _A1_HitboxCollider = _A1_Hitbox.GetComponent<BoxCollider2D>();
        _enemyContactFilter.SetLayerMask(_enemyLayer);
    }

    // Update is called once per frame
    void Update()
    {
        _onGround = isGrounded();
        _currentState.UpdateStates();
    }

    private bool isGrounded(){                                              //Check Ground function
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider2d.bounds.center - new Vector3(0f,_boxCollider2d.bounds.extents.y,0f), new Vector2(_boxCollider2d.bounds.size.x,_boxCollider2d.bounds.size.y/4), 0f, Vector2.down, _colliderOffset.y, _groundLayer);
        
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        }
        else{
            rayColor = Color.red;
        }

        // Debug.DrawRay(_boxCollider2d.bounds.center + new Vector3(_boxCollider2d.bounds.extents.x, 0), Vector2.down * (_boxCollider2d.bounds.extents.y + _colliderOffset.y), rayColor);
        // Debug.DrawRay(_boxCollider2d.bounds.center - new Vector3(_boxCollider2d.bounds.extents.x, 0), Vector2.down * (_boxCollider2d.bounds.extents.y + _colliderOffset.y), rayColor);
        // Debug.DrawRay(_boxCollider2d.bounds.center - new Vector3(_boxCollider2d.bounds.extents.x, _boxCollider2d.bounds.extents.y + _colliderOffset.y), Vector2.right * (_boxCollider2d.bounds.extents.x), rayColor);
        return raycastHit.collider != null;
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    void AttackComplete()
    {
        _isAttacking = false;
    }

}
