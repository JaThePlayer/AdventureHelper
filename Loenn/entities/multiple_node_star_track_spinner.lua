local multipleNodeStarTrackSpinner = {}

multipleNodeStarTrackSpinner.name = "AdventureHelper/StarTrackSpinnerMultinode"
multipleNodeStarTrackSpinner.nodeLimits = {1, -1}
multipleNodeStarTrackSpinner.nodeLineRenderType = "line"

multipleNodeStarTrackSpinner.fieldInformation = {
    moveTime = {
        minimumValue = 0.0
    },
    pauseTime = {
        minimumValue = 0.0
    }
}

multipleNodeStarTrackSpinner.placements = {
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

multipleNodeStarTrackSpinner.texture = "danger/starfish14"

return multipleNodeStarTrackSpinner