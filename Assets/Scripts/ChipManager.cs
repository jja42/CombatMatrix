using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    public struct Chip
    {
        public int chip_id;
        public string name;
        public int damage;
        public string nickname;
        public string description;
        public System.Action action;
        public Chip(int chip_id, string name, int damage, string nickname, string description, System.Action act)
        {
            this.chip_id = chip_id;
            this.name = name;
            this.damage = damage;
            this.nickname = nickname;
            this.description = description;
            action = act;
        }
    }

    public static ChipManager instance;

    public List<Chip> Database;
    public List<Chip> AvailableChips;
    public List<Chip> SelectedChips;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Database = new List<Chip>();
        AvailableChips = new List<Chip>();
        SelectedChips = new List<Chip>();
        GenerateDatabase();
    }

    void GenerateDatabase()
    {
        Database = new List<Chip>
        {
            new Chip(1,"Sword",80,"SWD","Short Range Blade",Sword),
            new Chip(2,"Wide Sword",60,"W-SWD","Wide Range Blade",WideSword),
            new Chip(3,"Long Sword",60,"L-SWD","Long Range Blade",LongSword),
            //new Chip(4,"Vampiric Sword",40,"VMP","Vampiric Blade",VampiricSword),
            new Chip(5,"Mega Shot",80,"M-GUN","Powerful Blast",MegaShot),
            new Chip(6,"Spread Shot",20,"S-GUN","Spread of Blasts",SpreadShot),
            //new Chip(7,"Poison Shot",10,"P-GUN","Inflicts DoT"),
            //new Chip(8,"Triple Shot",20,"T-GUN","Rapid Burst of Blasts"),
           // new Chip(9,"Turret",20,"TRT","Temporary Turret Ally"),
            new Chip(10,"Heal",20,"HEAL","Restore HP",Heal),
            //new Chip(11,"Bomb",120,"BMB","Mid Range Explosive"),
            //new Chip(12,"Blast Aura",20,"AUR","Temporary Damaging Aura",BlastAura),
            new Chip(13,"Guard",20,"GRD","Prevents Incoming Damage",Guard),
            //new Chip(14,"Time Bomb",100,"T-BMB","Short Range Blade"),
            //new Chip(15,"Mine",60,"MINE","Short Range Blade"),
            //new Chip(16,"Lock-On",40,"L-ON","Temporary Aim Assistance",LockOn)
        };
    }

    public void GenerateAvailableChips()
    {
        AvailableChips.Clear();
        int rand_index = Random.Range(0, Database.Count);
        for (int i = 0; i < 6; i++)
        {
            AvailableChips.Add(Database[rand_index]);
            rand_index = Random.Range(0, Database.Count);
        }
    }

    public Chip GetChipByID(int chip_id)
    {
        return Database.Find(chip => chip.chip_id == chip_id);
    }

    void Sword()
    {
        GameObject projectile = Resources.Load<GameObject>("Prefabs/Sword");
        GameObject shot = Instantiate(projectile);
        shot.transform.position = Player.instance.transform.position + new Vector3(2, 0);
        Destroy(shot, .5f);
    }

    void WideSword()
    {
        GameObject projectile = Resources.Load<GameObject>("Prefabs/WideSword");
        GameObject shot = Instantiate(projectile);
        shot.transform.position = Player.instance.transform.position + new Vector3(2.5f, 0);
        Destroy(shot, .5f);
    }

    void LongSword()
    {
        GameObject projectile = Resources.Load<GameObject>("Prefabs/LongSword");
        GameObject shot = Instantiate(projectile);
        shot.transform.position = Player.instance.transform.position + new Vector3(5, 0);
        Destroy(shot, .5f);
    }

    void VampiricSword()
    {

    }

    void MegaShot()
    {
        GameObject projectile = Resources.Load<GameObject>("Prefabs/MegaShot");
        GameObject shot = Instantiate(projectile);
        shot.transform.position = Player.instance.transform.position + new Vector3(1, -.25f);
        Destroy(shot, 5f);
    }

    void SpreadShot()
    {
        GameObject projectile = Resources.Load<GameObject>("Prefabs/SpreadShot");
        GameObject shot = Instantiate(projectile);
        shot.transform.position = Player.instance.transform.position + new Vector3(1, -.25f);
        Destroy(shot, 5f);
    }

    void Heal()
    {
        Player.instance.Heal(20);
    }

    void BlastAura()
    {
        GameObject projectile = Resources.Load<GameObject>("Prefabs/Blast Aura");
        GameObject shot = Instantiate(projectile);
        shot.transform.position = Player.instance.transform.position + new Vector3(0, -.25f);
        Destroy(shot, 5f);
    }

    void Guard()
    {
        Player.instance.Guard(20);
    }

    void LockOn()
    {

    }


}
