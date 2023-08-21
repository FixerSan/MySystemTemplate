using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    public PlayerData data;

    private bool init = false;
    public void Init(int _level)
    {
        init = true;
        Managers.Data.GetPlayerData(_level, (data) => 
        {
            data = new PlayerData(_level);

            //나중에 아이템 장착 나오면 장착한 걸로 다시 한 번 수정
            status.currentHP = data.hp;
            status.maxHP = data.hp;
            status.currentMP = data.mp;
            status.maxMP = data.mp;
            status.maxJumpForce = data.jumpForce;
            status.currentJumpForce = data.jumpForce;
            status.maxSpeed = data.speed;
            status.currentSpeed = data.speed;
            status.attackForce = data.force;
        });
    }

    public void Updata()
    {
        if (!init)
            return; 

    }

    public override void GetDamage(float _damage)
    {

    }

    public override void Hit(float _damage)
    {

    }
}

[System.Serializable]
public class PlayerData
{
    public int level;
    public int levelUpExp;
    public float hp;
    public float mp;
    public float force;
    public float speed;
    public float jumpForce;



    public PlayerData(int _level) 
    {
        Managers.Data.GetPlayerData(_level, (data) => 
        {
            level = data.level;
            levelUpExp = data.levelUpExp;
            hp = data.hp;  
            mp = data.mp;
            force = data.force;
            speed = data.speed;
            jumpForce = data.jumpForce;
        });
    }
}
