using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DungeonEnemyRoom : DungeonRoom
{
    public Door[] doors;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private void Start()
    {
        OpenDoors();
    }

    public int EnemiesActive()
    {
        int activeEnemies = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.activeInHierarchy)
            {
                activeEnemies++;
            }
        }
        return activeEnemies;
    }

    public void CheckEnemies()
    {
        if (EnemiesActive() == 1)
        {
            OpenDoors();
            dialogBox.SetActive(true);
            dialogText.text = dialog;
            StartCoroutine(Co_ForEnd(2));
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            CloseDoors();
            StartCoroutine(Co_OnEnter(2));
            virtualCamera.SetActive(true);

        }

    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Deactivate all enemies and pots
            //Activate all enemies and pots
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.SetActive(false);

        }
    }

    public void CloseDoors()
    {
        for(int i = 0; i < doors.Length; i ++)
        {
            doors[i].Close();
        }
        Debug.Log("Close Doors");
    }

    public void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Open();
        }
        Debug.Log("Open Doors");
    }

    private IEnumerator Co_ForEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }

    private IEnumerator Co_OnEnter(float delay)
    {
        dialogBox.SetActive(true);
        dialogText.text = "Ha-ha, you will have to destroy all enemies if you want to exit this room !";
        yield return new WaitForSeconds(delay);
        dialogBox.SetActive(false);
    }
}
