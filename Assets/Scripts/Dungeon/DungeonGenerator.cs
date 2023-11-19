using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonGenerator : MonoBehaviour
{
    public class Cell // 0 - North, 1 - South, 2 - East, 3 - West
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }
    public GameObject room;


    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn, 1 - can spawn, 2 - HAS to spawn

            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }

    }
    //size of dungeon in 2D
    public Vector2Int size;

    //start position of dungeon
    public int startPos = 0;

    public Rule[] rooms;

    public GameObject[] corridorsObjects;

    //The distance between each room
    public Vector2 offset;

    //Our board of dungeon rooms for the DFS algorithm to work
    List<Cell> board;

    //Our board of dungeon corridors
    Dictionary<(int, int), bool> corridors;

    //Start is called before the first frame update
    void Start()
    {
        Random.seed = 42;
        MazeGenerator();
    }

    void GenerateDungeon()
    {

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityOfSpawning(i, j);

                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if (randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            randomRoom = 0;
                        }
                    }


                    //Instantiate the main rooms
                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(2 * i * offset.x, 0, 2 * -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();

                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name = "Room " + i + "-" + j;

                }
            }
        }

        foreach (var tuple in corridors.Keys)
        {
            int rowStart = tuple.Item1 / size.x;
            int colStart = tuple.Item1 % size.x;

            int rowEnd = tuple.Item2 / size.x;
            int colEnd = tuple.Item2 % size.x;

            GameObject corridor;

            if (corridors[tuple])
            {
                //Horizontal
                corridor = corridorsObjects[0];
                var newCorridor = Instantiate(corridor, new Vector3(Mathf.Abs((colStart * offset.x * 2) + (colEnd * offset.x * 2)) / 2, 0, -rowStart * 2 * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newCorridor.name = "Corridor " + tuple.Item1 + "->" + tuple.Item2;
            }
            else
            {
                //Vertical
                corridor = corridorsObjects[0];
                var newCorridor = Instantiate(corridor, new Vector3(2 * colStart * offset.x, 0, -Mathf.Abs((rowStart * offset.x * 2) + (rowEnd * offset.x * 2)) / 2), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newCorridor.name = "Corridor " + tuple.Item1 + "->" + tuple.Item2;
                newCorridor.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                /*                newCorridor.transform.localRotation = Quaternion.Euler(new Vector3Int(0, 90, 0));
                */
            }
        }

    }

    void MazeGenerator()
    {
        board = new List<Cell>();
        corridors = new Dictionary<(int row, int col), bool>();

        //Add all the cells that the algorithm must have
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        //Keeps the position that we are now
        int currentCell = startPos;

        //Calling path
        Stack<int> path = new Stack<int>();

        /*int k = 0;

        //Show us the limit of the dungeon to be sure to exit. we can define it also as while(true)
        int limit = 1000;*/

        while (true)
        {
            /*k++;*/

            //We are sure that we always check the current node
            board[currentCell].visited = true;

            //Found the end
            if (currentCell == board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            //No available neighbors
            if (neighbors.Count == 0)
            {
                //We reached the last cell on the path
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    //There are more cells on the path
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                //Choose a neighbor at random
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //south or east
                    if (newCell - 1 == currentCell)
                    {
                        //East
                        corridors.Add((currentCell, newCell), true);
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        //South
                        corridors.Add((currentCell, newCell), false);
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //north or west
                    if (newCell + 1 == currentCell)
                    {
                        //West
                        corridors.Add((currentCell, newCell), true);
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        //North
                        corridors.Add((currentCell, newCell), false);
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check north neighbor
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        //check south neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        //check east neighbor
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        //check west neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }
}

