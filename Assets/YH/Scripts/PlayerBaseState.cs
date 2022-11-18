using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : BaseState
{
    public Player player;
    public PlayerBaseState(GameObject game) : base(game)
    {
        player = gameObj.GetComponent<Player>();
        this.gameObj = game;
    }
    public override void Enter()
    { }
    public override void Update()
    { }
    public override void Exit()
    { }
}
public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(GameObject game) : base(game)
    {
        
    }
    public override void Enter()
    {
        player.animator.SetBool("Idle", true);
        player.isAttackOver = true;
        player.comboNumber = 0;
    }
    public override void Exit()
    {
        player.animator.SetBool("Idle", false);
    }
}
public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(GameObject game) : base(game)
    {
        
    }
    public override void Enter()
    {
        player.animator.SetBool("Walk", true);
    }
    public override void Update()
    {
        if(player.moveInput.magnitude == 0 /*&& player.isAttackOver == true*/)
        {
            player.SetState(player.playerIdleState);
        }
    }
    public override void Exit()
    {
        player.animator.SetBool("Walk", false);
    }
}
public class PlayerAttackState : PlayerBaseState
{
    public GameObject comboUI;
   
    public PlayerAttackState(GameObject game) : base(game)
    {
        comboUI = player.comboUI;
    }
    public override void Enter()
    {
        player.animator.SetTrigger("Attack");
    }
    public override void Update()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
        {
            comboUI.SetActive(true);
            if(player.comboNumber == 1)
            {
                comboUI.SetActive(false);
                player.SetState(player.playerAttackTwoState);
            }
        }
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            comboUI.SetActive(false);
            player.SetState(player.playerIdleState);
        }
    }
}
public class PlayerAttackTwoState : PlayerBaseState
{
    public GameObject comboUI;
    public float playerSecondAtk;
    public PlayerAttackTwoState(GameObject game) : base(game)
    {
        playerSecondAtk = player.secondAtk;
        comboUI = player.comboUI;
    }
    public override void Enter()
    {
        player.Atk += playerSecondAtk;
        player.swordEffect.Play();
        player.animator.SetTrigger("Attack2");
    }
    public override void Update()
    {

        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
        {
            comboUI.SetActive(true);
            if (player.comboNumber == 2)
            {
                comboUI.SetActive(false);
                player.SetState(player.playerAttackThreeState);
            }
        }
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            comboUI.SetActive(false);
            player.SetState(player.playerIdleState);
        }
    }
    public override void Exit()
    {
        player.swordEffect.Stop();
        player.Atk -= playerSecondAtk;
    }
}
public class PlayerAttackThreeState : PlayerBaseState
{
    public float playerThirdAtk;    
    public PlayerAttackThreeState(GameObject game) : base(game)
    {
        
        playerThirdAtk = player.thirdAtk;
    }
    public override void Enter()
    {
        player.swordEffect.Play();
        player.Atk += playerThirdAtk;
        player.animator.SetTrigger("Attack3");
    }
    public override void Update()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            player.SetState(player.playerIdleState);
        }
    }
    public override void Exit()
    {
        player.swordEffect.Stop();
        player.Atk -= playerThirdAtk;
    }
}
public class PlayerShieldState : PlayerBaseState
{
    public PlayerShieldState(GameObject game) : base(game)
    {
    }
    public override void Enter()
    {
        player.animator.SetTrigger("Shield");
        player.shieldEffect.Play();
        player.bodyCollider.enabled = false;
    }
    public override void Update()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99)
        {
            player.SetState(player.playerIdleState);
        }
    }
    public override void Exit()
    {
        player.bodyCollider.enabled = true;
        player.shieldEffect.Stop();
    }
}
public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(GameObject game) : base(game)
    {
    }
    public override void Enter()
    {
        player.animator.SetTrigger("Roll");
    }
    public override void Update()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99)
        {
            player.SetState(player.playerIdleState);
        }
    }
}
public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(GameObject game) : base(game)
    {
    }
    public override void Enter()
    {
        player.hitEffect.Play();
        player.animator.SetTrigger("Hit");
        player.isHit = true;
    }
    public override void Update()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99)
        {
            player.SetState(player.playerIdleState);
            player.isHit = false;
            player.hitEffect.Stop();
        }
    }
}
public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(GameObject game) : base(game)
    {
    }
    public override void Enter()
    {
        player.animator.SetTrigger("Die");
        player.PlayerDie();
    }
}