name = "Auto Win P1"
description = \
"""\
This is just an example of how the win conditions system will work.
It will force the first com to always win after approx 5 seconds has passed
"""

def main(world, fps, coms):
    toret = type(coms)()

    # Logic to add coms here
    if fps.ElaspedMS > (1 * 5 * 1000):
        toret.Add(coms[0])
    # End logic

    if toret.Count == 0:
        return None
    else:
        return toret
