using UnityEngine;

public class Aria : MonoBehaviour
{
    public Vector3 vec = new Vector3(11.5f, 0f, 0f);
    public int count;
    public int target;
    private void Update()
    {
        OnRaycast();
    }
    float distance = 100f;
    void OnRaycast()
    {
        var cnt = 0;
        //Debug.DrawRay(transform.position, vec, Color.red, length);
        //Debug.DrawRay(transform.position, vec, Color.red, 5);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, vec, Mathf.Abs(transform.position.x - vec.x));
        if (hits == default) return;
        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("Mob"))
            {

                var f = Vector2.Distance(this.transform.position, hit.transform.position);
                if (distance == f)
                {
                    target = hit.transform.GetComponent<EUnit>().sNum < target ?
                    hit.transform.GetComponent<EUnit>().sNum : target;

                }
                else
                {
                    target = distance < f ? target :
                        hit.transform.GetComponent<EUnit>().sNum;
                    distance = distance < f ? distance : f;
                }
                cnt++;
            }
        }
        count = cnt;
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Mob")
    //     {
    //         count++;
    //     }
    //     else if (other.gameObject.tag == "Plant")
    //     {
    //         if(other.GetComponent<Throwing>() != null)
    //         other.GetComponent<Throwing>().aria = this;
    //     }
    // }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Mob")
    //     {
    //         count--;
    //     }
    // }
}