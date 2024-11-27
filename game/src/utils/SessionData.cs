using Godot;

namespace utils;

public class SessionData {
    public static string LastLoadedSaveDirectory;
    //public static string LastLoadedSaveName;
    public static Godot.Collections.Array UnlockedWords = new Godot.Collections.Array() {};

    public static bool LearnWord (string word) {
        if (!UnlockedWords.Contains(word)) {
            UnlockedWords.Add(word);
            GD.Print("[SessionData.LearnWord] Learned word " + word);
            return true;
        } else {
            return false;
        };
    }
}