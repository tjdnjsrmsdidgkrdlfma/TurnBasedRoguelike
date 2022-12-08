using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count wall_count = new Count(5, 9);
    public Count food_count = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floor_tiles;
    public GameObject[] wall_tiles;
    public GameObject[] food_tiles;
    public GameObject[] enemy_tiles;
    public GameObject[] outerwall_tiles;

    Transform board_holder;
    List<Vector2> grid_position = new List<Vector2>();

    void InitializeList()
    {
        grid_position.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                grid_position.Add(new Vector2(x, y));
            }
        }
    }

    void BoardSetUp()
    {
        board_holder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject to_instantiate = floor_tiles[UnityEngine.Random.Range(0, floor_tiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                    to_instantiate = outerwall_tiles[UnityEngine.Random.Range(0, outerwall_tiles.Length)];

                GameObject instance = Instantiate(to_instantiate, new Vector2(x, y), Quaternion.identity);

                instance.transform.SetParent(board_holder);
            }
        }
    }

    Vector2 RandomPosition()
    {
        int random_index = UnityEngine.Random.Range(0, grid_position.Count);
        Vector2 random_position = grid_position[random_index];
        grid_position.RemoveAt(random_index);
        return random_position;
    }

    void LayoutObjectAtRandom(GameObject[] tile_array, int minimum, int maximum)
    {
        int object_count = UnityEngine.Random.Range(minimum, maximum + 1);

        for(int i = 0; i < object_count; i++)
        {
            Vector2 random_position = RandomPosition();
            GameObject tile_choice = tile_array[UnityEngine.Random.Range(0, tile_array.Length)];
            Instantiate(tile_choice, random_position, Quaternion.identity);
        }
    }

    public void SetupSence(int level)
    {
        BoardSetUp();
        InitializeList();
        LayoutObjectAtRandom(wall_tiles, wall_count.minimum, wall_count.maximum);
        LayoutObjectAtRandom(food_tiles, food_count.minimum, food_count.maximum);
        int enemy_count = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemy_tiles, enemy_count, enemy_count);
        Instantiate(exit, new Vector2(columns - 1, rows - 1), Quaternion.identity);
    }
}
