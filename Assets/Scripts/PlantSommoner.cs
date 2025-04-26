using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantSommoner : MonoBehaviour
{
    Camera cam;
    public string mobName = "001_Square";
    public GameObject sommonMob;
    public GameObject sommonMobShadow;
    public ImageAniUI imageAniUI;
    public Text text;
    public bool isSpriteTreaking;
    public bool isCooldownNow;
    public float c_time;
    private void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        //sommonMobSprite = Resources.Load<GameObject>("SommonShadow");
        sommonMobShadow.GetComponent<SpriteRenderer>().sprite = sommonMob.GetComponent<SpriteRenderer>().sprite;
        sommonMobShadow?.SetActive(false);
        isSpriteTreaking = false;
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = $"{mobName.Split('_')[1]}\n{sommonMob.GetComponent<PUnit>().cost}";
    }
    void Update()
    {
        c_time -= Time.deltaTime;
        if (isSpriteTreaking && c_time <= 0f)
        {
            sommonMobShadow.transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                OnSommon();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                isSpriteTreaking = false;
                sommonMobShadow.SetActive(false);
            }
        }
    }
    void OnSommon()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, 10f);
        if (hits == default) return;
        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("Tile"))
            {
                var cost = SommonMob(hit.transform.GetComponent<Tile>(), sommonMob.GetComponent<PUnit>());
                if (cost < 0 || cost > CostManager.Instance.sPower)
                {
                    if (cost >= 0)
                        SystemMassageManager.Instance.SystemMassage($"{CostManager.Instance.sPower}({cost}) Not Enough Minerals.");
                    return;
                }
                var newob = Instantiate(sommonMob, new Vector3(hit.transform.position.x, hit.transform.position.y, 0f), Quaternion.identity);
                hit.transform.GetComponent<Tile>().isCanSommon = false;
                Destroy(hit.transform.GetComponent<Tile>().sommonMob);
                hit.transform.GetComponent<Tile>().sommonMob = newob;
                newob.SetActive(true);
                CostManager.Instance.sPower -= cost;
                isSpriteTreaking = false;
                sommonMobShadow.SetActive(false);
                c_time = sommonMob.GetComponent<PUnit>().cooldown;
                imageAniUI.GetComponent<ImageAniUI>().OnAni(c_time);
                break;
            }
        }
    }
    int SommonMob(Tile tile, PUnit sommonMob)
    {
        //조건 
        // 타일 있으면
        //      (sommonOb.sommonParent가 null이 나올때까지)
        //          tileOb와 sommonOb.sommonParent 의 id가 같으면
        //              가격 체크
        //          다르면
        //              sommonOb.sommonParent의 sommonParent 의 id로 다시
        // 타일 없으면
        //      가격 체크 (테크 총합)
        var unit = sommonMob;
        if (tile.sommonMob ?? false)
        {
            if (tile.sommonMob.GetComponent<PUnit>().id == (unit.sommonParent?.id ?? 0))
            {
                return unit.cost;
            }
            return -1;
        }
        else
        {
            if (unit.sommonParent ?? false)
                return -1;
            return unit.cost;
        }
    }
    public void OnChooseMob()
    {
        if (sommonMobShadow.activeSelf) return;
        if (c_time > 0f) return;
        isSpriteTreaking = true;
        sommonMobShadow.GetComponent<SpriteRenderer>().sprite = sommonMob.GetComponent<SpriteRenderer>().sprite;
        sommonMobShadow.SetActive(true);
    }
}
