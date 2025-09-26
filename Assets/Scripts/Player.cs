using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float hp;
    private int level;
    private float maxmana;
    private float mana;
    public float getMana()
    {
        return mana;
    }
    public void DeltaMana(float delta){
        this.mana += delta;
    }
    private int speed;
    public int getSpeed()
    {
        return speed;
    }

    [SerializeField] TMPro.TMP_Text hpBar;
    [SerializeField] TMPro.TMP_Text manaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //can be set from save file or from cloud or both
        hp = 10;
        level = 1;
        speed = 12;
        maxmana = 10;
        mana = 10;
    }

    private List<GameObject> CollusionList = new List<GameObject>();
    void OnTriggerEnter(Collider other)
    {
        CollusionList.Add(other.gameObject); 
    }

    void OnTriggerExit(Collider other)
    {
        CollusionList.Remove(other.gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject cols in CollusionList)
        {
            hp -= ((float)cols.GetComponent<AttackFab>().power) * (1f / 60f);
        }
        hpBar.SetText("Health: " + hp + "HP");
        if (mana + (1f / 60f) < maxmana) DeltaMana(1f / 60f);
        else mana = 10f;
        manaBar.SetText("Mana: " + mana + "MP");
        
    }
}
