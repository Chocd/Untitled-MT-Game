using System.Collections;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] GameObject AttackFab;
    [SerializeField] GameObject MovementPads;
    [SerializeField] Player Player;

    GameObject parentObject;
    void Start()
    {
        parentObject = new GameObject();
        parentObject.name = "AttackFabs";
    }
    public void CastAttackSpell(Spell spell)
    {
        if (casting) return;


        int manacost = 0;
        bool[] attackPoints = new bool[9];
        for (int i = 0; i < spell.aoe.GetLength(0); i++)
        {
            for (int j = 0; j < spell.aoe.GetLength(1); j++)
            {
                attackPoints[i * 3 + j] = spell.aoe[i, j];
                if (spell.aoe[i, j]) manacost++;
            }
        }
        if (Player.getMana() < manacost) return;
        else Player.DeltaMana(-(float)manacost);
        casting = true;
        for (int i = 0; i < attackPoints.Length; i++)
        {
            if (attackPoints[i])
            {
                Transform attackPad = MovementPads.transform.GetChild(i);
                GameObject Attack = Instantiate(AttackFab, new Vector3(attackPad.position.x, attackPad.position.y + 0.1f, attackPad.position.z), attackPad.rotation);
                Attack.transform.localScale = new Vector3(0, 0, 0);
                Attack.transform.parent = parentObject.transform;
                StartCoroutine(SmoothScaleLerp(Attack, Attack.transform.localScale, new Vector3(0.33f * (float)spell.size, 0.05f * (float)spell.size, 0.33f * (float)spell.size), spell.speed));
            }
        }

    }
    public bool casting = false;


    public bool IsCasting()
    {
        return casting;
    }
    private IEnumerator SmoothScaleLerp(GameObject scalingObject, Vector3 startScl, Vector3 finalScl, int speed)
    {
        casting = true;
        yield return new WaitForSeconds(0.5f);
        casting = false;
        float elapsedTime = 0;
        scalingObject.SetActive(true);
        while (elapsedTime < 0.3f * (6f - (float)speed))
        {
            scalingObject.transform.localScale = Vector3.Lerp(startScl, finalScl, elapsedTime / (0.3f * (6f - (float)speed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        scalingObject.transform.position = new Vector3(9999f, 9999f, 9999f);
        yield return new WaitForSeconds(0.2f);
        Destroy(scalingObject);
    }

    public void FMoveSpells()
    {
        //Will finish if we decide to move the spells with the catchup mechanic
    }

    
}
