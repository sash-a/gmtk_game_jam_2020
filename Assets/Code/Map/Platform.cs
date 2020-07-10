using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Chunk
{
    public int width, height;

    // Start is called before the first frame update
    void Start()
    {
        spawnPlatform();
    }

    private void spawnPlatform()
    {
        Vector2Int[] positions = new Vector2Int[width * height] ;
        int i = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                positions[i] = new Vector2Int(x, y);
                i++;
            }
        }
        spawnChunk(positions);
    }
}
