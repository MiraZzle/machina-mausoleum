using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;

    // Number of rooms that make up the boundaries of the map
    [SerializeField] private int mapBoundaries = 15;
    [SerializeField] private int keyRoomFromLast = 3;
    [SerializeField] private int minRooms;
    [SerializeField] private int maxRooms;

    // 2D array to store references to generated rooms
    private RoomReference[,] roomsToGenerate;

    // List to hold references to all generated rooms
    private List<RoomReference> roomHolder = new List<RoomReference>();

    // Queue for BFS of room generation
    private Queue<RoomReference> roomsQueue = new Queue<RoomReference>();

    // Size of each room in the map - actual size in editor
    [SerializeField] private int roomMapSize = 20;

    private int maxRoomCount;
    private int roomCount = 0;

    // Offset for spawning rooms
    private int spawnOffset;

    // Definition of a room reference class
    private class RoomReference
    {
        private const int sideCount = 4;
        private Vector2[] roomDirs = new Vector2[sideCount] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        // Store the direction from the parent room to this room
        public Vector2 parentDir;
        public Vector2 posInArray;

        // An array to track which sides of this room are active or connected to other rooms
        public bool[] activeSides = new bool[sideCount];

        // A dictionary to mark the status of each side (true if connected, false if not)
        public Dictionary<Vector2, bool> sideMarker = new Dictionary<Vector2, bool>() {
            { Vector2.up, false },
            { Vector2.right, false },
            { Vector2.down, false },
            { Vector2.left, false },
        };

        public bool generatedChildren = false;
        public bool isGenerated = false;

        private int neighbourRoomCount = 3;
        private int minNeighbouringRooms = 2;

        public roomTypes roomType;

        // Enum to define the possible types of rooms
        public enum roomTypes {
            normal,
            key,
            spawn,
            exit
        }

        // Determine the number of neighboring rooms based on the room type
        public Vector2[] getNeihbourRooms()
        {
            if (roomType == roomTypes.spawn)
            {
                return generateNeighbours(minNeighbouringRooms, sideCount);
            }

            return generateNeighbours(minNeighbouringRooms, neighbourRoomCount);
        }

        // Generate an array of neighboring rooms' directions based on specified counts
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
        InstantiateRooms();
    }

    // Generate the dungeon map
    private void GenerateMap()
    {
        // Create the starting room reference
        RoomReference startingRoom = new RoomReference();

        startingRoom.roomType = RoomReference.roomTypes.spawn;

        // Set the position of the starting room at the center of the map
        startingRoom.posInArray = new Vector2(mapBoundaries / 2, mapBoundaries / 2);

        roomsToGenerate[mapBoundaries / 2, mapBoundaries / 2] = startingRoom;

        // Enqueue the starting room for further processing
        roomsQueue.Enqueue(startingRoom);
        roomHolder.Add(startingRoom);

        // Continue generating rooms using BFS
        while (roomCount < maxRoomCount && roomsQueue.Count > 0)
        {
            RoomReference currentRoom = roomsQueue.Dequeue();

            Vector2[] newDirections = { };
            Vector2 roomPos = currentRoom.posInArray;

            newDirections = currentRoom.getNeihbourRooms();

            foreach (Vector2 dir in newDirections)
            {
                RoomReference generatedRoom = new RoomReference();

                // Set the position of the generated room
                generatedRoom.posInArray = dir + roomPos;

                int coordX = (int)generatedRoom.posInArray.x;
                int coordY = (int)generatedRoom.posInArray.y;

                // Calculate the direction from the current room to the generated room
                generatedRoom.parentDir = roomPos - generatedRoom.posInArray;

                if (roomsToGenerate[coordX, coordY] == null && ValidateCoordinates(coordX, coordY, 0, mapBoundaries))
                {
                    // Mark the sides between the current room and the generated room as connected
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

        roomHolder[roomHolder.Count - keyRoomFromLast].roomType = RoomReference.roomTypes.key;
        roomHolder.Last().roomType = RoomReference.roomTypes.exit;
    }

    // Instantiate and position the actual room prefabs in the dungeon
    private void InstantiateRooms()
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

            // Set the active entrances of the room based on activeSides
            DungeonRoomManager currentRoomManager = r.GetComponent<DungeonRoomManager>();
            currentRoomManager.SetActiveEntrances(activeSides);

            // Set room type to room manager
            switch (item.roomType)
            {
                case RoomReference.roomTypes.normal:
                    currentRoomManager.roomType = DungeonRoomManager.roomTypes.normal;
                    break;
                case RoomReference.roomTypes.exit:
                    currentRoomManager.roomType = DungeonRoomManager.roomTypes.exit;
                    currentRoomManager.isExitRoom = true;
                    break;
                case RoomReference.roomTypes.key:
                    currentRoomManager.roomType = DungeonRoomManager.roomTypes.key;
                    break;
                case RoomReference.roomTypes.spawn:
                    currentRoomManager.roomType = DungeonRoomManager.roomTypes.spawn;
                    currentRoomManager.isSpawnRoom = true;
                    break;
            }
        }
    }

    // Check if the given coordinates (x, y) are within the valid range defined by minCoord and maxCoord
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
