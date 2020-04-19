using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Xml;
using System.IO;

public class MapCreator : MonoBehaviour
{
    public Vector3Int tMapSize;
    public Tilemap topMap;

    private int[,] mapSample = {
        {1,1,1,1,1,1},
        {1,0,0,0,0,1},
        {1,1,0,0,1,1},
        {1,1,0,0,1,1},
        {1,0,0,0,0,1},
        {1,0,0,0,0,1},
        {1,1,1,1,1,1}};

    public Tile BlankTile;
    public Tile[] StoneTiles;
    public Tile[] StoneWalls;

    int width;
    int height;

    Tile getWall(string neighbors, string wallType)
    {
        int wallNum = getWallData(neighbors);

        if (wallNum == 0)
            return BlankTile;
        return StoneWalls[wallNum];
    }
    Tile getTile(string neighbors, string tileType)
    {
        int[] tileData = getTileData(neighbors); // num 1 - 14, flips, rotates

        print(tileData[0] + ", " + tileData[1] + ", " + tileData[2]);

        Vector3 tileScale = transform.localScale;
        for (int i = 0; i < tileData[1]; i++) { }
        for (int i = 0; i < tileData[2]; i++) { }

        var m = StoneTiles[0].transform;
        m.SetTRS(Vector3.zero, Quaternion.Euler(0f, 180f * (float)tileData[1], -90f * (float)tileData[2]), Vector3.one);

        StoneTiles[tileData[0]].transform = m;

        return StoneTiles[tileData[0]];
    }

    void getMap(int[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[y, x] == 1) 
                    topMap.SetTile(new Vector3Int(x - width / 2, -y + height / 2, 0), getWall(getNeighbors(y, x), "StoneWall"));
                else
                    topMap.SetTile(new Vector3Int(x - width / 2, -y + height / 2, 0), getTile(getNeighbors(y, x), "StoneTile"));
            }
        }

        
    }



    public void doMap()
    {
        clearMap();
        width = tMapSize.x;
        height = tMapSize.y;
        getMap(mapSample);
    }
    

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    doMap();
        //if (Input.GetMouseButtonDown(1))
        //    doMap();
    }

    public void clearMap()
    {
        topMap.ClearAllTiles();
    }








    bool checkTile(string neighbors, string tile)
    {
        for (int i = 0; i < 8; i++)
            if ((tile[i] != 'X') && (neighbors[i] != tile[i]))
                return false;
        return true;
    }

    void tileArrayRotate(ref string tile)
    {
        // in -> {0, 1, 2, 3, 4, 5, 6, 7}
        // out -> {6, 7, 0, 1, 2, 3, 4, 5}

        char[] temp = { tile[6], tile[7], tile[0], tile[1], tile[2], tile[3], tile[4], tile[5] };
        tile = new string(temp);
    }

    void tileArrayFlip(ref string tile)
    {
        // in -> {0, 1, 2, 3, 4, 5, 6, 7}
        // out -> {6, 5, 4, 3, 2, 1, 0, 7}

        char[] temp = { tile[6], tile[5], tile[4], tile[3], tile[2], tile[1], tile[0], tile[7] };
        tile = new string(temp);
    }

    bool checkTileO(ref int numFlips, ref int numRotates, string neighbors, string tile)
    {
        int canFlip = numFlips;
        bool flag = false;

        numFlips = 0;
        numRotates = 0;

        for (int i = 0; i < 4; i++)
        {
            flag = checkTile(neighbors, tile);
            if (flag)
                return true;
            numRotates++;
            tileArrayRotate(ref tile);
        }
        // tile in original oritentation
        // reset rotates
        numRotates = 0;

        if (canFlip == 1)
        {
            tileArrayFlip(ref tile);
            numFlips = 1;

            for (int i = 0; i < 4; i++)
            {
                flag = checkTile(neighbors, tile);
                if (flag)
                    return true;
                numRotates++;
                tileArrayRotate(ref tile);
            }
        }
        return false;
    }

    int[] getTileData(string neighbors)
    {
        //string tileKey1 = "FFFFFFFF";
        string tileKey2 = "FFXWXFFF";
        string tileKey3 = "XFXWXFXW";
        string tileKey4 = "FFXWWWXF";
        string tileKey5 = "XWWWWWXF";
        //string tileKey6 = "WWWWWWWW";
        string tileKey7 = "FFFFWFFF";
        string tileKey8 = "FFFFWFWF";
        string tileKey9 = "WFFFWFFF";
        string tileKey10 = "WFFFWFWF";
        string tileKey11 = "WFWFWFWF";
        string tileKey12 = "FFXWXFWF";
        string tileKey13 = "WFXWWWXF";
        string tileKey14 = "WFXWXFWF";

        int tileType = 0; int numFlips = 0; int numRotates = 0;
        int numFloors = 0;

        int[] retTile = { 0, 0, 0 };

        for (int i = 0; i < 8; i++) if (neighbors[i] == 'F') numFloors++;

        switch (numFloors)
        {
            case 8: // 8 floor; tile 1 only
                tileType = 1; numFlips = 0; numRotates = 0; retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile;
                break;
            case 7: // 7 floor, 1 wall; tile 2R or 7R

                // check 2R
                tileType = 2; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey2)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 7R
                tileType = 7; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey7)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                break;
            case 6: // 6 floor, 2 wall; tile 2R, 3R, 8R, 9R, or 12RF

                // check 2R
                tileType = 2; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey2)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 3R
                tileType = 3; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey3)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 8R
                tileType = 8; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey8)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 9R
                tileType = 9; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey9)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 12RF
                tileType = 12; numFlips = 1; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey12)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                break;
            case 5: // 5 floor, 3 wall; tile 2R, 3R, 4R, 10R, 12RF, or 14R

                // check 2R
                tileType = 2; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey2)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 3R
                tileType = 3; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey3)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 4R
                tileType = 4; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey4)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 10R
                tileType = 10; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey10)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 12RF
                tileType = 12; numFlips = 1; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey12)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 14R
                tileType = 14; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey14)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                break;
            case 4: // 4 floor, 4 wall; tile 3R, 4R, 11, 12R, 13R, or 14R

                // check 3R
                tileType = 3; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey3)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 4R
                tileType = 4; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey4)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 11
                tileType = 11; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey11)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 12RF
                tileType = 12; numFlips = 1; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey12)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 13R
                tileType = 13; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey13)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 14R
                tileType = 14; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey14)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                break;
            case 3: // 3 floor, 5 wall; tile 3R, 4R, 5R, 13R, or 14R

                // check 3R
                tileType = 3; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey3)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 4R
                tileType = 4; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey4)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 5R
                tileType = 5; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey5)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 13R
                tileType = 13; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey13)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 14R
                tileType = 14; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey14)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                break;
            case 2: // 2 floor, 6 wall; tile 3R, 5R, or 13R

                // check 3R
                tileType = 3; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey3)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 5R
                tileType = 5; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey5)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                // check 13R
                tileType = 13; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey13)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                break;
            case 1: // 1 floor, 7 wall; tile 5R only

                // check 5R
                tileType = 5; numFlips = 0; numRotates = 0;
                if (checkTileO(ref numFlips, ref numRotates, neighbors, tileKey5)) { retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile; }

                break;
            case 0: // 0 floor, 8 wall; tile 6 only
                tileType = 6; numFlips = 0; numRotates = 0; retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile;

                break;
            default:
                tileType = -21; numFlips = 0; numRotates = 0; retTile[0] = tileType; retTile[1] = numFlips; retTile[2] = numRotates; return retTile;
                break;
        }




        return retTile;
    }

    int getWallData(string neighbors)
    {
        int wallType = 0;
        switch (neighbors)
        {
            case "WWWWWWWW": wallType = 0; break;
            case "WWWWFWWW": wallType = 1; break;
            case "WWWWFFWW": wallType = 2; break;
            case "WWWWWFWW": wallType = 2; break;
            case "WWWWFFFW": wallType = 3; break;
            case "WWWWWFFW": wallType = 4; break;
            case "WWWWWWFW": wallType = 5; break;
            case "WWWWWWFF": wallType = 6; break;
            case "FWWWWWFF": wallType = 7; break;
            case "WWWWWWWF": wallType = 7; break;
            case "FWWWWWWF": wallType = 8; break;
            case "FWWWWWWW": wallType = 9; break;
            case "FFWWWWWW": wallType = 10; break;
            case "WFWWWWWW": wallType = 10; break;
            case "FFFWWWWW": wallType = 11; break;
            case "WFFWWWWW": wallType = 12; break;
            case "WWFWWWWW": wallType = 13; break;
            case "WWFFWWWW": wallType = 14; break;
            case "WWFFFWWW": wallType = 15; break;
            case "WWWFWWWW": wallType = 15; break;
            case "WWWFFWWW": wallType = 16; break;
            default:
                if ((neighbors[3] == 'F') && (neighbors[5] == 'F')) wallType = 17;
                else if ((neighbors[5] == 'F') && (neighbors[7] == 'F')) wallType = 18;
                else if ((neighbors[1] == 'F') && (neighbors[7] == 'F')) wallType = 19;
                else if ((neighbors[1] == 'F') && (neighbors[3] == 'F')) wallType = 20;
                else wallType = 0;
                break;
        }
        return wallType;
    }

    string getNeighbors(int x, int y)
    {
        // 2 3 4
        // 1 T 5
        // 0 7 6

        char[] neighbors = { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W' };

        try { if (mapSample[x + 1, y - 1] == 0) neighbors[0] = 'F'; } catch { }
        try { if (mapSample[x, y - 1] == 0) neighbors[1] = 'F'; } catch { }
        try { if (mapSample[x - 1, y - 1] == 0) neighbors[2] = 'F'; } catch { }
        try { if (mapSample[x - 1, y] == 0) neighbors[3] = 'F'; } catch { }
        try { if (mapSample[x - 1, y + 1] == 0) neighbors[4] = 'F'; } catch { }
        try { if (mapSample[x, y + 1] == 0) neighbors[5] = 'F'; } catch { }
        try { if (mapSample[x + 1, y + 1] == 0) neighbors[6] = 'F'; } catch { }
        try { if (mapSample[x + 1, y] == 0) neighbors[7] = 'F'; } catch { }

        return new string(neighbors);
    }
}