using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn_mechanic : MonoBehaviour
{
    GameObject player;
    Vector3 spawn;
    public player_move _player_move;
    public terrain_generator _terrain;

    public Health _Health;

    public event EventHandler OnHealthLoss;
    public event EventHandler OnHealthGain;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        spawn = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -6)
        {
            destroyTerrain();
            player.transform.position = spawn;
            _Health.health -= 1;
            CallHealthLoss();

        }
        if (_player_move.damage == true)
        {
            _Health.health -= 1;
            CallHealthLoss();
            _player_move.damage = false;
        }
    }

    void CallHealthLoss()
    {
        OnHealthLoss?.Invoke(this, EventArgs.Empty);
    }
    void CallHealthGain()
    {
        OnHealthGain?.Invoke(this, EventArgs.Empty);
    }
    void destroyTerrain()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("terrain");
        foreach (GameObject go in gos)
            Destroy(go);
        _terrain.spawn();
    }
}
