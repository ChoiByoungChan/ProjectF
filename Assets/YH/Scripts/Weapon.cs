using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;

    public void Attack(IHitable enemy)
    {
        enemy.Hit(player.Atk);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IHitable>() !=null)
        {
            IHitable enem;
            other.TryGetComponent<IHitable>(out enem);
            Attack(enem);
        }
    }
}
