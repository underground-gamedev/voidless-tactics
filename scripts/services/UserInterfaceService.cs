using Godot;
using System;

public static class UserInterfaceService
{
    private static object locker = new object();
    private static Node hud;
    public static T GetHUD<T>() where T: Node
    { 
        lock (locker)
        {
            return hud as T; 
        }
    }
    public static void SetHUD(Node hud)
    {
        lock (locker)
        {
            UserInterfaceService.hud = hud;
        }
    }
}