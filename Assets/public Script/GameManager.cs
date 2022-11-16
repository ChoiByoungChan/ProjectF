using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameObject shopCanvas;
    
    private int wave = 0;
    private int curWave;
    public int Wave
    {
        get { return wave; }
        set 
        {
            if(value > 4)
            {
                value = 4;
            }
            wave = value;

            if(wave != curWave)
            {
                shopCanvas.SetActive(true);
                curWave = wave;
            }
        }
    }
    private new void Awake()
    {
        base.Awake();
        curWave = Wave;
    }
}
