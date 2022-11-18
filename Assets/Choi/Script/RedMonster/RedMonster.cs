using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMonster : Monster
{
    public R_Attack r_attack;
    public R_Movement r_movement;
    public R_Scream r_scream;
    public bool isCool;
    public bool isFlameCool;
    public bool isAngry;
    public ParticleSystem[] particles;

    IEnumerator startCo;

    float phaseHp;

    private void Start()
    {
        r_attack = new R_Attack(gameObject);
        r_movement = new R_Movement(gameObject);
        r_scream = new R_Scream(gameObject);
        m_die = new R_DieState(gameObject);
        isAngry = false;
        isFlameCool = true;
        isCool = true;
        SetState(r_movement);
        phaseHp = (scriptable.hp / 100)*30;
    }
    private void Update()
    {
        if(Hp <= phaseHp && !isAngry)
        {
            SetState(r_scream);
            r_movement = new R_2Movement(gameObject);
        }
        curState.Update();
    }

    public void StartCor(int num)
    {
        if(num ==1)
        {
            startCo = ClawSkillCool();
        }
        else if( num == 2)
        {
            startCo = FlameSkillCool();
        }
        StartCoroutine(startCo);
    }

    public IEnumerator ClawSkillCool()
    {
        yield return new WaitForSeconds(7);
        isCool = true;
    }

    public IEnumerator FlameSkillCool()
    {
        yield return new WaitForSeconds(15);
        isFlameCool = true;
    }
}
