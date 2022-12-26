using CraftsmanHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public GameObject CurrentPlayer;

    public void SetCurrentPlayer(GameObject player) {
        CurrentPlayer = player;
    }
}