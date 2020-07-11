using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Chunk
{
    public int width, height;

    public List<String> specialBlockTypes;
    public List<int> specialPositions; 

    // Start is called before the first frame update
    void Start()
    {
        spawnPlatform();
    }

    private void spawnPlatform()
    {
        Vector2Int[] positions = new Vector2Int[width * height] ;
        int i = 0;//how many block placed so far
        int specialCount = 0;// how many specials placed so far
        Dictionary<int, String> specialTypes = new Dictionary<int, string>();//mapping of block pos to special type
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y == height - 1) {
                    //top row
                    if (specialCount < specialPositions.Count && specialPositions[specialCount] == x) {
                        //the next special block
                        specialTypes.Add(i, specialBlockTypes[specialCount]);
                        specialCount++;
                    }
                }
                positions[i] = new Vector2Int(x, y);
                i++;
            }
        }
        spawnChunk(positions, specialTypes);
    }
}
