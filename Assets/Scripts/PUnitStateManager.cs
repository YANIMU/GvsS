using UnityEngine;
using StateSystem;

public class PUnitStateManager : StateManager
{
    public override void OnStart()
    {
        ChangeState(new IdleState());
    }
    public void OnThrowing(){
        ChangeState(new ThrowState());
    }
}