using DG.Tweening;
using System.Numerics;
using TMPro;
using UnityEngine;

public class VisualTextGlowEffect : MonoBehaviour
{
    public Material textShader;
    public TextMeshProUGUI _textMeshPro;
    public float start;
    public float end = 1;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        textShader.SetFloat("_GlowPower", 0);
        FadeIn();
        GlowUp();
    }

    public void FadeIn()
    {
        DOTween.To(() => _textMeshPro.alpha, x => _textMeshPro.alpha = x, 1, 4).SetEase(Ease.InSine);

    }
    public void GlowUp()
    {
        textShader.DOFloat(end, "_GlowPower", duration).OnComplete(GlowDown).SetEase(Ease.InOutSine);
    }

    public void GlowDown()
    {
        textShader.DOFloat(start, "_GlowPower", duration).OnComplete(GlowUp).SetEase(Ease.InOutSine);
    }

    
}
