using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Chunk
{
    public int width, height;

    public List<String> specialBlockTypes;
    public List<int> specialPositions;

    public static string BLOCK_POSITION = "pos";

    public override void start()
    {
        base.start();
        sortSpecialBlocks();
        spawnPlatform();
    }

    private void sortSpecialBlocks()
    {//ensures positions increase in list orderS
        for (int j = 0; j <= specialPositions.Count - 2; j++)
        {
            for (int i = 0; i <= specialPositions.Count - 2; i++)
            {
                if (specialPositions[i] > specialPositions[i + 1])
                {
                    int tempS = specialPositions[i + 1];
                    string tempT = specialBlockTypes[i + 1];

                    specialPositions[i + 1] = specialPositions[i];
                    specialBlockTypes[i + 1] = specialBlockTypes[i];

                    specialPositions[i] = tempS;
                    specialBlockTypes[i] = tempT;
                }
            }
        }
    }

    private void spawnPlatform()
    {
        Vector2Int[] positions = new Vector2Int[width * height] ;
        int i = 0;//how many block placed so far
        int specialCount = 0;// how many specials placed so far
        Dictionary<int, String> specialTypes = new Dictionary<int, string>();//mapping of block pos to special type
        Dictionary<int, String> arguments = new Dictionary<int, string>();//mapping of block pos to special blocks args
        for (int y = 0; y < height; y++)
        {
            string verticalPrefix = (y == height - 1 ? "T" : (y == 0 ? "B" : ""));
            for (int x = 0; x < width; x++)
            {
                string horizontalPrefix = (x == width - 1 ? "R" : (x == 0 ? "L" : ""));
                if (y == height - 1)
                {
                    //top row
                    if (specialCount < specialPositions.Count)
                    {//still specials left
                        string specialType = specialBlockTypes[specialCount];
                        if (specialPositions[specialCount] == x)
                        {
                            //the next special block
                            if (specialType.Contains("{"))
                            {//contains args
                                string args = specialType.Split('{')[1].Split('}')[0]; //sep args
                                arguments.Add(i, args);
                                specialType = specialType.Split('{')[0]; // remove args from type
                            }

                            specialTypes.Add(i, specialType);
                            specialCount++;
                        }
                    }
                }


                string posArg = BLOCK_POSITION + verticalPrefix + horizontalPrefix;

                if (!arguments.ContainsKey(i))
                {
                    arguments.Add(i, posArg);
                }
                else {
                    arguments[i] = arguments[i] + "," + posArg;
                }

                positions[i] = new Vector2Int(x, y);
                i++;
            }
        }
        spawnChunk(positions, specialTypes, arguments);
    }
}
