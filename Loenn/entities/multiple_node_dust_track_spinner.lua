local drawableSprite = require "structs.drawable_sprite"

local multipleNodeDustTrackSpinner = {}

multipleNodeDustTrackSpinner.name = "AdventureHelper/DustTrackSpinnerMultinode"
multipleNodeDustTrackSpinner.nodeLimits = {1, -1}
multipleNodeDustTrackSpinner.nodeLineRenderType = "line"

multipleNodeDustTrackSpinner.fieldInformation = {
    moveTime = {
        minimumValue = 0.0
    },
    pauseTile = {
        minimumValue = 0.0
    }
}

multipleNodeDustTrackSpinner.placements = {
    {
        name = "multiple_node_dust_track_spinner",
        data = {
            moveTime = 0.4,
            pauseTime = 0.2,
            pauseFlag = "",
            pauseOnCutscene = false
        }
    }
}

local baseTexture = "danger/dustcreature/base00"
local centerTexture = "danger/dustcreature/center00"

function multipleNodeDustTrackSpinner.sprite(room, entity)
    return {
        drawableSprite.fromTexture(baseTexture, entity),
        drawableSprite.fromTexture(centerTexture, entity),
    }
end

return multipleNodeDustTrackSpinner