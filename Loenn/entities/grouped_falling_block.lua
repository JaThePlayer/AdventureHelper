local fakeTilesHelper = require("helpers.fake_tiles")

local groupedFallingBlock = {}

groupedFallingBlock.name = "AdventureHelper/GroupedFallingBlock"

groupedFallingBlock.placements = {
    name = "grouped_falling_block",
    data = {
        width = 8,
        height = 8,
        tiletype = "3",
        climbFall = true
    }
}

groupedFallingBlock.sprite = fakeTilesHelper.getEntitySpriteFunction("tiletype", false)
groupedFallingBlock.fieldInformation = fakeTilesHelper.getFieldInformation("tiletype")

return groupedFallingBlock
