using DG.Tweening;
using UnityEngine;

public class VisualTextGlowEffect : MonoBehaviour
{
    public Material textShader;
    public float start;
    public float end = 1;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        start = textShader.GetFloat("_GlowPower"); 
        GlowUp();
    }

    public void GlowUp()
    {
        textShader.DOFloat(end, "_GlowPower", duration).OnComplete(GlowDown).SetEase(Ease.InElastic);
    }

    public void GlowDown()
    {
        textShader.DOFloat(start, "_GlowPower", duration).OnComplete(GlowUp).SetEase(Ease.InElastic);
    }

    
}
