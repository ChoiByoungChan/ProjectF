using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class M_DieState : MonsterBaseState
{
    GameObject target;
    public M_DieState(GameObject gameObject) : base(gameObject)
    {
    }
    public override void Enter()
    {
        monster.animator.SetTrigger("Die");
        monster.monsterCol.enabled = false;
        target = GameObject.FindObjectOfType<TurnWaveLight>(true).gameObject;
    }
    public override void Update()
    {
        if(monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1)
        {
            target.SetActive(true);
            MonoBehaviour.Destroy(gameObj);
        }
    }
}
