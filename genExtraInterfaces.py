import os

os.chdir(r'P:\Code\Git\EntityEngine\Src\Core\EntityFramework\ComponentInterfaces')

targets = ["Children", "Group", "Tag", "WinCondition"]

srcCom = os.path.join(os.getcwd(), "IInput.cs")
srcSys = os.path.join(os.getcwd(), "IInputSystem.cs")

with open(srcCom) as f:
    dataCom = f.read()

with open(srcSys) as f:
    dataSys = f.read()
    
for target in targets:
    dataC = dataCom.replace("Input", target)
    dataS = dataSys.replace("Input", target)
    with open(os.path.join(os.getcwd(), "I" + target + ".cs"), 'w') as f:
        f.write(dataC)
    with open(os.path.join(os.getcwd(), "I" + target + "System.cs"), 'w') as f:
        f.write(dataS)
