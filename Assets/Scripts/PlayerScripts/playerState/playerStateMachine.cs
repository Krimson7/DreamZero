// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerStateMachine : MonoBehaviour
{
    //StateVariable
    playerBaseState _currentState;
    playerBaseState _prevState;
    playerStateFactory _states;

    [Header("Components")]
    public Rigidbody2D _rb;
    PlayerInput _playerInput;
    // public Animator _animator;
    // public Animator _animator2;
    public GameObject _characterHolder;
    // public playerUseSpirit playerForm;
    public BoxCollider2D _boxCollider2d;
    public GameObject _A1_Hitbox;
    public BoxCollider2D _A1_HitboxCollider;
    public PlayerHp _playerHp;
    PlayerManaBlocks _playerMana;
    public playerEffectController _playerEffectController;
    public Transform _effectSpawnPoint;

    [Header("Inputs")]
    private Vector2 _currentMovementInput;
    [SerializeField]bool _isMovementPressed = false;
    [SerializeField]bool _isJumpHeld = false;
    // bool _jumpKeyDown = false;
    bool _isAttack1Held = false;
    public bool _Attack1KeyDown = false;
    public bool _ParryKeyDown = false;
    [SerializeField]bool _requireNewJumpPress = false;
    public float _wallJumpTimer = 0.2f;
    public float _wallJumpCounter = 0f;
    public bool interactKeyDown = false;
    public bool isInteractPressedAfterEnterTrigger = true;
    public bool foundInteractable = false;
    public I_interactable interact;


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
    public bool _clinging;
    public float _groundLength = 0.1f;
    public Vector3 _colliderOffset;
    public LayerMask _groundLayer;
    public LayerMask _enemyLayer;
    ContactFilter2D _enemyContactFilter = new ContactFilter2D();
    public Transform _clingPoint;
    

    [Header("Animation")]
    // private bool _isFalling = false;
    private bool _isAttacking = false;
    // private bool _isAttackPressed = false;  
    // [SerializeField] private string _currentAnimaton;
    // public const string PLAYER_IDLE = "Player_Idle";
    // public const string PLAYER_RUN = "Player_Running";
    // // public const string PLAYER_JUMP = "Player_Jump";
    // public const string PLAYER_FALL = "Player_Fall";
    // public const string PLAYER_ATTACK = "Player_Attack";
    // public const string PLAYER_AIR_ATTACK = "Player_Air_Attack";
    
    [Header("Player Variables")]
    // public float _Hp = 100f;
    public bool _isParrying = false;
    public bool _canParry = true;
    public bool _parryState = false;
    bool _parrySuccess = false;
    [SerializeField] private float _parryTimer = 0f;
    [SerializeField] private float _parrySuccessCD = 0.5f;
    [SerializeField] private float _parryFullCD = 1f;

    bool _isInvincible = false;
    [SerializeField] float _invincibleTime = 0.5f;
    [SerializeField] float _invincibleTimer = 1f;
    [SerializeField] float _knockbackForce = 0.5f;
    

    
    // private enum State {
    //     Base,
    //     Parry,
    // }
    
    // [SerializeField] private State _state;

    //state get
    public playerBaseState currentState {get {return _currentState; } set {_currentState = value; } }
    public playerBaseState prevState {get {return _prevState; } set {_prevState = value; } }
    
    //Component gets 
    public Rigidbody2D rb {get {return _rb;} set {_rb = value;}}
    public PlayerInput playerInput {get {return _playerInput;}}
    // public Animator animator {get {return _animator;}}
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
    public float wallJumpTimer {get{return _wallJumpTimer;}}
    public float wallJumpCounter {get{return _wallJumpCounter;} set {_wallJumpCounter = value;}}
    
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
    public bool clinging {get {return _clinging;}}
    public LayerMask groundLayer {get{return _groundLayer;}}
    public LayerMask enemyLayer {get {return _enemyLayer;}}
    public ContactFilter2D enemyContactFilter {get {return _enemyContactFilter;}}

    //Attack gets
    public bool isAttacking {get{return _isAttacking;} set{_isAttacking = value;}}
    public float attackDelay {get{return _attackDelay;}}
    public float atk {get{return _atk;}}

    //Parry gets
    public bool ParryKeyDown {get{return _ParryKeyDown;}}
    public bool isParrying {get{return _isParrying;} set{_isParrying = value;}}                 
    public bool canParry {get{return _canParry;} set{_canParry = value;}}                       //can parry
    public bool parrySuccess {get{return _parrySuccess;} set{_parrySuccess = value;}}           //if parry is successful
    public float parryTimer {get{return _parryTimer;} set{_parryTimer = value;}}                //CD timer
    public float parrySuccessCD {get{return _parrySuccessCD;} set{_parrySuccessCD = value;}}    //CD if success parry
    public float parryFullCD {get{return _parryFullCD;} set{_parryFullCD = value;}}             //CD if failed parry

    //gotHit gets
    public bool isInvincible {get{return _isInvincible;} set{_isInvincible = value;}}
    public float invincibleTimer {get{return _invincibleTimer;} set{_invincibleTimer = value;}}

    public float knockbackForce {get{return _knockbackForce;}}
    
    //PlayerVar gets
    // public float playerHp {get{return _Hp;} set{_Hp = value;}}



    void onMovementInput (InputAction.CallbackContext context){
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.y = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x !=0 || _currentMovementInput.y !=0;
    }

    void onJumpKeyDown (InputAction.CallbackContext context){
        // _jumpKeyDown = true;
        // _requireNewJumpPress = false;
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
    void onParryKeyDown (InputAction.CallbackContext context){
        _ParryKeyDown = true;
    }
    void onParryKeyUp (InputAction.CallbackContext context){
        _ParryKeyDown = false;
    }
    void onInteractKeyDown(InputAction.CallbackContext context){
        interactKeyDown = true;
        // if(foundInteractable) isInteractPressedAfterEnterTrigger = true;
        if(foundInteractable) interact.Interact(this);
    }
    void onInteractKeyUp(InputAction.CallbackContext context){
        interactKeyDown = false;
        isInteractPressedAfterEnterTrigger = false;
        
    }
    void Awake(){
        _playerInput = new PlayerInput();
        _characterHolder = transform.Find("ChaHolder").gameObject;
        _playerHp = GetComponent<PlayerHp>();
        _playerMana = GetComponent<PlayerManaBlocks>();
        _playerEffectController = _characterHolder.GetComponent<playerEffectController>();
        // 

        //setup states
        _states = new playerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        // _state = State.Base;

        _playerInput.CharacterControls.Move.started += onMovementInput;
        _playerInput.CharacterControls.Move.canceled += onMovementInput;
        _playerInput.CharacterControls.Move.performed += onMovementInput;
        _playerInput.CharacterControls.Jump.started += onJumpKeyDown;
        _playerInput.CharacterControls.Jump.canceled += onJumpKeyUp;
        _playerInput.CharacterControls.Attack1.started += onAttack1KeyDown;
        _playerInput.CharacterControls.Attack1.canceled += onAttack1KeyUp;
        _playerInput.CharacterControls.Parry.started += onParryKeyDown;
        _playerInput.CharacterControls.Parry.canceled += onParryKeyUp;
        _playerInput.CharacterControls.Interact.started += onInteractKeyDown;
        _playerInput.CharacterControls.Interact.canceled += onInteractKeyUp;

        
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
        _clinging = isClinging();
        _currentState.UpdateStates();
        checkInvincible();
        checkParry();
    }

    private bool isGrounded(){                                              //Check Ground function
        RaycastHit2D raycastHit = 
        Physics2D.BoxCast(_boxCollider2d.bounds.center - new Vector3(0f,_boxCollider2d.bounds.extents.y,0f), 
        new Vector2(_boxCollider2d.bounds.size.x,_boxCollider2d.bounds.size.y/4), 
        0f, Vector2.down, _colliderOffset.y, _groundLayer);
        
        // Color rayColor;
        // if(raycastHit.collider != null){
        //     rayColor = Color.green;
        // }
        // else{
        //     rayColor = Color.red;
        // }

        // Debug.DrawRay(_boxCollider2d.bounds.center + new Vector3(_boxCollider2d.bounds.extents.x, 0), Vector2.down * (_boxCollider2d.bounds.extents.y + _colliderOffset.y), rayColor);
        // Debug.DrawRay(_boxCollider2d.bounds.center - new Vector3(_boxCollider2d.bounds.extents.x, 0), Vector2.down * (_boxCollider2d.bounds.extents.y + _colliderOffset.y), rayColor);
        // Debug.DrawRay(_boxCollider2d.bounds.center - new Vector3(_boxCollider2d.bounds.extents.x, _boxCollider2d.bounds.extents.y + _colliderOffset.y), Vector2.right * (_boxCollider2d.bounds.extents.x), rayColor);
        return raycastHit.collider != null;
    }

    private bool isClinging(){
        bool Clingable = Physics2D.OverlapCircle(_clingPoint.position, 0.1f, _groundLayer);
        return Clingable;
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

    public void ParryComplete(){
        _isParrying = false;
        _parrySuccess = false;
        _canParry = true;
        _parryTimer = 0;
        _parryState = false;
        // Debug.Log("set false by completeParry");
    }


    void checkParry(){
        if(_parryTimer < _parryFullCD){
            _parryTimer += Time.deltaTime;
        }
        if(!_isParrying) return;

        if(_parryTimer > _parrySuccessCD){
            if(_parrySuccess){
                ParryComplete();
            }else{
                // Debug.Log("set false by failed parry");
                _isParrying = false;
            }
        }
        
    }

    void checkInvincible(){
        if(_isInvincible){
            if(_invincibleTimer <= _invincibleTime){
                _invincibleTimer += Time.deltaTime;
            }else{
                _isInvincible = false;
            }
        }else _invincibleTimer = 0;
    }

    public void checkTakeDamage(float damage){
        checkTakeDamage(damage, Vector2.up);
    }

    public void checkTakeDamage(float damageDone, Vector2 hitDirection){
        if(_isParrying && !_parrySuccess){
            UnityEngine.Debug.Log("Parried");
            // _parryTimer = _parrySuccessCD/2;
            _playerEffectController.playParryHitEffect(_effectSpawnPoint.position);
            _parrySuccess = true;
            _playerMana.Getmana(1);
            _playerEffectController.playGainManaEffect(transform.position);
            return;
        }
        if(_parrySuccess) return;
        if(_isInvincible) return;
        // _parryTimer = _parryFullCD;

        _playerHp.takeDamage(damageDone);
        int fr = _facingRight?-1:1;
        rb.AddForce(Vector2.up * 1, ForceMode2D.Impulse);
        rb.AddForce(hitDirection * -_knockbackForce, ForceMode2D.Impulse);
        UnityEngine.Debug.Log("player took damage");
        _isInvincible = true;
        // StartCoroutine("invincibleTime");        
    }


    // IEnumerator invincibleTimeCoroutine(){
    //     _isInvincible = true;
    //     UnityEngine.Debug.Log("player invincible");
        
    //     yield return new WaitForSeconds(_invincibleTimer);
    //     _isInvincible = false;
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        // isInteractPressedAfterEnterTrigger = true;
        var interactable = other.GetComponent<I_interactable>();
        if(interactable != null) {
            foundInteractable = true;
            interact = interactable;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        var interactable = other.GetComponent<I_interactable>();
        if(interactable != null) foundInteractable = false;
        
    }
}
