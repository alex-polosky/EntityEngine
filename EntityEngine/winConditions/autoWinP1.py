name = "Auto Win P1"
description = \
"""\
This is just an example of how the win conditions system will work.
It will force the first com to always win after approx 60 seconds has passed
"""

def main(world, fps, coms):
    ###################################################################
    ## Default
    import EntityFramework.Components
    import EntityEngine.Components

    from System.Collections import Generic
    List = Generic.List
    ###################################################################

    toret = type(coms)()

    # Logic to add coms here
    if fps.ElaspedMS > (1 * 5 * 1000):
        #toret.Add(coms[0])
        toret.Add(
            world
            .GetComponentSystem[EntityFramework.Components.TagComponent, EntityFramework.Components.TagSystem]()
            .getTaggedEntity("win")
            .GetComponent[EntityEngine.Components.WinComponent]()
        )
    # End logic

    if toret.Count == 0:
        return None
    else:
        return toret
