using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    [SerializeField] private DataBase data;


    private void Awake()
    {
        SaveData sd = new SaveData();

        sd.IdBlocks = new int[4] { 1, 2, 3, 4 };
        sd.CountBlocks = new int[4] { 10, 20, 30, 40 };

        StaticData.saveData = sd;
    }
}
