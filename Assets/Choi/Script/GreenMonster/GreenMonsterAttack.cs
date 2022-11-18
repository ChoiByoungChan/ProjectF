using UnityEngine;

public class GreenMonsterAttack : M_AttackState
{
    public GreenMonster greenMonster;
    public GreenMonsterAttack(GameObject gameObject) : base(gameObject)
    {
        gameObj = gameObject;
        greenMonster = gameObj.GetComponent<GreenMonster>();
    }
    public override void Enter()
    {
        greenMonster.attackCols[0].enabled = true;
        greenMonster.animator.SetTrigger("HandAttack");
        
    }
    public override void Update()
    {
        if (greenMonster.animator.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack") && greenMonster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            greenMonster.SetState(greenMonster.g_MoveState);
        }
    }

    public override void Exit()
    {
        greenMonster.attackCols[0].enabled = false;
    }
}

public class GreenMonsterRushAttack : GreenMonsterAttack
{
    public GreenMonsterRushAttack(GameObject gameObject) : base(gameObject)
    {
    }
    public override void Enter()
    {
        greenMonster.attackCols[1].enabled = true;
        greenMonster.animator.SetTrigger("RushAttack");
        greenMonster.StartCor();
    }
    public override void Update()
    {
        if (greenMonster.animator.GetCurrentAnimatorStateInfo(0).IsName("Horn Attack") && greenMonster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            greenMonster.SetState(greenMonster.g_MoveState);
        }
    }

    public override void Exit()
    {
        greenMonster.attackCols[1].enabled = false;
        greenMonster.isRush = false;
    }
}
