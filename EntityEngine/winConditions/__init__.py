#import importlib as _il
import os as _os
import sys as _sys

print "Loading Win Conditions" 

print "Current working directory: " + str(_os.getcwd())
print
print "Files/Folders in current working directory: "
for x in _os.listdir(_os.getcwd()): print x
print
print "Folders in path: "
for x in _sys.path: print x
print

try:
    import clr
    clr.AddReference('EntityFramework')
    clr.AddReference('EntityEngine')
    clr.AddReference('PyInterface')
    for r in clr.References: print r
    print
except BaseException as e:
    print e.__class__
    print e.message

class _mod():
    def __init__(self, name, desc, main):
        self.name = name
        self.description = desc
        self.main = main

def _getFileContents(f):
    with open(f) as _f:
        pyCode = _f.read()
    globs = {}
    locs = {}
    exec pyCode in globs, locs
    return _mod(locs['name'], locs['description'], locs['main'])

def _getWin():
    old_os = _os.getcwd()
    new_os = '\\'.join(__file__.split('\\')[:-1])
    _os.chdir(new_os)
    
    WinConditions = []
    for x in _os.listdir('\\'.join(__file__.split('\\')[:-1])):
        if x == '__init__.py': continue
        if x[-3:] != '.py': continue
        try:
            module = _getFileContents(x)
            WinConditions.append(module)
        except:
            _sys.stderr.write("ERROR: cannot import python module: " + x + "\n")
            
    _os.chdir(old_os)
    return WinConditions

WinConditions = _getWin()

del _mod
del _getFileContents
del _getWin
del _os
del _sys
