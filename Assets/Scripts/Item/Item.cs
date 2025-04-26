using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 destroyPosition;
    public float gravity;
    public float speed;
    public float deadTime = 10f;
    public float d_Time;
    public Vector3 direction;
    public bool isClick = false;
    public bool isDestroy = false;
    public ItemEffect itemEffect;
    ImageAni imageAni;
    private void Start()
    {
        startPosition = transform.position;
        direction = (Vector2.up + Random.Range(-0.2f, 0.2f) * Vector2.right).normalized;
        itemEffect = GetComponent<ItemEffect>();
        imageAni = GetComponent<ImageAni>();
        d_Time = deadTime + 1;
    }
    private void Update()
    {
        if (isClick)
        {
            //Mathf.Abs(transform.position.x - destroyPosition.position.x) < 1f ||
            //Mathf.Abs(transform.position.x - destroyPosition.position.x) < 1f
            if (isDestroy) { Destroy(this.gameObject); }
            if (transform.position == destroyPosition)
            { isDestroy = true; }
            transform.position = Vector3.MoveTowards(transform.position, destroyPosition, Time.deltaTime * speed * 2);
        }
        else
        {
            if (d_Time <= 0f) { TimeOverDestroy(); d_Time = 3f; }
            d_Time -= Time.deltaTime;
            if (transform.position.y >= startPosition.y)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + direction, speed * Time.deltaTime);
                direction = new Vector3(direction.x, direction.y - gravity * Time.deltaTime, -1f);
            }
        }
    }
    public virtual void BeAni(List<ImageAni.ColorOneStep> anilist, List<System.Action> actlist)
    {
        imageAni.OnAni(anilist, actlist);
    }
    private void OnMouseDown()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        itemEffect.OnExecute();
        isClick = true;
    }
    public void TimeOverDestroy()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        BeAni(
            new() { new(Color.white, new Color(1, 1, 1, 0), 1f) },
            AniDataManager.Null(2)
            );
        Destroy(this.gameObject, 2f);
    }
}