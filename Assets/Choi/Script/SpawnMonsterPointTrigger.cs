using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnMonsterPointTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] monsters;
    [SerializeField] TurnWaveLight nextAction;
    BoxCollider spawnCollider;

    private void Awake()
    {
        spawnCollider = GetComponent<BoxCollider>();
        spawnCollider.isTrigger = true;
        nextAction.NextWaveAction += SetTrigger;
    }

    private void SetTrigger()
    {
        spawnCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Instantiate(monsters[GameManager.Instance.Wave]);
            spawnCollider.isTrigger = false;
        }
    }
}
