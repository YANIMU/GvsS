using System;
using System.Collections.Generic;
using UnityEngine;

public class PUnit : Unit
{
    public PUnit sommonParent;
    public int RealCost{
        get => (sommonParent?.RealCost ?? 0) + cost;
    }
    public int cost;
    public float cooldown;
    public BoxCollider2D boxCollider2D;
    private void Update() {
        GetComponent<SpriteRenderer>().color = originColor;
    }
    public override void BeAni(List<ImageAni.ColorOneStep> anilist, List<Action> actlist)
    {
        if ((this.transform.GetComponent<Boom>()?.enabled ?? false) && imageAni.isAni) return;
        base.BeAni(anilist, actlist);
    }
    public override void BeActAni(List<ActAni.FrameSprite> anilist, List<Action> actlist, bool isLoop)
    {
        Debug.Log($"{actAni ?? null} {anilist?.Count ?? null} {actlist?.Count ?? null} {isLoop}");
        base.BeActAni(anilist, actlist, isLoop);
    }
}