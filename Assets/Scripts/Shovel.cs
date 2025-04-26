using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    Camera cam;
    public GameObject shovelSprite;
    public bool isSpriteTreaking;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        isSpriteTreaking = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isSpriteTreaking)
        {
            shovelSprite.transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            //sommonMobSprite.GetComponent<SpriteRenderer>().color()
            if (Input.GetMouseButtonDown(0))
            {
                OnSommon();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                isSpriteTreaking = false;
                shovelSprite.SetActive(false);
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
                if (!hit.transform.GetComponent<Tile>().isCanSommon)
                {
                    Destroy(hit.transform.GetComponent<Tile>().sommonMob);
                    hit.transform.GetComponent<Tile>().isCanSommon = true;
                    isSpriteTreaking = false;
                    shovelSprite.SetActive(false);
                    break;
                }
            }
        }
    }
    public void OnShovel()
    {
        isSpriteTreaking = true;
        shovelSprite.SetActive(true);
    }
}
