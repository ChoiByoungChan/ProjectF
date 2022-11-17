using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour,IHitable
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private TextMeshProUGUI hpText;
    private CharacterController character;
    public GameObject comboUI;
    public ParticleSystem swordEffect;
    public ParticleSystem shieldEffect;
    public ParticleSystem hitEffect;
    private float moveSpeed;
    public float comboNumber;
    public float maxhp;
    public float minAtk;
    public float minDef;
    public float hpRecovery;
    public float secondAtk;
    public float thirdAtk;
    public bool isHit;
    public bool isAttackOver;
    public bool isGuardOver;
    public Vector3 moveInput;
    public JoyStick joyStick;
    public Vector3 movePosition;
    private IStater curState;
    private PlayerIdleState playerIdleState;
    private PlayerWalkState playerWalkState;
    private PlayerAttackState playerAttackState;
    private PlayerAttackTwoState playerAttackTwoState;
    private PlayerShieldState playerShieldState;
    private PlayerRollState playerRollState;
    private PlayerHitState playerHitState;
    private PlayerDieState playerDieState;
    private WaitForSeconds fiveSeconds;
    private float hp;
    public float Hp
    {
        get { return hp; }
        set 
        {
            if (isGuardOver == false)
                return;
            if (hp < maxhp && isHit == false && value < 0)
            {
                hp = value / def;
                hpBar.value = hp;
                hpText.text = "HP : "+ hp;
                isHit = true;
                SetState(playerHitState);
                if (hp <= 0)
                {
                    SetState(playerDieState);
                    hp = maxhp;
                }
            }
            if(value > 0)
            {
                hp = value;
                hpBar.value = hp;
            }
        }
    }
    private float atk;
    public float Atk
    {
        get { return atk; }
        set 
        {
            atk = value;
        }
    }
    private float def;
    public float Def
    {
        get { return def; }
        set 
        { 
            def = value;
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
        comboNumber = 0;
        maxhp = 100;
        hp = maxhp;
        hpText.text = "HP : " + hp;
        minAtk = 1;
        atk = minAtk;
        minDef = 1;
        def = minDef;
        moveSpeed = 5f;
        hpRecovery = 0f;
        fiveSeconds = new WaitForSeconds(5f);
        isAttackOver = true;
        isGuardOver = true;
        isHit = false;
        character = GetComponent<CharacterController>();
        playerIdleState = new PlayerIdleState(this.gameObject);
        playerWalkState = new PlayerWalkState(this.gameObject);
        playerAttackState = new PlayerAttackState(this.gameObject);
        playerAttackTwoState = new PlayerAttackTwoState(this.gameObject);
        playerShieldState = new PlayerShieldState(this.gameObject);
        playerHitState = new PlayerHitState(this.gameObject);
        playerDieState = new PlayerDieState(this.gameObject);
        playerRollState = new PlayerRollState(this.gameObject);
    }
    private void Start()
    {
        SetState(playerIdleState);
        StartCoroutine(HpRecovery());
    }
    private void Update()
    {
        curState.Update();
        hpBar.maxValue = maxhp;
    }
    public void Move(Vector3 inputDirection)
    {
        if (isAttackOver)
        {
            inputDirection = transform.InverseTransformDirection(-inputDirection);
            character.Move(inputDirection * moveSpeed * Time.deltaTime);
            SetState(playerWalkState);
        }
    }
    public void Attack()
    {
        if (isAttackOver)
        {
            isAttackOver = false;
            SetState(playerAttackState);
        }
        else if (!isAttackOver)
        { 
            comboNumber++;
        }
        else if(!isAttackOver && comboNumber == 1)
        {
            comboNumber++;
        }
    }
    public void Shield()
    {
            isGuardOver = false;
            SetState(playerShieldState);
    }
    public void Roll()
    {
            SetState(playerRollState);
    }
    IEnumerator HpRecovery()
    {
        while (true)
        {
            yield return fiveSeconds;
            Hp += hpRecovery;
        }
    }
    public void Hit(float damage)
    {
        Hp -= damage;
    }
}

