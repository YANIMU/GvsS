using UnityEngine;
using StateSystem;

public class EUnitStateManager : StateManager
{
    public override void OnStart()
    {
        ChangeState(new WalkState());
    }
    public void OnExploded(){
        ChangeState(new ExplodedState());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == GetComponent<EUnit>().targetName)
        {
            GetComponent<EUnit>().attack.target = other.GetComponent<Unit>();
            ChangeState(new AttackState());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == GetComponent<EUnit>().targetName)
        {
            GetComponent<EUnit>().attack.target = null;
            ChangeState(new WalkState());
        }
    }
}