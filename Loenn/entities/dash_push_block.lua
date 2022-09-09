local drawableNinePatch = require "structs.drawable_nine_patch"

local dashPushBlock = {}

dashPushBlock.name = "AdventureHelper/DashPushBlock"
dashPushBlock.minimumSize = {16, 16}

dashPushBlock.placements = {
    {
        name = "dash_push_block",
        data = {
            width = 16,
            height = 16
        }
    }
}

local bodyTexture = "objects/AdventureHelper/dashpushblock/Body"
local ninePatchOptions = {} -- lönn nine-patch default values

function dashPushBlock.sprite(room, entity)
    local x, y = entity.x or 0, entity.y or 0
    local width, height = entity.width or 16, entity.height or 16

    local sprites = drawableNinePatch.fromTexture(bodyTexture, ninePatchOptions, x, y, width, height)
    return sprites
end

return dashPushBlock
