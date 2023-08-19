using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;

    [SerializeField] private int roomMapSize = 20;
    [SerializeField] private int mapBoundaries = 15;

    [SerializeField] private int minRooms;
    [SerializeField] private int maxRooms;

    private RoomReference[,] roomsToGenerate;

    private List<RoomReference> roomHolder = new List<RoomReference>();
    private Queue<RoomReference> roomsQueue = new Queue<RoomReference>();

    private int maxRoomCount;
    private int roomCount = 0;

    private int spawnOffset;

    private class RoomReference
    {
        private const int sideCount = 4;
        private Vector2[] roomDirs = new Vector2[sideCount] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };


        public Vector2 parentDir;
        public Vector2 posInArray;

        public bool[] activeSides = new bool[sideCount];

        public Dictionary<Vector2, bool> sideMarker = new Dictionary<Vector2, bool>() {
            { Vector2.up, false },
            { Vector2.right, false },
            { Vector2.down, false },
            { Vector2.left, false },
        };

        public bool generatedChildren = false;
        public bool isGenerated = false;
        public bool isStarting = false;


        private int minNeighbouringRooms = 2;

        public Vector2[] getNeihbourRooms()
        {

            if (isStarting)
            {

                return generateNeighbours(minNeighbouringRooms, 4);
            }

            return generateNeighbours(minNeighbouringRooms, 3);

        }

        private Vector2[] generateNeighbours(int maxNeigbours, int minNeighbours)
        {
            int neighbourCount = Random.Range(minNeighbours, maxNeigbours);
            Vector2[] randomRooms = new Vector2[neighbourCount];

            for (int i = 0; i < neighbourCount; i++)
            {
                int randInt = Random.Range(0, roomDirs.Length);
                Vector2 addedDir = roomDirs[randInt];

                while (randomRooms.Contains(addedDir))
                {
                    randInt = Random.Range(0, roomDirs.Length);
                    addedDir = roomDirs[randInt];
                }

                randomRooms[i] = addedDir;

            }
            return randomRooms;
        }
    }

    void Start()
    {
        roomsToGenerate = new RoomReference[mapBoundaries, mapBoundaries];
        maxRoomCount = Random.Range(minRooms, maxRooms);

        GenerateMap();
        testRooms();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ReloadGenerator();
        }
    }

    private void ReloadGenerator()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GenerateMap()
    {
        RoomReference startingRoom = new RoomReference();

        startingRoom.isStarting = true;
        startingRoom.posInArray = new Vector2(mapBoundaries / 2, mapBoundaries / 2);

        roomsToGenerate[mapBoundaries / 2, mapBoundaries / 2] = startingRoom;
        roomsQueue.Enqueue(startingRoom);
        roomHolder.Add(startingRoom);


        while (roomCount < maxRoomCount && roomsQueue.Count > 0)
        {

            RoomReference currentRoom = roomsQueue.Dequeue();

            Vector2[] newDirections = { };

            Vector2 roomPos = currentRoom.posInArray;

            newDirections = currentRoom.getNeihbourRooms();

            

            foreach (Vector2 dir in newDirections)
            {
                RoomReference generatedRoom = new RoomReference();

                generatedRoom.posInArray = dir + roomPos;

                int coordX = (int)generatedRoom.posInArray.x;
                int coordY = (int)generatedRoom.posInArray.y;

                generatedRoom.parentDir = roomPos - generatedRoom.posInArray;

                if (roomsToGenerate[coordX, coordY] == null && ValidateCoordinates(coordX, coordY, 0, mapBoundaries))
                {

                    generatedRoom.sideMarker[-dir] = true;


                    currentRoom.sideMarker[dir] = true;

                    roomsToGenerate[coordX, coordY] = generatedRoom;

                    roomHolder.Add(generatedRoom);
                    roomsQueue.Enqueue(generatedRoom);

                    roomCount++;
                }
            }

            currentRoom.isGenerated = true;
        }
    }

    private void testRooms()
    {
        spawnOffset = (mapBoundaries / 2);

        foreach (var item in roomHolder)
        {

            int coordX = (int)item.posInArray.x - spawnOffset;
            int coordY = (int)item.posInArray.y - spawnOffset;

            Vector3 actualPos = new Vector3(coordX * roomMapSize, coordY * roomMapSize, 3);

            
            var activeSides = item.sideMarker.Values.ToArray();


            GameObject r = Instantiate(roomPrefab, actualPos, Quaternion.identity);
            r.transform.parent = gameObject.transform;

            r.GetComponent<DungeonRoomManager>().SetActiveEntrances(activeSides);
        }
    }

    private bool ValidateCoordinates(int x, int y, int minCoord, int maxCoord)
    {
        if (x >= minCoord && x <= maxCoord)
        {
            if (y >= minCoord && y <= maxCoord)
            {
                return true;
            }
        }

        return false;
    }
}
