using UnityEngine;

namespace StateSystem
{
    public class UnitState
    {
        public float delayTime;
        public float d_Time;
        public virtual void EnterState(Unit unit)
        {
            delayTime = 10f;
            d_Time = delayTime;
            if (unit.GetComponent<UnitActData>() ?? false)
                unit.BeActAni(unit.GetComponent<UnitActData>().IDLE_ActAni,
                AniDataManager.Null(8), true
                );
        }
        public virtual void Update(Unit unit)
        {
            d_Time -= Time.deltaTime;
        }
        public virtual void ExitState(Unit unit)
        {

        }
        public virtual void HandleInput(Unit unit)
        {

        }
        protected virtual void ChangeState(Unit unit)
        {

        }
        public virtual void OnAni(Unit unit)
        {

        }
        public virtual System.Type GetState()
        {
            return this.GetType();
        }
        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
    public class IdleState : UnitState
    {

    }
    public class AttackState : UnitState
    {
        public Attack attack;
        public override void EnterState(Unit unit)
        {
            attack = ((EUnit)unit).attack;
            delayTime = attack.speedDelayTime;
            d_Time = delayTime / unit.timeSpeed;
        }
        public override void Update(Unit unit)
        {
            base.Update(unit);
            if (d_Time <= 0f && (attack.target ?? false))
            {
                attack.isCanAttack = true;
                d_Time = delayTime / unit.timeSpeed;
                unit.BeAttackAni(
    AniDataManager.Instance.AttackActAni,
    new() { () => attack.target.OnDamaged(attack.dmg), () => { } });
            }
        }
    }
    public class WalkState : UnitState
    {
        public Walking walking;
        public override void EnterState(Unit unit)
        {
            walking = ((EUnit)unit).walking;
            Debug.Log(walking ?? null);
            unit.BeActAni(
                AniDataManager.Instance.WalkingActAni,
                new() { () => walking.BeWalk(false), () => walking.BeWalk(true) },
                true
            );
        }
        public override void Update(Unit unit)
        {
            walking.Move();
        }
    }
    public class ExplodedState : UnitState
    {
        public override void EnterState(Unit unit)
        {
            ((EUnit)unit).walking.enabled = false;
            ((EUnit)unit).isExploded = true;
            unit.GetComponent<BoxCollider2D>().enabled = false;
            unit.BeAni(AniDataManager.Instance.ExplodedAni,
                    new() { () => { }, () => { }, () => unit.BeDestroy(unit.gameObject) });
        }
    }
    public class ThrowState : UnitState
    {
        public override void EnterState(Unit unit)
        {
            unit.BeActAni(
                unit.GetComponent<UnitActData>().Attack_ActAni,
                new()
                {
                ()=>{},
                ()=>{unit.GetComponent<Throwing>()?.SommonThrowing();},
                ()=>{},
                ()=>{},
                ()=>{ unit.stateManager.ChangeState(new IdleState());}
                },
                false
            );
        }
    }
}