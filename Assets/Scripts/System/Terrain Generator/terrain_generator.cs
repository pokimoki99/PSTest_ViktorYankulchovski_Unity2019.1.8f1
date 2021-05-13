using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class terrain_generator : MonoBehaviour
{

    public GameObject Ground_big_easy;
    public GameObject Ground_big2_easy;
    public GameObject Ground_small_easy;
    public GameObject Ground_big_medium;
    public GameObject Ground_big2_medium;
    public GameObject Ground_small_medium;
    public GameObject hp_power_up;
    public GameObject detection;
    public GameObject player;

    public Detecc detecc;

    List<GameObject> terrains;

    public Score score;

    Vector2 whereToSpawn;
    int num;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        terrains = new List<GameObject>();

        spawn();

    }
    // Update is called once per frame
    void Update()
    {
        if (detecc.switched)
        {
            Random_pos();

            if (score.scoreValue >= 0 && score.scoreValue <= 50 || DifficultyManager.Instance.easy)
            {
                if (num == 0)
                {
                    whereToSpawn = new Vector2(player.transform.position.x + 9, Ground_big_easy.transform.position.y - 3.5f);
                    terrains.Add(Instantiate(Ground_big_easy, whereToSpawn, Quaternion.identity));
                    detecc.switched = false;
                    Debug.Log("spawned0");

                }
                else if (num == 1)
                {
                    whereToSpawn = new Vector2(player.transform.position.x + 9, Ground_big2_easy.transform.position.y - 3.5f);
                    terrains.Add(Instantiate(Ground_big2_easy, whereToSpawn, Quaternion.identity));
                    detecc.switched = false;
                    Debug.Log("spawned1");
                }
                else if (num == 2)
                {
                    whereToSpawn = new Vector2(player.transform.position.x + 3, Ground_small_easy.transform.position.y - 3.5f);
                    terrains.Add(Instantiate(Ground_small_easy, whereToSpawn, Quaternion.identity));
                    detecc.switched = false;
                    Debug.Log("spawned2");
                }
                if (hp == 0)
                {
                    whereToSpawn = new Vector2(player.transform.position.x + 14, hp_power_up.transform.position.y);
                    terrains.Add(Instantiate(hp_power_up, whereToSpawn, Quaternion.identity));
                }


            }
            if (score.scoreValue > 50 || DifficultyManager.Instance.hard)
            {
                Random_pos();
                if (num == 0)
                {
                    whereToSpawn = new Vector2(player.transform.position.x - 9, Ground_big_medium.transform.position.y - 3.5f);
                    terrains.Add(Instantiate(Ground_big_medium, whereToSpawn, Quaternion.identity));
                    detecc.switched = false;
                }
                if (num == 1)
                {
                    whereToSpawn = new Vector2(player.transform.position.x - 9, Ground_big2_medium.transform.position.y - 3.5f);
                    terrains.Add(Instantiate(Ground_big2_medium, whereToSpawn, Quaternion.identity));
                    detecc.switched = false;
                }
                if (num == 2)
                {
                    whereToSpawn = new Vector2(player.transform.position.x - 14, Ground_small_medium.transform.position.y - 3.3f);
                    terrains.Add(Instantiate(Ground_small_medium, whereToSpawn, Quaternion.identity));
                    detecc.switched = false;
                }
            }

            Debug.Log("new platform");
            detecc.switched = false;
        }

        if (detecc.recentre)
        {
            detecc.recentre = false;
            Delete_Terrain();
            spawn();

        }
    }

    public void spawn()
    {
        if (score.scoreValue >= 0)
        {
            whereToSpawn = new Vector3(0.5f, -5.32f, 16.54f);
            terrains.Add(Instantiate(Ground_big2_easy, whereToSpawn, Quaternion.identity));
            player.transform.position = new Vector2(-1f, player.transform.position.y);
        }
        if (score.scoreValue >= 50)
        {
            whereToSpawn = new Vector3(0.4f, -5.32f, 16.54f);
            terrains.Add(Instantiate(Ground_big2_medium, whereToSpawn, Quaternion.identity));
            player.transform.position = new Vector2(-3f, player.transform.position.y);
        }
    }
    void Random_pos()
    {
        num = (Random.Range(0, 3));
        hp = (Random.Range(0, 2));
        Debug.Log(num);
    }

    void Delete_Terrain()
    {
        foreach (var terrain in terrains)
        {
            Destroy(terrain);
        }
    }
}
