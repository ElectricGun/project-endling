IMPORTANT

Current Save Schema:

saveName     : string
revision     : int
currentLevel : int
playerData : Dictionary {
    checkpoint : int
    positionX  : int
    positionY  : int
    velocityX  : int
    velocityY  : int
}
savedLevels : Array of Dictionaries [
    {
        levelObjects : Array of Dictionaries {
            positionX : int
            positionY : int
            velocityX : int
            velocityY : int
        }
    }
]