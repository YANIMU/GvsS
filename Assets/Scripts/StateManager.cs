using UnityEngine;
using StateSystem;
public class StateManager : MonoBehaviour
{
    public Unit unit{
        get => GetComponent<Unit>();
    }
    public string nowState;
    public UnitState state;
    private void Start() {
        Debug.Log($"{nameof(StateManager)} : {nameof(Start)}");
        OnStart();
    }
    private void Update() {
        state.Update(unit);
    }
    public virtual void OnStart(){
        
    }
    public string GetState(){
        return state.ToString();
    }
    public void ChangeState(UnitState state){
        this.state = state;
        this.state.EnterState(unit);
        nowState = GetState();
    }
}