# EntityEngine
A game engine I am currently developing utilizing an Entity Framework and SharpDX

### End-goal:
  Develop map format, and editor for said map. Game defaults to load mainmenu.map. Mainmenu.map has code that allows loading of other maps, and is generally the 'jumpstart' of the program. Think of this like an operating system for games.

### How do I get there? [ie implementation]:
  Custom Entity Framework;  
  SharpDX - DirectX10  
  IronPython  
  C#  
  Python  
  [Eventually a physics engine]

### TO-DO:
  Physics engine/System  
  AI engine/System  
  Position System needs move/rotate/scale commands  
  Camera class needs same commands  
  Input Map System needs implemented  
  Script System needs implemented  
  Map layout/generation/loading System needs implemented  
  Player System needs implemented  
  Generic shaders need written  
  Map editor needs made

### Implemented:
  Render System loads and works as intended  
  Win System loads and works as intended  
  Position System implemented, needs work  
  Main game form can be launched as host window, or be ran in target element on window using Embedded form  
  Extra debug form made for Python shell that appears in debug mode with access to base objects  
  Script engine using IronPython works as expected, just need a component system implemented  
