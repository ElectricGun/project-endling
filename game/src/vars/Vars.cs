using System;
using Godot.Collections;
public static class Vars {
    public static float TransitionSpeed = 3;

    public static readonly Array<string> Wordlist = new() {
        //colours
        "red", "green", "blue", "yellow", "white", "purple", "pink",

        //objects
        "lamp",

        //adjectives
        "tall", "short", "big", "small"
    };

    public static readonly Array<string> SignificantWords = new() {
        //significant
        "colour" 
    };
}