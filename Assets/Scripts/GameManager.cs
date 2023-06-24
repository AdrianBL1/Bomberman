using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;

    [Header("WinScene")]
    public string scene;

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }

        if (aliveCount <= 1)
        {
            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(scene);
    }
}
