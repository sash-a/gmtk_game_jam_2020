using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Chunk
{
    public int width, height;
    void Start()
    {
        if(width< 5 || height < 5)
        {
            throw new System.Exception("cages must be at least 4 blocks in each dim");
        }
        spawnCage();
    }

    private void spawnCage()
    {
        Vector2Int[] positions = new Vector2Int[2* width + 2 * (height-2)];
        int i = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y != 0 && y != height - 1 && x != 0 && x != width - 1) {
                    //if not a boundry block
                    continue;
                }

                positions[i] = new Vector2Int(x, y);
                i++;
            }
        }
        spawnChunk(positions);
    }
}
