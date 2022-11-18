using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : M_AttackState
{
    public PurpleMonster p_monster;
    public P_Attack(GameObject gameObject) : base(gameObject)
    {
        gameObj = gameObject;
        p_monster = gameObject.GetComponent<PurpleMonster>();
    }

    public override void Enter()
    {
        p_monster.animator.SetTrigger("Attack");
        p_monster.attackCols[0].enabled = true;
    }

    public override void Update()
    {
        if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75f && monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Basic Attack"))
        {
            monster.SetState(p_monster.p_movement);
        }
    }

    public override void Exit()
    {
        p_monster.attackCols[0].enabled = false;
    }

}

public class P_TailAttack : P_Attack
{
    public P_TailAttack(GameObject gameObject) : base(gameObject)
    {
    }

    public override void Enter()
    {
        p_monster.animator.SetTrigger("TailAttack");
        p_monster.attackCols[1].enabled = true;
        p_monster.StartCor();
    }

    public override void Update()
    {
        if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f && monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Tail Attack"))
        {
            monster.SetState(p_monster.p_movement);
        }
    }

    public override void Exit()
    {
        p_monster.attackCols[1].enabled = false;
    }
}

