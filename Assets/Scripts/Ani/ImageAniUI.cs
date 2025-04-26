using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAniUI : MonoBehaviour
{
    public Image image;
    public float a_dTime;
    public float d_dTime;
    public float aniDelayTime;
    public float doneDelayTime;
    public bool isAni = false;
    public bool isZero = false;
    public int ind = -1;
    public List<ImageAni.ColorOneStep> anilist = new();
    public Color start
    {
        get => anilist[ind].startColor;
    }
    public Color end
    {
        get => anilist[ind].endColor;
    }
    public float d_t
    {
        get => anilist[ind].delayTime;
    }
    private void Start()
    {
        image = GetComponent<Image>();
        d_dTime = doneDelayTime;
    }
    private void Update()
    {
        a_dTime -= Time.deltaTime;
        if (isAni)
        {
            if (a_dTime <= 0f)
            {
                ind++;
                if (ind >= anilist.Count)
                {
                    ind = -1;
                    isAni = false;
                    return;
                }
                else
                {
                    image.color = anilist[ind].startColor;
                    a_dTime = anilist[ind].delayTime;
                }
            }
            image.color = GetGradation();
            if (ind == 0) image.fillAmount = (a_dTime > 0f ? a_dTime : 0f) / d_t;
            else if(ind > 0) image.fillAmount = 1;
        }
    }
    public Color GetGradation()
    {
        return new Color(
            GetColorValue(start.r, end.r, a_dTime, d_t),
            GetColorValue(start.g, end.g, a_dTime, d_t),
            GetColorValue(start.b, end.b, a_dTime, d_t),
            GetColorValue(start.a, end.a, a_dTime, d_t)
        );
    }
    public float GetColorValue(float s, float e, float dt, float t)
    {
        if (dt < 0f || t < 0) return e;
        var result = (s * (dt / t) + e * (1 - dt / t));
        return result < 0f ? 0f : (result > 1f ? 1f : result);
    }
    public void OnAni(float aniDelayTime)
    {
        isAni = true;
        anilist[0] = new(anilist[0].startColor, anilist[0].endColor, aniDelayTime);
    }
}