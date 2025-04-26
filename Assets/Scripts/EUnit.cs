using System;
using System.Collections.Generic;
using UnityEngine;

public class EUnit : Unit
{
    public int sNum = 0;
    public MobsItem mobsItem;
    public string targetName;
    public Walking walking
    {
        get => GetComponent<Walking>();
    }
    public Attack attack
    {
        get => GetComponent<Attack>();
    }
    public bool isExploded;
    protected override void OnStart()
    {
        base.OnStart();
        mobsItem = transform.childCount > 0 ? transform.GetChild(0).GetComponent<MobsItem>() : null;
        attack.dmg = dmg;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        // if ((attack.target?.hp ?? 0) > 0)
        // {
        //     //attack.OnAttack();
        // }
        // else
        // {
        //     walking.isStop = false;
        //     walking.isAni = true;
        // }
    }
    protected override void ReduceHP(int dmg)
    {
        if ((mobsItem?.hp ?? 0) > 0)
        {
            mobsItem.ReduceHP(dmg);
        }
        else
        {
            base.ReduceHP(dmg);
        }
    }
    public void OnExploded()
    {
        ((EUnitStateManager)stateManager).OnExploded();
    }
    public override void BeAni(List<ImageAni.ColorOneStep> anilist, List<Action> actlist)
    {
        if (isExploded)
        {
            Debug.Log(nameof(mobsItem) + $"{mobsItem ?? null}");
            Debug.Log(nameof(mobsItem) + "Start");
            Debug.Log(nameof(mobsItem) + "Done");
            base.BeAni(anilist, actlist);
            mobsItem?.BeAni(this.imageAni);
            return;
        }
        if (mobsItem ?? false)
        {
            if (mobsItem?.isHead ?? false)
            {
                base.BeAni(anilist, AniDataManager.Null(2));
            }
            mobsItem.BeAni(anilist, actlist);
        }
        else
        {
            base.BeAni(anilist, actlist);
        }
    }
    public override void BeAttackAni(List<ActAni.FrameSprite> anilist, List<Action> actlist)
    {
        base.BeAttackAni(anilist, actlist);
        if (mobsItem ?? false)
        {
            if (mobsItem?.isHead ?? false)
            {
                //mobsItem.BeAni(anilist, ImageAni.Null(2));
            }
        }
    }
}