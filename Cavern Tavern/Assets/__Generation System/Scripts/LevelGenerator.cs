using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    int[,] generatedMap;
    public int height;
    public int width;
    public string seed;
    private System.Random pseudoRandom;

    public Tilemap backgroundTilemap;
    public Tilemap obstacleTilemap;
    public RuleTile grass;
    public RuleTile water;


    [Range(0, 100)]
    public int fillingPercentage;

    void Start()
    {
        CellularAutomata();
        placeTiles();
    }

    #region map generation
    void CellularAutomata()
    {
        seed = (seed.Length <= 0) ? Time.time.ToString() : seed;
        pseudoRandom = new System.Random(seed.GetHashCode());

        GenerateMap();

        for (int i = 0; i < 5; i++)
            SmoothMap();

        RemoveSecludedCells();
        RecoverEdgeCells();
    }

    void GenerateMap()
    {
        generatedMap = new int[width, height];

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                generatedMap[x, y] = (pseudoRandom.Next(0, 100) < fillingPercentage) ? 1 : 0;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            printMap();
        }
    }
    void printMap()
    {
        string arrayString = "";
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                arrayString += string.Format("{0} ", generatedMap[i, j]);
            }
            arrayString += System.Environment.NewLine + System.Environment.NewLine;
        }
        Debug.Log(arrayString);
    }

    int getNeighboursCellCount(int x, int y, int[,] map)
    {
        int neighbors = 0;
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                neighbors += map[i + x, j + y];

        neighbors -= map[x, y];

        return neighbors;
    }

    void SmoothMap()
    {
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                int neighbors = getNeighboursCellCount(x, y, generatedMap);

                // enforcing rule for cave generation by Johnson et. al.
                // source: https://www.researchgate.net/publication/228919622_Cellular_automata_for_real-time_generation_of
                // T > 4 => C = true
                // T = 4 => C = C
                // T < 4 => C = false

                if (neighbors > 4)
                    generatedMap[x, y] = 1;
                else if (neighbors < 4)
                    generatedMap[x, y] = 0;
            }
        }
    }

    void RecoverEdgeCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    generatedMap[x, y] = 0;
            }
        }
    }

    void RemoveSecludedCells()
    {
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                generatedMap[x, y] = (getNeighboursCellCount(x, y, generatedMap) <= 0) ? 0 : generatedMap[x, y];
            }
        }

    }

    #endregion

    #region tile placement
    
    void placeTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(generatedMap[x,y] == 0)
                    backgroundTilemap.SetTile(new Vector3Int(x, y, 0), grass);
                else if (generatedMap[x, y] == 1)
                    obstacleTilemap.SetTile(new Vector3Int(x, y, 0), water);       
            }
        }
    }

    #endregion
}
