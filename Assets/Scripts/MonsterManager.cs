using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance;
    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Monster3;
    public GameObject[] Monsters;

    void Awake()
    {
        Instance = this;
        Monsters = new GameObject[3];
        Monsters[0] = Monster1;
        Monsters[1] = Monster2;
        Monsters[2] = Monster3;
    }

    void Start()
    {
        for (int i = 0; i < 25; i++)
            CreateMonster();
    }

    void Update()
    {
    }

    public void CreateMonster()
    {
        int index = UnityEngine.Random.Range(0, 3);
        GameObject monster = Instantiate(Monsters[index]);
    }
}