using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement PlayerMovement;
    [SerializeField] PlayerMovement EnemyMovement;
    [SerializeField] SpellCaster SpellCaster;
    [SerializeField] SpellCaster EnemySpellCaster;
    SpellLibrary SpellLibrary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpellLibrary = new SpellLibrary();
        SpellLibrary.AddSpell("fireball", 3, 3, 3);

        bool[,] tempaoe = {{false,true ,false},
                           {true ,false,true },
                           {false,true ,false}};
        SpellLibrary.AddSpell("firejail", 3, 3, 3, tempaoe);

        bool[,] tempaoe2 = {{true,false ,true},
                           {false ,true,false },
                           {true,false ,true}};
        SpellLibrary.AddSpell("firecross", 3, 1, 2, tempaoe2);

    }

    // Update is called once per frame
    void Update()
    {


        //Enemy Movement and Action
        {
            if (!EnemyMovement.IsMoving() && !EnemySpellCaster.IsCasting())
            {
                System.Random rnd = new System.Random();
                int move = rnd.Next(1, 8);
                switch (move)
                {
                    case 1:
                        EnemyMovement.MovePlayer('N');
                        break;
                    case 2:
                        EnemyMovement.MovePlayer('S');
                        break;
                    case 3:
                        EnemyMovement.MovePlayer('W');
                        break;
                    case 4:
                        EnemyMovement.MovePlayer('E');
                        break;
                    case 5:
                        EnemySpellCaster.CastAttackSpell(SpellLibrary.getSpell("fireball"));
                        break;
                    case 6:
                        EnemySpellCaster.CastAttackSpell(SpellLibrary.getSpell("firejail"));
                        break;
                    case 7:
                        EnemySpellCaster.CastAttackSpell(SpellLibrary.getSpell("firecross"));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void MoveOrAttack(string input)
    {
        if (!PlayerMovement.IsMoving() && !SpellCaster.IsCasting())
        {
            if (input == "N")
            {
                PlayerMovement.MovePlayer('N');
            }
            else if (input == "S")
            {
                PlayerMovement.MovePlayer('S');
            }
            else if (input == "E")
            {
                PlayerMovement.MovePlayer('W');
            }
            else if (input == "W")
            {
                PlayerMovement.MovePlayer('E');
            }
            else if (input == "1")
            {
                SpellCaster.CastAttackSpell(SpellLibrary.getSpell("fireball"));
            }
            else if (input == "2")
            {
                SpellCaster.CastAttackSpell(SpellLibrary.getSpell("firejail"));
            }
            else if (input == "3")
            {
                SpellCaster.CastAttackSpell(SpellLibrary.getSpell("firecross"));
            }
        }
    }
    public void ForceMove(Player player)
    {
        if (player.gameObject.name == "Player") EnemyMovement.ForceMovePlayer();
        else PlayerMovement.ForceMovePlayer();
    }
   
    
}
