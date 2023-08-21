using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected ControllerStatus status;
    public abstract void GetDamage(float _damage);
    public abstract void Hit(float _damage);
}

[System.Serializable]
public class ControllerStatus
{
    public float maxHP;
    public float currentHP;

    public float maxMP;
    public float currentMP;

    public float maxSpeed;
    public float currentSpeed;

    public float maxJumpForce;
    public float currentJumpForce;

    public float attackForce;
}
