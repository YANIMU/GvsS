using UnityEngine;

public class Throwing : MonoBehaviour
{
    public GameObject throwingObject;
    public Unit thisUnit;
    public Aria aria;
    public string t_ObjectName = "Triangle";
    public int throwingCount;
    public float countDelayTime;
    public float throwingDelayTime;
    public int t_Count;
    public float c_dTime = 0f;
    public float t_dTime = 0f;
    private void Start()
    {
        throwingObject = Resources.Load<GameObject>($"ThrowingObject/{t_ObjectName}");
        thisUnit = GetComponent<Unit>();
        aria = GetComponent<Aria>();
        OnReset();
    }
    void OnReset()
    {
        t_Count = throwingCount;
        c_dTime = countDelayTime / thisUnit.timeSpeed;
        t_dTime = throwingDelayTime / thisUnit.timeSpeed;
    }
    private void Update()
    {
        if ((aria?.count ?? 0) > 0)
        {
            c_dTime -= Time.deltaTime;
            t_dTime -= Time.deltaTime;
            if (t_Count > 0f)
            {
                if (c_dTime <= 0f)
                {
                    ((PUnitStateManager)thisUnit.stateManager).OnThrowing();
                    t_Count--;
                    c_dTime = countDelayTime;
                }
            }
            else
            {
                if (t_dTime <= 0f)
                {
                    OnReset();
                }
            }
        }
        else
        {
            OnReset();
        }
    }
    public void SommonThrowing()
    {
        var newob = Instantiate(throwingObject);
        newob.transform.position = this.gameObject.transform.position;
        newob.GetComponent<ThrowingObject>().target = aria.target;
        newob.SetActive(true);
    }
}