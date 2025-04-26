using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//자체 애니매이션 클래스
public class ImageAni : MonoBehaviour
{
    public Unit thisUnit
    {
        get => GetComponent<Unit>();
    }
    public MobsItem thisMobsItem
    {
        get => GetComponent<MobsItem>();
    }
    public SpriteRenderer spriteRenderer;
    public Color origin = Color.white;
    public float a_dTime;
    public int ind = -1;
    public bool isAni = false;
    public bool isAniEnd = false;
    public List<ColorOneStep> anilist = new();
    public List<System.Action> actlist = new();
    public Color start
    {
        get => anilist[ind].startColor == Color.white ? origin : anilist[ind].startColor;
    }
    public Color end
    {
        get => anilist[ind].endColor == Color.white ? origin : anilist[ind].endColor;
    }
    public float d_t
    {
        get => anilist[ind].delayTime;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        a_dTime -= Time.deltaTime;
        if (isAni)
        {
            if (a_dTime <= 0f)
            {
                if (ind >= 0) actlist[ind].Invoke();
                ind++;
                if (ind >= anilist.Count)
                {
                    ind = -1;
                    isAni = false;
                    spriteRenderer.color = origin;
                    return;
                }
                else
                {
                    Debug.Log(anilist.Count + " " + actlist.Count);
                    spriteRenderer.color = anilist[ind].startColor;
                    a_dTime = anilist[ind].delayTime / (thisUnit?.timeSpeed ?? GetComponent<MobsItem>().timeSpeed);
                }
            }
            Debug.Log($"ind{ind}");
            spriteRenderer.color = GetGradation();
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
    public void OnAni()
    {
        isAni = true;
    }
    public void OnAni(List<ColorOneStep> nlist, List<System.Action> clist)
    {
        anilist = nlist;
        actlist = clist;
        if (thisUnit != null)
        {
            origin = thisUnit.originColor;
            Debug.Log(this.gameObject.name + ",unit" + ":" + OnColor(origin));
        }
        else if (thisMobsItem != null)
        {
            origin = thisMobsItem.originColor;
            Debug.Log(this.gameObject.name + ",mitem" + ":" + OnColor(origin));
        }
        else
        {
            origin = Color.white;
            Debug.Log(this.gameObject.name + "white" + ":" + OnColor(origin));
        }
        isAni = true;
    }
    public string OnColor(Color color)
    {
        return $"{color.r}, {color.g}, {color.b}, {color.a}";
    }
    [System.Serializable]
    public struct ColorOneStep
    {
        public Color startColor;
        public Color endColor;
        public float delayTime;
        public ColorOneStep(Color start, Color end, float dt)
        {
            startColor = start;
            endColor = end;
            delayTime = dt;
        }
        public static ColorOneStep GetDefault()
        {
            return new(Color.black, Color.black, 0f);
        }
    }
}