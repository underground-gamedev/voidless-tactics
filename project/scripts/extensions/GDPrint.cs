using Godot;

public static class GDPrint
{
	/// <summary>
	/// the point is to toggle then you want to receive logs
	/// </summary>
	public static bool active = false;
	public static void Print(string str)
	{
		if (active)
		{
			GD.Print(str);
		}
	}
}
