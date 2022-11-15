using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxMaker : MonoBehaviour
{
    [SerializeField] private Material[] skyboxs;
    [SerializeField] RenderSettings settings;

    private Color[] fogColors = new Color[3];
    private int wave;
    public int Wave { get { return wave; }
        set
        {
            if(value > 4)
            {
                value = 4;
            }
            wave = value;
            SetSkybox(wave);
            FogSetting(wave);
        }
    } // 나중에 ShopManager의 wave사용해서 변경

    private void Start()
    {
        RenderSettings.skybox = skyboxs[0];
        RenderSettings.fog = false;
        Wave = 0;
        fogColors[0] = new Color(0.22f, 0.28f, 0.6f, 0.5f);
        fogColors[1] = new Color(0.55f, 0.56f, 0.57f, 0.5f);
        fogColors[2] = new Color(0.58f, 0.24f, 0.23f, 0.5f);
    }

    private void Update() // 담에 지우기
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Wave++;
        }
    }

    private void SetSkybox(int num)
    {
        //RenderSettings.skybox = skyboxs[ShopManager.Instantiate.wave];
        RenderSettings.skybox = skyboxs[Wave];
    }

    private void FogSetting(int wave)
    {
        if(wave == 0)
        {
            RenderSettings.fog = false;
        }
        else
        {
            if (!RenderSettings.fog)
                RenderSettings.fog = !RenderSettings.fog;
            if (wave == 1 || wave ==2)
            {
                RenderSettings.fogColor = fogColors[0];
            }
            else if( wave == 3)
            {
                RenderSettings.fogColor = fogColors[1];
            }
            else
            {
                RenderSettings.fogColor = fogColors[2];
            }
        }
    }
}
