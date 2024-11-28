using System;
using Godot;

public static class Directories {
    private const string _DataPath = "user://";
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
        public static string DataPath 
    {
        get
        {
            return ProjectSettings.GlobalizePath(_DataPath);
        }

        private set {

        }
    
    }
}