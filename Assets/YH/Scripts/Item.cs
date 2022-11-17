using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Item : Singleton<Item>
{
    public Player player;
    public ItemData[] Data;
    public Dictionary<ItemData, Action> itemDic = new Dictionary<ItemData, Action>();

    private new void Awake()
    {
        base.Awake();
        itemDic.Add(Data[0], HpUp1); 
        itemDic.Add(Data[1], HpUp2);
        itemDic.Add(Data[2], AtkUp1);
        itemDic.Add(Data[3], AtkUp2);
        itemDic.Add(Data[4], ComboUp1);
        itemDic.Add(Data[5], ComboUp2);
        itemDic.Add(Data[6], DefUp1);
        itemDic.Add(Data[7], DefUp2);
        itemDic.Add(Data[8], HpRecoveryUp1); 
        itemDic.Add(Data[9], HpRecoveryUp2);
    }
    public void HpUp1()
    {
        player.maxhp += 50;
        player.Hp = player.maxhp;
        Debug.Log("체력업1");
    }
    public void HpUp2()
    {
        player.maxhp += 100;
        player.Hp = player.maxhp;
        Debug.Log("체력업2");
    }
    public void AtkUp1()
    {
        player.Atk += 1;
        Debug.Log("공업1");
    }
    public void AtkUp2()
    {
        player.Atk += 5;
        Debug.Log("공업2");
    }
    public void ComboUp1()
    {
        player.Atk += 1;
        player.secondAtk += 3;
        player.thirdAtk += 5;
        Debug.Log("콤보업1");
    }
    public void ComboUp2()
    {
        player.Atk += 3;
        player.secondAtk += 5;
        player.thirdAtk += 10;
        Debug.Log("콤보업2");
    }
    public void DefUp1()
    {
        player.Def += 1;
        Debug.Log("방업1");
    }
    public void DefUp2()
    {
        player.Def += 3;
        Debug.Log("방업2");
    }
    public void HpRecoveryUp1()
    {
        player.hpRecovery += 1;
        Debug.Log("체젠1");
    }
    public void HpRecoveryUp2()
    {
        player.hpRecovery += 3;
        Debug.Log("체젠2");
    }
}
