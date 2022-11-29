using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    Vector2 Pos;
    bool moving = false;
    bool shooting = false;
    public float shoot_timer;
    public float move_timer;
    float og_move_timer;
    float og_shoot_timer;
    public GameObject projectile;
    public static Player instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        og_move_timer = move_timer;
        og_shoot_timer = shoot_timer;
        Pos = GameManager.instance.GetGridSquare(0, 0, true).index;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.paused)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && !moving)
            {
                moving = true;
                MovePosition("Right");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !moving)
            {
                moving = true;
                MovePosition("Left");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && !moving)
            {
                moving = true;
                MovePosition("Up");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !moving)
            {
                moving = true;
                MovePosition("Down");
            }
            if (move_timer <= 0)
            {
                moving = false;
            }
            else
            {
                move_timer -= Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space) && shoot_timer <= 0)
            {
                shoot_timer = og_shoot_timer;
                GameObject shot = Instantiate(projectile);
                shot.transform.position = transform.position + new Vector3(1, -.25f);
                Destroy(shot, 5);
            }
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {
                if(ChipManager.instance.SelectedChips.Count != 0)
                {
                    shoot_timer = og_shoot_timer;
                    ChipManager.instance.SelectedChips[0].action();
                    ChipManager.instance.SelectedChips.RemoveAt(0);
                }
            }
            if (shoot_timer > 0)
            {
                shoot_timer -= Time.deltaTime;
            }
        }
    }

    void MovePosition(string direction)
    {
        move_timer = og_move_timer;
        switch (direction)
        {
            case "Right":
                if(Pos.x < 1)
                {
                    transform.position = GameManager.instance.GetGridSquare((int)Pos.x + 1,(int) Pos.y, true).pos;
                    Pos += new Vector2(1, 0);
                }
                break;
            case "Left":
                if (Pos.x > -1)
                {
                    transform.position = GameManager.instance.GetGridSquare((int)Pos.x - 1, (int)Pos.y, true).pos;
                    Pos += new Vector2(-1, 0);
                }
                break;
            case "Up":
                if (Pos.y < 1)
                {
                    transform.position = GameManager.instance.GetGridSquare((int)Pos.x, (int)Pos.y + 1, true).pos;
                    Pos += new Vector2(0, 1);
                }
                break;
            case "Down":
                if (Pos.y > -1)
                {
                    transform.position = GameManager.instance.GetGridSquare((int)Pos.x, (int)Pos.y - 1, true).pos;
                    Pos += new Vector2(0, -1);
                }
                break;
            default:
                break;
        }
        GameManager.instance.UpdateEnemyTargetPosition(Pos);
    }
    protected override void Die()
    {
        GameManager.instance.paused = true;
        GameManager.instance.Loss();
    }
}
