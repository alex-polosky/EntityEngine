import xml.etree.ElementTree as ET
from xml.etree.ElementTree import XMLParser

class Menu():
    Id = ""
    Variables = []
    Entries = []
    
    def __init__(self, id="", variables=[], entries=[]):
        self.Id = id
        self.Variables = variables
        self.Entries = entries

class Variable():
    Languages = {0: "cs", 1:"py"}
    
    Language = -1
    Name = ""
    Value = None

    def __init__(self, language=-1, name="", value=None):
        self.language = language
        self.name = name
        self.value = value

class Entry():
    Id = ""
    Text = ""
    Action = None
    Picture = None
    Description = None

    def __init__(self, id="", text="", action=None, picture=None, description=None):
        self.Id = id
        self.Text = text
        self.Action = action
        self.Picture = picture
        self.Description = description

class Action():
    Menu = None
    Language = None
    PassIn = None
    Code = None

    def __init__(self, menu=None, language=None, passIn=None, code=None):
        self.Menu = menu
        self.Language = language
        self.PassIn = passIn
        self.Code = code

class MenuParser():

    tagStack = []
    attrStack = []

    menuStack = []
    curMenu = None

    indent = 0
    
    def start(self, tag, attr):
        tag = tag.lower()
        print ("    " * self.indent),
        self.indent += 1
        
        self.tagStack.append(tag)
        self.attrStack.append(attr)

        if tag == "menu":
            self.menuStack.append(Menu())
            curMenu = self.menuStack[-1]
        if tag == "variables":
            pass
        if tag == "variable":
            pass#self.curMenu.Variables.append(Variable())
        if tag == "entry":
            pass#self.curMenu.Entries.append(Entry())
        if tag == "action":
            pass#self.curMenu.Entries[-1].Action = Action()
            
    def end(self, tag):
        tag = tag.lower()
        self.indent -= 1
        print
        
        if len(self.tagStack) != 0:
            self.tagStack.pop(-1)
        if len(self.attrStack) != 0:
            self.attrStack.pop(-1)
        
        if tag == "menu":
            if len(self.menuStack) != 0:
                self.menuStack.pop(-1)
        if tag == "variables":
            pass
        if tag == "variable":
            pass
        if tag == "entry":
            pass
        if tag == "action":
            pass
        
    def data(self, data):
        tag = self.tagStack[-1]
        attr = self.attrStack[-1]
        print tag,
        
        if tag == "text":
            pass#self.curMenu.Entries[-1].Text = data
        if tag == "picture":
            pass#self.curMenu.Entries[-1].Picture = data
        if tag == "description":
            pass#self.curMenu.Entries[-1].Description = data
    
    def close(self):
        return self.curMenu

    def __init__(self):
        self.tagStack = []
        self.attrStack = []
        self.menuStack = []
        self.curMenu = None

class PrettyParser():

    indent = 0
    toret = ""
    
    def start(self, tag, attr):
        tag = tag.lower()

        self.toret += ("    " * self.indent) + tag + "\n"
        self.indent += 1
            
    def end(self, tag):
        tag = tag.lower()
        self.indent -= 1
        
    def data(self, data):
        pass
    
    def close(self):
        return self.toret

    def __init__(self): 
        self.toret = ""
        self.indent = 0

def main(sXml):
    target = MenuParser()
    parser = XMLParser(target=target)
    parser.feed(sXml)
    return parser.close()


_testXml = r'''<?xml version="1.0" encoding="utf-8"?>
<Menu id="menu_main">
  <variables>
    <variable language="cs" name="csColl" value="?" />
    <variable language="cs" name="sys" value="?" />
  </variables>
  <entry id="newGame">
    <text>New Game</text>
    <action language="cs" passin="sys">sys.LoadDefaultMap;sys.LoadDefaultScenario;</action>
  </entry>
  <entry id="loadGame">
    <text>Load Game</text>
    <action>
      <Menu id="menu_loadGame">
        <entry id="loadGame_0">
          <text>Game testing</text>
          <picture>SaveGames/game_testing0</picture>
          <description>No real description found</description>
          <!-- these would normally be guids, but for brevity I'm using simple integers -->
          <action language="cs" passin="sys">sys.LoadMapToCurrent(0); sys.LoadScenario(0);</action>
        </entry>
        <entry id="loadGame_1">
          <text>More Game Testing</text>
          <picture>SaveGames/game_testing1</picture>
          <description>Also no description.</description>
          <!-- these would normally be guids, but for brevity I'm using simple integers -->
          <action language="cs" passin="sys">sys.LoadMapToCurrent(1); sys.LoadScenario(0);</action>
        </entry>
        <entry id="loadGame_2">
          <text>Yet another game test</text>
          <picture>SaveGames/game_testing2</picture>
          <description>Fuck any of this fucking shit</description>
          <!-- these would normally be guids, but for brevity I'm using simple integers -->
          <action language="cs" passin="sys">sys.LoadMapToCurrent(2); sys.LoadScenario(0);</action>
        </entry>
      </Menu>
    </action>
  </entry>
  <entry id="settings">
    <text>Settings</text>
    <action>
      <Menu id="menu_settings">
        <entry id="audio">
          <text>Audio</text>
          <action>
            <Menu id="menu_audio">
              <entry id="test0">
                <text>Test0</text>
                <action></action>
              </entry>
              <entry id="test1">
                <text>Test1</text>
                <action></action>
              </entry>
              <entry id="test2">
                <text>Test2</text>
                <action></action>
              </entry>
            </Menu>
          </action>
        </entry>
        <entry id="video">
          <text>Video</text>
        </entry>
        <entry id="controls">
          <text>Controls</text>
        </entry>
      </Menu>
    </action>
  </entry>
  <entry id="exit">
    <text>Exit</text>
    <action language="cs" passin="csColl">csColl.Exit</action>
  </entry>
</Menu>'''

if __name__ == '__main__':
    a = main(_testXml)
