local multipleNodeBladeTrackSpinner = {}

multipleNodeBladeTrackSpinner.name = "AdventureHelper/StarTrackSpinnerMultinode"
multipleNodeBladeTrackSpinner.nodeLimits = {1, -1}
multipleNodeBladeTrackSpinner.nodeLineRenderType = "line"

multipleNodeBladeTrackSpinner.fieldInformation = {
    moveTime = {
        minimumValue = 0.0
    },
    pauseTile = {
        minimumValue = 0.0
    }
}

multipleNodeBladeTrackSpinner.placements = {
    {
        name = "multiple_node_star_track_spinner",
        data = {
            moveTime = 0.4,
            pauseTime = 0.2,
            pauseFlag = "",
            pauseOnCutscene = false
        }
    }
}

multipleNodeBladeTrackSpinner.texture = "danger/starfish14"

return multipleNodeBladeTrackSpinner