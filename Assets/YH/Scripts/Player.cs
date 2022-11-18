using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour,IHitable
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField] private GameObject gameOverButton;
    private CharacterController character;
    public GameObject comboUI;
    public ParticleSystem swordEffect;
    public ParticleSystem shieldEffect;
    public ParticleSystem hitEffect;
    public float moveSpeed;
    public TurnWaveLight waveAction;
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
    private Vector3 gravity;
    public JoyStick joyStick;
    public Animator animator;
    public Vector3 movePosition;
    private IStater curState;
    public PlayerIdleState playerIdleState;
    private PlayerWalkState playerWalkState;
    private PlayerAttackState playerAttackState;
    public PlayerAttackTwoState playerAttackTwoState;
    private PlayerShieldState playerShieldState;
    private PlayerRollState playerRollState;
    private PlayerHitState playerHitState;
    private PlayerDieState playerDieState;
    private WaitForSeconds fiveSeconds;
    public PlayerAttackThreeState playerAttackThreeState;
    private float hp;
    public float Hp
    {
        get { return hp; }
        set 
        {
            {
                hp = value;
                HpUI();
                if (hp <= 0)
                {
                    SetState(playerDieState);
                    //hp = maxhp;
                }
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
        animator = GetComponent<Animator>();
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
        playerAttackThreeState = new PlayerAttackThreeState(this.gameObject);
    }
    private void Start()
    {
        SetState(playerIdleState);
        StartCoroutine(HpRecovery());
        waveAction.NextWaveAction += NextWaveSetPosition;
    }
    private void Update()
    {
        curState.Update();
        gravity.y += Physics.gravity.y;
        character.Move(gravity * Time.deltaTime);
        hpBar.maxValue = maxhp;
    }
    public void Move(Vector3 inputDirection)
    {
        if (isAttackOver)
        {
            movePosition = transform.InverseTransformDirection(inputDirection);
            character.Move(movePosition * moveSpeed * Time.deltaTime);
            SetState(playerWalkState);
        }
    }
    private void NextWaveSetPosition()
    {
        character.enabled = false;
        transform.position = new Vector3(-0.5f, 6.05f, 21.55f);
        character.enabled = true;
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
        if (isGuardOver || !isHit)
        {
            Hp -= (damage / Def);
            isHit = true;
            SetState(playerHitState);
        }
    }
    private void HpUI()
    {
        hpBar.maxValue = maxhp;
        hpBar.value = hp;
        hpText.text = "HP : " + hp;
    }

    public void PlayerDie()
    {
        Time.timeScale = 0.1f;
        gameOverButton.SetActive(true);
    }
}

