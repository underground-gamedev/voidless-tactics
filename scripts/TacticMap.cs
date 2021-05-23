using Godot;
using System;

public class TacticMap
{
	private bool[,] solid;
	private int width;
	private int height;

	public int Width { get => width; }
	public int Height { get => height; }
	public int TileCount { get => width * height; }
	public TacticMap(int width, int height) 
	{
		this.width = width;
		this.height = height;
		this.solid = new bool[width, height];
	}

	public void SetSolid(int x, int y, bool solid) 
	{
		this.solid[x, y] = solid;
	}

	public bool GetSolid(int x, int y)
	{
		return this.solid[x, y];
	}
}
