An unfinished concept of a word themed puzzle metroidvania. Not intended to be functional

---------- ORIGINAL README BELOW ----------

IMPORTANT

Save Schema (revision 2):

    saveName     : string
    revision     : int
    currentLevel : string

    unlockedWords : Array

    savedLevels : Dictionary {
        playerData : Dictionary {
            checkpoint : int
            positionX  : int
            positionY  : int
            velocityX  : int
            velocityY  : int
        }
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

Settings Schema

    isFullScreen : bool
