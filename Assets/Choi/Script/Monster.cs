using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Monster : MonoBehaviour, IHitable
{
    private float hp;
    public float atk;
    public M_ScriptableObject scriptable;
    public Animator animator;
    public CharacterController monsterController;
    public GameObject player;
    public bool isDead;

    public Collider[] attackCols;

    public float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                isDead = true;
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
        player = GameObject.Find("Player");
        SetStaters();
    }
    private void SetStaters()
    {
        Hp = scriptable.hp;
        atk = scriptable.atk;
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
