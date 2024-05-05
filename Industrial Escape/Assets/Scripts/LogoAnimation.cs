using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoAnimation : MonoBehaviour
{
  
    private Image image;
    private bool isScalingUp = true;
    private Vector3 initialScale;

    void Start()
    {
        image = GetComponent<Image>();
        initialScale = transform.localScale;
        StartLogoAnimation();
    }

    void StartLogoAnimation()
    {
        image.rectTransform.DOScale(transform.localScale * 1.2f, 1f).OnComplete(()=>
        {
            image.rectTransform.DOScale(initialScale, 1f).OnComplete(StartLogoAnimation);

        });
        
    }
}