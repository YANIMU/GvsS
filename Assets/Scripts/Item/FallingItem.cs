using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public GameObject item;
    public string itemName = "Item";
    public float delayTime;
    public float d_Time;
    private void Start()
    {
        item = Resources.Load<GameObject>($"Item/{itemName}");
        d_Time = delayTime;
    }
    private void Update()
    {
        d_Time -= Time.deltaTime;
        if (d_Time <= 0f)
        {
            var newob = Instantiate(item, transform.position, Quaternion.identity);
            newob.SetActive(true);
            d_Time = delayTime + Random.Range(-3f,3f);
        }
    }
}