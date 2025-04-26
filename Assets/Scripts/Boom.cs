using UnityEngine;

public class Boom : MonoBehaviour
{
    public Unit thisUnit;
    public BoomObject boomObject;
    public float delayTime = 1f;
    public float d_Time;

    private void Start()
    {
        boomObject = transform.GetChild(0).GetComponent<BoomObject>();
        thisUnit = GetComponent<Unit>();
        thisUnit.BeAni(
            AniDataManager.Instance.BoomAni,
            new() { () => { Debug.Log(1); }, () => { Debug.Log(2); this.BeBoom(); } });
        d_Time = delayTime;
    }
    // private void Update()
    // {
    //     d_Time -= Time.deltaTime;
    //     if (!thisUnit.imageAni.isAni && d_Time <= 0f)
    //     {
    //         while (boomObject.list.Count > 0)
    //         {
    //             boomObject.list[0].GetComponent<Unit>().hp = 0;
    //             boomObject.list.RemoveAt(0);
    //         }
    //         Destroy(this.gameObject);
    //     }
    // }
    public void BeBoom()
    {
        Debug.Log(nameof(BeBoom) + boomObject.list.Count);
        
        while (boomObject.list.Count > 0)
        {
            Debug.Log(boomObject.list[0].name);
            boomObject.list[0].GetComponent<EUnit>().OnExploded();
            //boomObject.list.RemoveAt(0);
        }
        Destroy(this.gameObject);
    }
}