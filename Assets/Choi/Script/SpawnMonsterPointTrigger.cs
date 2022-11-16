using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnMonsterPointTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] monsters;
    [SerializeField] TurnWaveLight nextAction;
    [SerializeField] Transform monsterSpawn;
    BoxCollider spawnCollider;

    private void Awake()
    {
        spawnCollider = GetComponent<BoxCollider>();
        spawnCollider.isTrigger = true;
        nextAction.NextWaveAction += SetTrigger;
    }

    private void SetTrigger()
    {
        spawnCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Instantiate(monsters[GameManager.Instance.Wave],monsterSpawn.position,Quaternion.Euler(0,0,0));
            spawnCollider.enabled = false;
        }
    }
}
