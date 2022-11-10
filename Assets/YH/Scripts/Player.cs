using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private float axisX;
    private float axisZ;
    private float moveSpeed;
    private float maxhp;
    public bool isHit;
    public bool isAttackOver;
    public bool isGuardOver;
    public Vector3 moveInput;
    private IStater curState;
    private PlayerIdleState playerIdleState;
    private PlayerWalkState playerWalkState;
    private PlayerAttackState playerAttackState;
    private PlayerShieldState playerShieldState;
    private PlayerRollState playerRollState;
    private PlayerHitState playerHitState;
    private PlayerDieState playerDieState;
    [SerializeField]
    private float hp;
    public float Hp
    {
        get { return hp; }
        set 
        { hp = value; 
            if(hp < maxhp && isHit == false)
            {
                isHit = true;
                SetState(playerHitState);
            }
            if(hp<=0)
            {
                SetState(playerDieState);
                hp = maxhp;
            }
        }
    }
    public void SetState(IStater input)
    {
        if (curState != null)
        {
            curState.Exit();
        }
        curState = input;
        curState.Enter();
    }
    private void Awake()
    {
        maxhp = 100;
        hp = maxhp;
        moveSpeed = 5f;
        isAttackOver = true;
        isGuardOver = true;
        isHit = false;
        character = GetComponent<CharacterController>();
        playerIdleState = new PlayerIdleState(this.gameObject);
        playerWalkState = new PlayerWalkState(this.gameObject);
        playerAttackState = new PlayerAttackState(this.gameObject);
        playerShieldState = new PlayerShieldState(this.gameObject);
        playerHitState = new PlayerHitState(this.gameObject);
        playerDieState = new PlayerDieState(this.gameObject);
        playerRollState = new PlayerRollState(this.gameObject);
    }
    private void Start()
    {
        SetState(playerIdleState);
    }
    private void Update()
    {
        curState.Update();
        Move();
        Attack();
        Shield();
        Roll();
    }
    private void Move()
    {

        axisX = Input.GetAxis("Horizontal");
        axisZ = Input.GetAxis("Vertical");
        moveInput = new Vector3(axisX, 0, axisZ);
        if (moveInput.magnitude > 0 && isAttackOver)
        {
            character.Move(moveInput * moveSpeed * Time.deltaTime);
            SetState(playerWalkState);
        }
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAttackOver == true)
        {
            isAttackOver = false;
            SetState(playerAttackState);
        }
    }
    private void Shield()
    {
        if(Input.GetKeyDown(KeyCode.C) && isGuardOver == true)
        {
            isGuardOver = false;
            SetState(playerShieldState);
        }
    }
    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetState(playerRollState);
        }
    }
}
