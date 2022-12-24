using CraftsmanHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    public PlayerControl input;

    protected override void Awake() {
        base.Awake();
        input = new PlayerControl();
    }

    private void OnEnable() {
        input.Enable();
    }

    private void OnDisable() {
        input.Disable();
    }
}