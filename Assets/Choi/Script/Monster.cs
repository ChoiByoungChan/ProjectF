using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class Monster : MonoBehaviour, IHitable
{
    [SerializeField]private float hp;
    //public GameObject nextLight;
    public float atk;
    public float speed;
    public M_ScriptableObject scriptable;
    public Animator animator;
    public CharacterController monsterController;
    public GameObject player;

    public Collider monsterCol;

    public Collider[] attackCols;

    public float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                SetState(new M_DieState(gameObject));
            }
        }
    }

    public IStater curState;
    public M_MoveState m_MoveState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        monsterController = GetComponent<CharacterController>();
        monsterCol = GetComponent<Collider>();
        player = GameObject.Find("Player");
        SetStaters();
    }
    private void SetStaters()
    {
        Hp = scriptable.hp;
        atk = scriptable.atk;
        speed = scriptable.speed;
    }

    public void SetState(IStater input)
    {
        if(curState != null)
        {
            curState.Exit();
        }
        curState = input;
        curState.Enter();
    }

    public void Hit(float damage)
    {
        Hp -= damage;
    }
}
