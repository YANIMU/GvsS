using UnityEngine;

public class Walking : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public bool isStop;
    public bool isFoot;
    // void Update()
    // {
    //     if (!isStop)
    //     {
    //         if (isAni)
    //         {
    //             this.GetComponent<ActAni>().OnActAni(
    //                 AniDataManager.Instance.WalkingActAni,
    //                 new() { () => BeWalk(false), () => BeWalk(true) },
    //                 true
    //             );
    //             isAni = false;
    //         }
    //         transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, (isFoot ? minSpeed : maxSpeed) * Time.deltaTime * GetComponent<Unit>().timeSpeed);
    //     }
    // }
    public void BeWalk(bool b)
    {
        isFoot = b;
    }
    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left, (isFoot ? minSpeed : maxSpeed) * Time.deltaTime * GetComponent<Unit>().timeSpeed);
    }
}