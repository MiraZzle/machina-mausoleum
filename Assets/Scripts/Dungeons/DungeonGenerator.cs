using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;

    [SerializeField] private int roomMapSize = 20;
    [SerializeField] private int mapBoundaries = 15;

    [SerializeField] private int minRooms;
    [SerializeField] private int maxRooms;

    private RoomReference[,] roomsToGenerate;
    private GameObject[] rooms;

    Stack<RoomReference> roomsStack;

    private class RoomReference
    {
        public Vector2 parentDir;
        public Vector2 posInArray;

        public bool[] activeSides = new bool[4];
        public bool generatedChildren = false;
        public bool isGenerated = false;

        private int maxNewRooms = 2;
    }

    void Start()
    {
        roomsToGenerate = new RoomReference[mapBoundaries, mapBoundaries]; 
    }

    void Update()
    {
        
    }

    private void MakeChildRooms()
    {

    }

    private void GenerateMap()
    {
        
    }

    private void InitRooms(RoomReference[,] roomMap)
    {

    }
}
