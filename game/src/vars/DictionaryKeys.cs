using System.Reflection.Metadata;

public static class DictionaryKeys {
    public const string 
        // global
        KeyFormatRevision  = "formatRevision",
        KeyCurrentLevel = "currentLevel",
        KeySavedLevels = "savedLevels",
        KeyUnlockedWords = "unlockedWords",

        // objectdata
        KeyPositionX = "positionX",
        KeyPositionY = "positionY",
        KeyVelocityX = "velocityX",
        KeyVelocityY = "velocityY",
        KeyCheckpoint= "checkpoint",
        KeyNodePath = "nodePath",
        KeyNodeType = "nodeType",
        KeyIsSpawned = "isSpawned",
        KeyPlayerAlreadySpawned = "pAlreadySpawned", 

        // leveldata
        KeyLevelObjects = "levelObjects",
        KeySaveName  = "saveName",
        KeyPlayerData = "playerData",


        // camera data
        KeyCameraFollowObject = "cameraFollowObject",
        KeyCameraLerpSpeed = "cameraLerpSpeed",

        // state/statemachine
        KeyStateNodeName = "stateNodeName",
        KeyStateFlags = "stateFlags"

        ;
}