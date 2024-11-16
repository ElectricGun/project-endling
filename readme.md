IMPORTANT

Current Save Schema (revision 2):

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
savedLevels : Dictionary {
    levelData : Dictionary {
            levelObjects : Array [
                objectData : Dictionary : {
                    isSpawned : bool
                    nodePath  : string
                    nodeType  : type
                    positionX : int
                    positionY : int
                    velocityX : int
                    velocityY : int
                }
            ]
        }
}