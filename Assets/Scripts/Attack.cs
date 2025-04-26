using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Unit target;
    public int dmg;
    public float speedDelayTime;
    public float s_dTime;
    public bool isCanAttack;
    private void Start()
    {
        s_dTime = speedDelayTime / this.GetComponent<Unit>().timeSpeed;
    }
    // private void Update()
    // {
    //     s_dTime -= Time.deltaTime;
    //     if (s_dTime <= 0f&& (target ?? false))
    //     {
    //         isCanAttack = true;
    //     s_dTime = speedDelayTime / this.GetComponent<Unit>().timeSpeed;
    //     // }
    //     // if(isCanAttack && (target ?? false)){
    //     //     isCanAttack = false;
    //         gameObject.GetComponent<Unit>().BeAttackAni(
    //         AniDataManager.Instance.AttackActAni,
    //         new() { () => target?.OnDamaged(dmg), () => { } });
    //     }
    // }
    public void OnAttack()
    {
        if (!isCanAttack) return;
        if (target == null) return;
        Debug.Log(nameof(OnAttack));
        isCanAttack = false;
        //StartCoroutine(AttackAction());
        gameObject.GetComponent<Unit>().BeAttackAni(
            AniDataManager.Instance.AttackActAni ,
            new() { () => target.OnDamaged(dmg), () => { } });
    }
    IEnumerator AttackAction()
    {
        if (target == null) yield break;
        gameObject.GetComponent<Unit>().BeAttackAni(
            AniDataManager.Instance.AttackActAni,
            new() { () => target.OnDamaged(dmg), () => { } });
        yield return new WaitForSeconds(0.1f);
        if (target == null) yield break;
        Debug.Log(System.DateTime.Now);
        yield return null;

    }
}