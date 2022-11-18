using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnWaveLight : MonoBehaviour
{
    public UnityAction NextWaveAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            GameManager.Instance.Wave++;
            NextWaveAction?.Invoke();
            //플레이어 위치 바꾸고
            gameObject.SetActive(false);
        }
    }
}