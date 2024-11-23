using System;
using Godot.Collections;
public static class Vars {
    public static float TransitionSpeed = 3;

    public static Array<string> Wordlist = new() {
        //significant
        "colour", 
        
        //colours
        "red", "green", "blue", "yellow", "white", "purple", "pink",

        //objects
        "lamp",

        //adjectives
        "tall", "short", "big", "small"
    };
}