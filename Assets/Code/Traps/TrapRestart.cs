using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRestart : MonoBehaviour
{
    public enum trap_type
    {
        Firegun,
        MovingWall,
        RadialFire,
        Saw,
        Turrel_1,
        Spikes,
        MovingCapsul,
        SpinBlades,
        RotateCapsul
    }
    public trap_type Trap_Type;
    public GameObject trap;

    public void RestartTrap()
    {
        if (Trap_Type == trap_type.RotateCapsul)
        {
            trap.GetComponent<RotateCapsulController>().EndAttack();
        }

        if (Trap_Type == trap_type.SpinBlades)
        {
            trap.GetComponent<SpinBladesController>().EndAttack();
        }

        if (Trap_Type == trap_type.MovingCapsul)
        {
            trap.GetComponent<MovingCapsulController>().EndAttack();
        }

        if (Trap_Type == trap_type.Turrel_1)
        {
            trap.GetComponent<TurretController>().EndAttack();
        }

        if (Trap_Type == trap_type.Saw)
        {
            trap.GetComponent<SawController>().EndAttack();
        }

        if (Trap_Type == trap_type.Firegun)
        {
            trap.GetComponent<FiregunController>().EndAttack();
        }

        if (Trap_Type == trap_type.RadialFire)
        {
            trap.GetComponent<RadialFireController>().EndAttack();
        }

        if (Trap_Type == trap_type.MovingWall)
        {
            trap.GetComponent<MovingWallController>().EndAttack();
        }

        if (Trap_Type == trap_type.Spikes)
        {
            trap.GetComponent<SpikeController>().EndAttack();
        }
    }

    public void StartTrap()
    {
        if (Trap_Type == trap_type.RotateCapsul)
        {
            trap.GetComponent<RotateCapsulController>().StartAttack();
        }

        if (Trap_Type == trap_type.SpinBlades)
        {
            trap.GetComponent<SpinBladesController>().StartAttack();
        }

        if (Trap_Type == trap_type.MovingCapsul)
        {
            trap.GetComponent<MovingCapsulController>().StartAttack();
        }

        if (Trap_Type == trap_type.Turrel_1)
        {
            trap.GetComponent<TurretController>().StartAttack();
        }

        if (Trap_Type == trap_type.Saw)
        {
            trap.GetComponent<SawController>().StartAttack();
        }

        if (Trap_Type == trap_type.Firegun)
        {
            trap.GetComponent<FiregunController>().StartAttack();
        }

        if (Trap_Type == trap_type.RadialFire)
        {
            trap.GetComponent<RadialFireController>().StartAttack();
        }

        if (Trap_Type == trap_type.MovingWall)
        {
            trap.GetComponent<MovingWallController>().StartAttack();
        }

        if (Trap_Type == trap_type.Spikes)
        {
            trap.GetComponent<SpikeController>().StartAttack();
        }
    }
}
