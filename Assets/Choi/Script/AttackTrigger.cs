using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    IHitable hitable;
    public Monster monster;
    GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == monster.player)
        {
            target = other.gameObject;
            hitable = target.GetComponent<IHitable>();
            hitable.Hit(monster.atk);
        }
    }
}
