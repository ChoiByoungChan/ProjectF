using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SandBag : MonoBehaviour, IHitable
{
    Rigidbody rigi;
    GameObject player;
    Vector3 forceVec;

    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        forceVec = player.transform.localPosition - transform.localPosition;
    }
    public void Hit(float damage)
    {
        rigi.AddForce(forceVec * -1 * damage);
    }
}
