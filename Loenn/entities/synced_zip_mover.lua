local drawableSprite = require("structs.drawable_sprite")
local drawableLine = require("structs.drawable_line")
local drawableNinePatch = require("structs.drawable_nine_patch")
local drawableRectangle = require("structs.drawable_rectangle")
local utils = require("utils")

local syncedZipMover = {}

syncedZipMover.name = "AdventureHelper/LinkedZipMover"
syncedZipMover.depth = -9999
syncedZipMover.nodeVisibility = "never"
syncedZipMover.nodeLimits = {1, 1}
syncedZipMover.minimumSize = {16, 16}

syncedZipMover.fieldInformation = {
    colorCode = {
        fieldType = "color"
    },
    spritePath = {
        editable = true,
        options = { "", "objects/zipmover" }
    },
    speedMultiplier = {
        minimumValue = 0.0
    }
}

syncedZipMover.placements = {
    {
        name = "normal",
        data = {
            width = 16,
            height = 16,
            colorCode = "ff0000",
            spritePath = ""
        }
    },
    {
        name = "custom_speed",
        data = {
            width = 16,
            height = 16,
            colorCode = "ff0000",
            spritePath = "",
            speedMultiplier = 1.0
        }
    }
}

local blockNinePatchOptions = {
    mode = "border"
}

local defaultCogTexture = "objects/zipmover/cog"
local defaultLightsTexture = "objects/zipmover/light01"
local defaultBlockTexture = "objects/zipmover/block"

local function addNodeSprites(sprites, entity, cogTexture, centerX, centerY, centerNodeX, centerNodeY)
    local nodeCogSprite = drawableSprite.fromTexture(cogTexture, entity) or drawableSprite.fromTexture(defaultCogTexture, entity)

    nodeCogSprite:setPosition(centerNodeX, centerNodeY)
    nodeCogSprite:setJustification(0.5, 0.5)

    local ropeColor = entity.colorCode or "ff0000"

    local points = {centerX, centerY, centerNodeX, centerNodeY}
    local leftLine = drawableLine.fromPoints(points, ropeColor, 1)
    local rightLine = drawableLine.fromPoints(points, ropeColor, 1)

    leftLine:setOffset(0, 4.5)
    rightLine:setOffset(0, -4.5)

    leftLine.depth = 5000
    rightLine.depth = 5000

    for _, sprite in ipairs(leftLine:getDrawableSprite()) do
        table.insert(sprites, sprite)
    end

    for _, sprite in ipairs(rightLine:getDrawableSprite()) do
        table.insert(sprites, sprite)
    end

    table.insert(sprites, nodeCogSprite)
end

local function addBlockSprites(sprites, entity, blockTexture, lightsTexture, x, y, width, height)
    local rectangle = drawableRectangle.fromRectangle("fill", x + 2, y + 2, width - 4, height - 4, {0.0, 0.0, 0.0})

    local frameNinePatch = drawableNinePatch.fromTexture(blockTexture, blockNinePatchOptions, x, y, width, height)
        or drawableNinePatch.fromTexture(defaultBlockTexture, blockNinePatchOptions, x, y, width, height)
    local frameSprites = frameNinePatch:getDrawableSprite()

    local lightsSprite = drawableSprite.fromTexture(lightsTexture, entity) or drawableSprite.fromTexture(defaultLightsTexture, entity)

    lightsSprite:addPosition(math.floor(width / 2), 0)
    lightsSprite:setJustification(0.5, 0.0)

    table.insert(sprites, rectangle:getDrawableSprite())

    for _, sprite in ipairs(frameSprites) do
        table.insert(sprites, sprite)
    end

    table.insert(sprites, lightsSprite)
end

function syncedZipMover.sprite(room, entity)
    local sprites = {}

    local x, y = entity.x or 0, entity.y or 0
    local width, height = entity.width or 16, entity.height or 16
    local halfWidth, halfHeight = math.floor(entity.width / 2), math.floor(entity.height / 2)

    local nodes = entity.nodes or {{x = 0, y = 0}}
    local nodeX, nodeY = nodes[1].x, nodes[1].y

    local centerX, centerY = x + halfWidth, y + halfHeight
    local centerNodeX, centerNodeY = nodeX + halfWidth, nodeY + halfHeight

    local spritePath = entity.spritePath or ""
    local blockTexture, lightTexture, cogTexture = spritePath .. "/block", spritePath .. "/light01", spritePath .. "/cog"

    addNodeSprites(sprites, entity, cogTexture, centerX, centerY, centerNodeX, centerNodeY)
    addBlockSprites(sprites, entity, blockTexture, lightTexture, x, y, width, height)

    return sprites
end

function syncedZipMover.selection(room, entity)
    local x, y = entity.x or 0, entity.y or 0
    local width, height = entity.width or 8, entity.height or 8
    local halfWidth, halfHeight = math.floor(entity.width / 2), math.floor(entity.height / 2)

    local nodes = entity.nodes or {{x = 0, y = 0}}
    local nodeX, nodeY = nodes[1].x, nodes[1].y
    local centerNodeX, centerNodeY = nodeX + halfWidth, nodeY + halfHeight

    local spritePath = entity.spritePath or ""
    local cogTexture = spritePath .. "/cog"

    local cogSprite = drawableSprite.fromTexture(cogTexture, entity) or drawableSprite.fromTexture(defaultCogTexture, entity)
    local cogWidth, cogHeight = cogSprite.meta.width, cogSprite.meta.height

    local mainRectangle = utils.rectangle(x, y, width, height)
    local nodeRectangle = utils.rectangle(centerNodeX - math.floor(cogWidth / 2), centerNodeY - math.floor(cogHeight / 2), cogWidth, cogHeight)

    return mainRectangle, {nodeRectangle}
end

return syncedZipMover
