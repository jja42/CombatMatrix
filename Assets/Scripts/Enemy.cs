using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public Vector2 Pos;
    public Vector2 TargetPos;
    bool moving;
    public float move_timer;
    float og_move_timer;
    public float shoot_timer;
    float og_shoot_timer;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        Pos = GameManager.instance.GetGridSquare(0, 0, false).index;
        TargetPos = Pos;
        og_move_timer = move_timer;
        og_shoot_timer = shoot_timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.paused)
        {
            if (TargetPos != Pos && !moving)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
            if (moving)
            {
                if (move_timer <= 0)
                {
                    MovetoTarget();
                }
                else
                {
                    move_timer -= Time.deltaTime;
                }
            }
            if (Pos.y == TargetPos.y && shoot_timer <= 0)
            {
                GameObject shot = Instantiate(projectile);
                shot.transform.position = transform.position + new Vector3(-1, 0);
                shoot_timer = og_shoot_timer;
                Destroy(shot, 5);
            }
            else
            {
                shoot_timer -= Time.deltaTime;
            }
        }
    }

    void MovetoTarget()
    {
        if(Pos.y != TargetPos.y)
        {
            if(Pos.y < TargetPos.y)
            {
                transform.position = GameManager.instance.GetGridSquare((int)Pos.x, (int)Pos.y + 1, false).pos;
                Pos += new Vector2(0, 1);
                move_timer = og_move_timer;
                return;
            }
            else
            {
                transform.position = GameManager.instance.GetGridSquare((int)Pos.x, (int)Pos.y - 1, false).pos;
                Pos += new Vector2(0, -1);
                move_timer = og_move_timer;
                return;
            }
        }
        if(Pos.x > TargetPos.x)
        {
            transform.position = GameManager.instance.GetGridSquare((int)Pos.x - 1, (int)Pos.y, false).pos;
            Pos += new Vector2(-1, 0);
            move_timer = og_move_timer;
            return;
        }
        else
        {
            transform.position = GameManager.instance.GetGridSquare((int)Pos.x + 1, (int)Pos.y, false).pos;
            Pos += new Vector2(1, 0);
            move_timer = og_move_timer;
            return;
        }
    }
    protected override void Die()
    {
        GameManager.instance.paused = true;
        GameManager.instance.Win();
    }
}
