using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsItem : MonoBehaviour
{
    public int hp;
    public float timeSpeed
    {
        get => transform.parent.GetComponent<Unit>().timeSpeed;
    }
    public bool isMetal;
    public bool isHead;
    public MobsItem child
    {
        get => (transform.childCount > 0) ? transform.GetChild(0).GetComponent<MobsItem>() : null;
    }
    public Unit thisUnit
    {
        get => transform.parent.GetComponent<Unit>();
    }
    public Color originColor
    {
        get => thisUnit.originColor;
    }
    ImageAni imageAni
    {
        get => GetComponent<ImageAni>();
    }
    private void Start()
    {
        transform.localPosition = isHead ? Vector3.up : Vector3.left * 0.5f;
    }
    private void Update()
    {
        if (isHead)
            GetComponent<SpriteRenderer>().color = transform.parent.GetComponent<SpriteRenderer>().color;
    }
    public void ReduceHP(int dmg)
    {
        if (child ?? false)
        {
            child.ReduceHP(dmg);
        }
        else hp -= dmg;
        if (hp <= 0) BeDestroy();
    }
    private void BeDestroy()
    {
        Destroy(this.gameObject);
    }
    public virtual void BeAni(List<ImageAni.ColorOneStep> anilist, List<System.Action> actlist)
    {
        imageAni.OnAni(anilist, actlist);
    }
    public virtual void BeAni(ImageAni iAni)
    {
        imageAni.OnAni(iAni.anilist, iAni.actlist);
    }
}