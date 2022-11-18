using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Shop : MonoBehaviour
{
    public Button[] buttons;
    public Item item;
    public Action[] getItem = new Action[3];
    public Image[] images;
    public TextMeshProUGUI[] names;
    public TextMeshProUGUI[] tooltips;
    public ItemData[] items;
    [SerializeField]
    private int random;    

    /* private void Awake()
     {
         for (int i = 0; i < buttons.Length; i++)
             buttons[i].onClick.AddListener(()=> { getItem[i].Invoke(); });
     }*/
    private void OnEnable()
    {
        StartCoroutine(RandomShop());
    }
    private void Update()
    {
        random = UnityEngine.Random.Range(0, items.Length);
    }
    public void OnClickButton1()
    {
        getItem[0].Invoke();
        gameObject.SetActive(false);
    }
    public void OnClickButton2()
    {
        getItem[1].Invoke();
        gameObject.SetActive(false);
    }
    public void OnClickButton3()
    {
        getItem[2].Invoke();
        gameObject.SetActive(false);
    }
    public void OnClikExit()
    {
        gameObject.SetActive(false);
    }
    IEnumerator RandomShop()
    {
        
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = items[random].Icon;
            names[i].text = items[random].Name;
            tooltips[i].text = items[random].Tooltip;
            getItem[i] = item.itemDic[items[random]];
            yield return new WaitForSeconds(0.5f);
            
        }
    }
}
