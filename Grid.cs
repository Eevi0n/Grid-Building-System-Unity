using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    
    public Grid(int width, int height,  float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        //Makes grid
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Adds numbers to grid
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);

                //Draws grid
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+ 1, y), Color.white, 100f);
            }
        }
        //Draws lines to complete grid
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    //returns world position
    private Vector3 GetWorldPosition(int x,  int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //returns XY  cordeninates
    private void GetXY(Vector3 WorldPosition, out int x, out int y)
    {
        x= Mathf.FloorToInt((WorldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((WorldPosition - originPosition).y / cellSize);
    }

    //sets spot on grid to equal a value
    public void setValue(int x, int y, int value)
    {
        //Checks if xand c are valid if not ignores
        if(x>= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    //sets value based on cordanites
    public void setValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        setValue(x, y, value);
    }

    //returns value in coordenites
    public int getValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }

        else
        {
            return 0;
        }
    }

    //returns value of a world position
    public int getValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return getValue(x, y);
    }
}
