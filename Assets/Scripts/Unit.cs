using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int id;
    public int hp;
    public int maxhp;
    public int dmg;
    public float timeSpeed = 1f;
    public bool isAlive;
    public bool isFrozen;
    public bool isAcelerate;
    public float ss_dTime;
    public float speedStateDelayTime = 15f;
    public Color originColor = Color.white;
    public StateManager stateManager{
        get => GetComponent<StateManager>();
    }
    protected ImageAni imageAni{
        get=> GetComponent<ImageAni>();
    }
    protected ActAni actAni{
        get => GetComponent<ActAni>();
    }
    private void Start()
    {
        OnStart();
    }
    private void Update()
    {
        OnUpdate();
    }
    protected virtual void OnStart()
    {
        hp = maxhp;
    }
    protected virtual void OnUpdate()
    {
        if (isFrozen) originColor = new Color32(0x08, 0x4E, 0xFF, 0xFF);
        else originColor = Color.white;
        GetComponent<SpriteRenderer>().color = originColor;
        if (isFrozen)
        {
            ss_dTime -= Time.deltaTime;
            timeSpeed = 0.5f;
            if (ss_dTime <= 0f)
            {
                isFrozen = false;
            }
        }
        else
        {
            ResetEffectTime();
            timeSpeed = 1f;
        }
    }
    public void ResetEffectTime()
    {
        ss_dTime = speedStateDelayTime;
    }
    public void OnDamaged(int dmg)
    {
        Debug.Log(nameof(OnDamaged));
        BeAni(AniDataManager.Instance.DamagedAni, new(){
            () => ReduceHP(dmg), () => {}
        });
    }
    IEnumerator DamagedAction(int dmg)
    {
        BeAni(AniDataManager.Instance.DamagedAni, new(){
            () => ReduceHP(dmg), () => {}
        });
        yield return new WaitForSeconds(0.1f);
    }
    protected virtual void ReduceHP(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }
    public virtual void BeAni(List<ImageAni.ColorOneStep> anilist, List<System.Action> actlist)
    {
        imageAni.OnAni(anilist, actlist);
    }
    public virtual void BeActAni(List<ActAni.FrameSprite> anilist, List<System.Action> actlist, bool isLoop)
    {
        actAni.OnActAni(anilist, actlist, isLoop);
    }
    public virtual void BeAttackAni(List<ActAni.FrameSprite> anilist, List<System.Action> actlist)
    {
        actAni.OnActAni(anilist, actlist, true);
    }
    public void BeDestroy(GameObject ob){
        Destroy(ob);
    }
}