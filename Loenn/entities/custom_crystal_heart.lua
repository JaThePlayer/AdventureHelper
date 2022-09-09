local drawableSprite = require "structs.drawable_sprite"

local customCrystalHeart = {}

customCrystalHeart.name = "AdventureHelper/CustomCrystalHeart"

customCrystalHeart.fieldInformation = {
    color = {
        fieldType = "color"
    },
    path = {
        options = {
            ["Custom Heart (empty path)"] = "",
            ["Blue Heart (heartgem0)"] = "heartgem0",
            ["Red Heart (heartgem1)"] = "heartgem1",
            ["Gold Heart (heartgem2)"] = "heartgem2",
            ["White Heart (heartgem3)"] = "heartgem3"
        }
    }
}

customCrystalHeart.placements = {
    {
        name = "custom",
        data = {
            color = "00a81f",
            path = ""
        }
    },
    {
        name = "blue",
        data = {
            color = "00a81f",
            path = "heartgem0"
        }
    },
    {
        name = "red",
        data = {
            color = "00a81f",
            path = "heartgem1"
        }
    },
    {
        name = "gold",
        data = {
            color = "00a81f",
            path = "heartgem2"
        }
    },
    {
        name = "white",
        data = {
            color = "00a81f",
            path = "heartgem3"
        }
    }
}

local heartTextures = {
    ["heartgem0"] = "collectables/heartGem/0/00",
    ["heartgem1"] = "collectables/heartGem/1/00",
    ["heartgem2"] = "collectables/heartGem/2/00",
    ["heartgem3"] = "collectables/heartGem/3/00"
}

local recolorableHeartOutlineTexture = "collectables/AdventureHelper/RecolorHeart_Outline/00"
local recolorableHeartTexture = "collectables/AdventureHelper/RecolorHeart/00"

function customCrystalHeart.sprite(room, entity)
    local path = entity.path

    if path == "" then
        local outline = drawableSprite.fromTexture(recolorableHeartOutlineTexture, entity)
        local heart = drawableSprite.fromTexture(recolorableHeartTexture, entity)
        return {heart, outline}
    end

    local texture = heartTextures[path] or heartTextures["heartgem3"]
    local sprite = drawableSprite.fromTexture(texture, entity)
    sprite.color = {1.0, 1.0, 1.0, 1.0}
    return sprite
end

return customCrystalHeart
