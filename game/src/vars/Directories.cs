using System;
using Godot;

public static class Directories {
    public static string SaveDir = "user://saves"; 

    public static string SaveDirGlobal 
    {
        get
        {
            return ProjectSettings.GlobalizePath(SaveDir);
        }

        private set {

        }
    
    }
}