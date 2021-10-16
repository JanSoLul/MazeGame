using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject wall_tile;
    public GameObject road_tile;
    public GameObject WOF;
    public GameObject character;
    public GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        int[,] maze = {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,2,1,0,0,0,1,1,0,0,0,0,1,1,1,1,1},
            {1,0,0,0,1,1,1,0,0,1,1,0,0,1,1,1,1},
            {1,0,1,0,0,0,0,0,1,1,1,1,0,0,0,1,1},
            {1,1,1,0,1,1,1,0,0,1,1,0,0,1,0,0,1},
            {1,1,1,0,1,0,0,1,0,0,1,1,0,1,1,1,1},
            {1,0,0,0,1,0,1,1,1,0,1,0,0,1,0,1,1},
            {1,0,1,1,1,0,0,0,0,1,1,1,1,1,0,1,1},
            {1,0,0,1,1,0,1,1,0,1,1,1,1,1,0,1,1},
            {1,1,0,0,0,0,1,1,0,1,0,0,0,0,0,0,1},
            {1,0,0,1,0,1,1,1,0,0,0,1,1,1,1,0,1},
            {1,0,1,0,0,0,1,0,1,1,0,1,0,1,1,0,1},
            {1,0,0,1,1,0,1,0,1,1,1,0,0,0,1,0,1},
            {1,1,0,1,0,0,0,0,1,0,0,0,1,0,0,0,1},
            {1,0,0,1,0,1,1,0,1,0,1,1,1,0,1,0,1},
            {1,0,1,0,0,1,0,0,1,0,0,0,1,1,1,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,3,1}};
        for (int x=0; x<17; x++)
        {
            for (int y=0; y<17; y++)
            {
                if (maze[x, y] == 1)
                {
                    GameObject new_wall = Instantiate(wall_tile);
                    new_wall.transform.position = new Vector3(x, y, 0);
                }
                else
                {
                    GameObject new_road = Instantiate(road_tile);
                    new_road.transform.position = new Vector3(x, y, 0);
                    if (maze[x, y] == 2)
                    {
                        character.transform.position = new Vector3(x, y, 0);
                    }
                    else if (maze[x, y] == 3)
                    {
                        GameObject maze_goal = Instantiate(goal);
                        maze_goal.transform.position = new Vector3(x, y, 0);
                    }
                }
                GameObject new_WOF = Instantiate(WOF);
                new_WOF.transform.position = new Vector3(x, y, -1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
