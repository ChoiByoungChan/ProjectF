using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxMaker : MonoBehaviour
{
    [SerializeField] private Material[] skyboxs;
    private Color[] fogColors = new Color[3];

    public TurnWaveLight waveAction;

    private void Start()
    {
        RenderSettings.skybox = skyboxs[0];
        RenderSettings.fog = false;
        fogColors[0] = new Color(0.22f, 0.28f, 0.6f, 0.5f);
        fogColors[1] = new Color(0.55f, 0.56f, 0.57f, 0.5f);
        fogColors[2] = new Color(0.58f, 0.24f, 0.23f, 0.5f);
        waveAction.NextWaveAction += SetSkybox;
        waveAction.NextWaveAction += FogSetting;
    }

    private void SetSkybox()
    {
        RenderSettings.skybox = skyboxs[GameManager.Instance.Wave];
    }

    private void FogSetting()
    {
        float wave = GameManager.Instance.Wave;
        if (wave == 0)
        {
            RenderSettings.fog = false;
        }
        else
        {
            if (!RenderSettings.fog)
                RenderSettings.fog = true;
            if (wave == 1 || wave == 2)
            {
                RenderSettings.fogColor = fogColors[0];
            }
            else if (wave == 3)
            {
                RenderSettings.fogColor = fogColors[1];
            }
            else if(wave == 4)
            {
                RenderSettings.fogColor = fogColors[2];
            }
        }
    }
}
