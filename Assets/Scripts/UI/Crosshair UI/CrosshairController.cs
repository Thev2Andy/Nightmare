using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image Crosshair;

    [Space]

    public int DefaultCrosshair;
    public Color DefaultCrosshairColor;
    public Vector2 DefaultCrosshairScale;

    [Space]
    
    public Sprite[] CrosshairSprites;

    public static CrosshairController Instance;

    private void Awake()
    {
       if(Instance != this) Destroy(Instance);
       Instance = this;
    }

    private void Start()
    {
       ChangeCrosshair(DefaultCrosshair, DefaultCrosshairColor, DefaultCrosshairScale);
    }

    public void ChangeCrosshair(int CrosshairID, Color CrosshairColor, Vector2 CrosshairScale)
    {
        if(CrosshairID < 0 || CrosshairID > CrosshairSprites.Length)
        {
            Crosshair.sprite = null;
            Crosshair.color = Color.white;
            Crosshair.rectTransform.sizeDelta = new Vector2(22.5f, 22.5f);
            return;
        }

        SetCrosshairActive(true);

        Crosshair.sprite = CrosshairSprites[CrosshairID];
        Crosshair.color = CrosshairColor;
        Crosshair.rectTransform.sizeDelta = CrosshairScale;
    }

    public void SetCrosshairActive(bool Active)
    {
        Crosshair.enabled = Active;
    }
}
