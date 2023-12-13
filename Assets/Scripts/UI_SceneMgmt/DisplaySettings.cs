using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DisplaySettings : MonoBehaviour
{
    public UniversalRenderPipelineAsset urpHighFidel;
    public void QualitySettingChange(string value)
    {
        switch(value)
        {
            case "low":
                urpHighFidel.renderScale = 0.172f;
                break;
            case "medium":
                urpHighFidel.renderScale = 0.8f;
                break;
            case "high":
                urpHighFidel.renderScale = 1f;
                break;
            case "ultra":
                urpHighFidel.renderScale = 1.5f;
                break;
        }
    }
}
