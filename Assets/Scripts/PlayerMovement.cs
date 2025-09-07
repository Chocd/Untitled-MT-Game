using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player Player;
    [SerializeField] GameObject MovementPads;
    [SerializeField] GameController GameController;
    private int playerLocation;
    private bool moving;
    private bool fmoving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerLocation = 5;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePlayer(char direction)
    {
        if (moving)
        {
            Debug.LogWarning("Player is already moving");
            return;
        }
        int nextLocation = playerLocation;
        //Check if the player can move in the given direction
        if ((playerLocation == 1 || playerLocation == 2 || playerLocation == 3) && direction == 'N')
        {
            GameController.ForceMove(Player);
            return; // WILL NEED TO REMOVE LATER
        }
        else if ((playerLocation == 1 || playerLocation == 4 || playerLocation == 7) && direction == 'W')
        {
            Debug.LogWarning("Non-Valid Move");
            return;
        }
        else if ((playerLocation == 3 || playerLocation == 6 || playerLocation == 9) && direction == 'E')
        {
            Debug.LogWarning("Non-Valid Move");
            return;
        }
        else if ((playerLocation == 7 || playerLocation == 8 || playerLocation == 9) && direction == 'S')
        {
            Debug.LogWarning("Non-Valid Move");
            return;
        }

        if (direction == 'N') nextLocation -= 3;
        else if (direction == 'S') nextLocation += 3;
        else if (direction == 'W') nextLocation -= 1;
        else if (direction == 'E') nextLocation += 1;

        if (nextLocation != playerLocation)
        {
            playerLocation = nextLocation;
            StartCoroutine(SmoothLerp(GetNextPosCoor(playerLocation)));
        }
    }
    public void ForceMovePlayer()
    {
        if (fmoving)
        {
            Debug.LogWarning("Player is already force moving");
            return;
        }
        if ((playerLocation == 1 || playerLocation == 2 || playerLocation == 3))
        {
            Debug.LogWarning("Player is already at the forefront");
            return;
        }
        playerLocation -= 3;
        StartCoroutine(FSmoothLerp(FGetNextPosCoor(playerLocation)));
    }

    public bool IsMoving()
    {
        return moving;
    }
    private int MapChildIndex()
    {
        return playerLocation - 1;
    }
    private int MapChildIndex(int index)
    {
        return index - 1;
    }
    private Vector3 GetNextPosCoor(int nextLocation)
    {
        Vector3 boxPos = MovementPads.transform.GetChild(MapChildIndex(nextLocation)).transform.position;
        return new Vector3(boxPos.x, Player.gameObject.transform.position.y, boxPos.z);
    }
    private Vector3 FGetNextPosCoor(int nextLocation)
    {
        return new Vector3(Player.gameObject.transform.position.x - 1.1f, Player.gameObject.transform.position.y, Player.gameObject.transform.position.z);
    }
    private bool pauseMove = false;
    private IEnumerator SmoothLerp(Vector3 finalPos)
    {
        moving = true;
        Vector3 startingPos = Player.gameObject.transform.position;
        Debug.Log("Moving the player from " + startingPos + " to " + finalPos);
        float elapsedTime = 0;
        bool pauseCoorChange = false;
        while (elapsedTime < (1.5f - ((float)Player.getSpeed()) * 0.1f))
        {
            if (pauseMove)
            {
                if (!pauseCoorChange)
                {
                    startingPos += new Vector3(-1.1f, 0f, 0f);
                    finalPos = new Vector3(finalPos.x - 1.1f, finalPos.y, finalPos.z);
                } 
                pauseCoorChange = true;

                Debug.Log("After Force Moving, New Moving the player from " + startingPos + " to " + finalPos);
                yield return null;
            }
            else
            {
                Player.gameObject.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / (1.5f - ((float)Player.getSpeed()) * 0.1f)));
                elapsedTime += Time.deltaTime;
                pauseCoorChange = false;
                yield return null;
            }
            
        }
        moving = false;

        Debug.Log("Moving done");
    }

    private IEnumerator FSmoothLerp(Vector3 finalPos)
    {
        fmoving = true;
        pauseMove = true;
        Vector3 startingPos = Player.gameObject.transform.position;
        Debug.Log("FORCE Moving the player from " + startingPos + " to " + finalPos);
        float elapsedTime = 0;

        while (elapsedTime < 0.3f)
        {
            Player.gameObject.transform.position = Vector3.Lerp(startingPos, finalPos, elapsedTime / 0.3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pauseMove = false;
        Debug.Log("Forced Moving done");
        fmoving = false;
    }
}

