using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - North, 1 - South, 2 - East, 3 - West
    public GameObject[] doors;
    public GameObject[] passages;
    public GameObject[] lamps;
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
            passages[i].SetActive(status[i]);
            lamps[i].SetActive(status[i]);
        }
    }
}
