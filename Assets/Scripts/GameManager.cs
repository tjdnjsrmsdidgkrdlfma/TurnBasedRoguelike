using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager board_script;

    int level = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        board_script = GetComponent<BoardManager>();
        InitialGame();
    }

    void InitialGame()
    {
        board_script.SetupSence(level);
    }
}
