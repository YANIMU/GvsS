using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AniDataManager : MonoBehaviour
{
    public static AniDataManager Instance;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public List<ImageAni.ColorOneStep> DamagedAni = new(){
            new(Color.white,Color.white,0f),// ImageAni.ColorOneStep.GetDefualt()
            new(Color.red,Color.white,0.2f)
        };
    public List<ImageAni.ColorOneStep> BoomAni = new(){
            new(Color.white,Color.white,0f),// ImageAni.ColorOneStep.GetDefualt()
            new(Color.white,Color.red,0.5f),
        };
    public List<ImageAni.ColorOneStep> ExplodedAni = new(){
            new(Color.white,Color.white,0f),// ImageAni.ColorOneStep.GetDefualt()
            new(Color.black,Color.black,1f),
            new(Color.black,new(0,0,0,0),0.3f)
        };
    public List<ActAni.FrameSprite> AttackActAni = new();
    public List<ActAni.FrameSprite> WalkingActAni = new();
    
    public static List<System.Action> Null(int n)
    {
        List<System.Action> list = new();
        for (int i = 0; i < n; i++)
        {
            list.Add(() => { });
        }
        return list;
    }
}