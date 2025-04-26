using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
    public float speed;
    public float time;
    public int dmg;
    public bool isHeat;
    public int target;
    public int element;
    public bool isCanFrozen
    {
        get => element < 0;
    }
    public bool isBurning
    {
        get => element > 0;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, speed * Time.deltaTime);
        if (time > 5f) Destroy(this.gameObject);
        GetComponent<SpriteRenderer>().color = isCanFrozen ? new Color32(0x08, 0xEA, 0xFF, 0xFF) : (isBurning ? new Color32(0xEC, 0x66, 0x37, 0xFF) : Color.white);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //sortingOrder(orderinlayer) 가장 큰
        if (other.gameObject.tag != "Mob" || isHeat) return;

        isHeat = true;
        other.GetComponent<Unit>().OnDamaged(isBurning ? dmg * 2 : dmg);
        if (isCanFrozen)
        {
            if (other.transform.childCount == 0)
            {
                other.GetComponent<Unit>().isFrozen |= isCanFrozen;
            }
            else
            {
                other.GetComponent<Unit>().isFrozen |= (isCanFrozen && (other.transform.GetChild(0).GetComponent<MobsItem>()?.isHead ?? false));
            }
            other.GetComponent<Unit>().ResetEffectTime();
        }
        else if (isBurning)
        {
            other.GetComponent<Unit>().isFrozen = false;
        }
        gameObject.SetActive(false);
        Destroy(this.gameObject, 0.2f);
    }
}