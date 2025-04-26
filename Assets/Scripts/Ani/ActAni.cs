using System.Collections.Generic;
using UnityEngine;

public class ActAni : MonoBehaviour
{
    public Unit thisUnit;
    public SpriteRenderer spriteRenderer;
    public Sprite origin;
    public float a_dTime;
    public int ind = -1;
    public bool isAni = false;
    public bool isLoop = false;
    public List<FrameSprite> anilist = new();
    public List<System.Action> actlist = new();
    public Sprite sprite
    {
        get => anilist[ind].sprite;
    }
    public float d_t
    {
        get => anilist[ind].delayTime;
    }
    private void Start()
    {
        thisUnit = GetComponent<Unit>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        origin = spriteRenderer.sprite;
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
                if (ind >= anilist.Count && isLoop)
                {
                    ind = -1;
                    isAni = false;
                    spriteRenderer.sprite = origin;
                    return;
                }
                else
                {
                    ind = ind >= anilist.Count ? 0 : ind;
                    Debug.Log(anilist.Count + " " + actlist.Count);
                    spriteRenderer.sprite = sprite;
                    a_dTime = d_t / (thisUnit?.timeSpeed ?? GetComponent<MobsItem>().timeSpeed);
                }
            }
            Debug.Log($"ind{ind}");
            //spriteRenderer.color = GetGradation();
        }
    }
    public void OnActAni()
    {
        isAni = true;
    }
    public void OnActAni(List<FrameSprite> nlist, List<System.Action> clist, bool isRoop)
    {
        anilist = nlist;
        actlist = clist;
        ind = -1;
        isAni = true;
    }
    [System.Serializable]
    public struct FrameSprite
    {
        public Sprite sprite;
        public float delayTime;
        public FrameSprite(Sprite sprite, float dt)
        {
            this.sprite = sprite;
            delayTime = dt;
        }
        public static FrameSprite GetDefualt()
        {
            return new(null, 0f);
        }
    }
}