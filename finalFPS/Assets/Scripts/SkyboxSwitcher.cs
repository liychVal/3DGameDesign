using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSwitcher : MonoBehaviour
{
    public Material[] skyboxMaterials;
 
    private int currentSkyboxIndex = 0;
 
 
 
    void Start()
 
    {
 
        RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex]; // 初始设置天空盒
 
    }
 
 
 
    void Update()
 
    {
 
        // 'Q'键
 
        if (Input.GetKeyDown(KeyCode.Q))
 
        {
 
            // 切换到下一个天空盒
 
            SwitchSkybox();
 
        }
 
    }
 
 
 
    void SwitchSkybox()
 
    {
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxMaterials.Length; 
        RenderSettings.skybox = skyboxMaterials[currentSkyboxIndex];
    }

}
