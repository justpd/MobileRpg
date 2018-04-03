import re, os, sys, fileinput, argparse

fd_Network = "../NetWork/"
file_dataObject = fd_Network + "DataObjects.cs"
file_clienTCP = fd_Network + "ClientTCP.cs"
file_nwPackets = fd_Network + "NetWorkPackets.cs"
file_cliHndlNwData = fd_Network + "ClientHandleNetworkData.cs"

def uncapitalize(s):
    return s[:1].lower() + s[1:]

def addDataObject (className, _vars):

    with open(file_dataObject) as f:
        lines = f.readlines()

    with open(file_dataObject, 'a') as f:
        
        f.write("\n\npublic class {className}".format(className = className))
        f.write("\n{\n")

        for var in _vars:
            f.write("\tpublic {_var}\n".format(_var = var))

        f.write("}")

def newDataObject():
    temp = []
    className = input("Write class name: ")
    varCount = int(input("Write var count: "))
    for i in range(varCount):
        temp.append(input("Enter var: "))

    addDataObject(className, temp)


def newDataObject(className):
    temp = []
    varCount = int(input("Write var count: "))
    for i in range(varCount):
        temp.append(input("Enter var: "))

    addDataObject(className, temp)


def addSendDataStr(sendDataName):

    re_send = r"\}\s\/{2}(?:^|\#{3})senddata(?:$|\#{3})"
    addNetWorkPacket(sendDataName)
    
    with open(file_clienTCP) as f:
        lines = f.readlines()

    with open(file_clienTCP, 'a') as f:
        for line in lines:
            if(re.search(re_send, line)):
                f.write("\n\n\tpublic static void Send_{sendDataName}(string str)\n{\n.".format(sendDataName = sendDataName))
                f.write("\t\tPacketBuffer buffer = new PacketBuffer();\n")
                f.write("\t\tbuffer.WriteInt((int)ClientPackets.C_{packetIndex});\n".format(packetIndex = sendDataName))
                f.write("\t\tbuffer.WriteString(str);\n")
                f.write("\t\tSendData(buffer.ToArray());\n")
                f.write("\t\tbuffer.Dispose();\n\t}\n") 

    for line in fileinput.input(file_clienTCP, inplace = 1):
        sys.stdout.write(line.replace('} //###senddata###', ''))

    with open(file_clienTCP, 'a') as f:
        f.write("\n} //###senddata###")


def addSendDataObj(sendDataType):
    re_send = r"\}\s\/{2}(?:^|\#{3})senddata(?:$|\#{3})"
    newDataObject(sendDataType)
    addNetWorkPacket(sendDataType)
    
    with open(file_clienTCP) as f:
        lines = f.readlines()

    with open(file_clienTCP, 'a') as f:
        for line in lines:
            if(re.search(re_send, line)):
                f.write("\n\tpublic static void Send_{sendDataType}({sendDataType} {sendDataName})\n".format(sendDataType = sendDataType, sendDataName = uncapitalize(sendDataType)))
                f.write("\t{\n\t\tPacketBuffer buffer = new PacketBuffer();\n")
                f.write("\t\tbuffer.WriteInt((int)ClientPackets.C_{packetIndex});\n".format(packetIndex = sendDataType))
                f.write("\t\tstring json = JsonConvert.SerializeObject({sendDataName});\n".format(sendDataName = uncapitalize(sendDataType)))
                f.write("\t\tbuffer.WriteString(json);\n")
                f.write("\t\tSendData(buffer.ToArray());\n")
                f.write("\t\tbuffer.Dispose();\n\t}") 
        
    for line in fileinput.input(file_clienTCP, inplace = 1):
        sys.stdout.write(line.replace('} //###senddata###', ''))

    with open(file_clienTCP, 'a') as f:
        f.write("\n} //###senddata###")

def addNetWorkPacket (clientData):
    # re_lb = r"\/{2}(?:^|\#{3})lb(?:$|\#{3})" #} //###lb###
    re_send = r"(\d{6})(\W\s\/{2}(?:^|\#{3})nwpackets(?:$|\#{3}))"

    with open(file_nwPackets) as f:
        lines = f.readlines()

    for line in fileinput.input(file_nwPackets, inplace = 1):
        sys.stdout.write(line.replace('} //lb\n', ''))

    f = open(file_nwPackets, 'a')
    for line in lines:
        matches = re.findall(re_send, line)
        if(matches):
            f.close()
            clientPacketIndex = int(matches[0][0])
            for line in fileinput.input(file_nwPackets, inplace = 1):
                sys.stdout.write(line.replace(' //###nwpackets###', ''))
            with open(file_nwPackets, 'a') as f:
                f.write("\tC_{clientData} = {clientPacketIndex}, //###nwpackets###".format(clientData = clientData, clientPacketIndex = clientPacketIndex + 1))
                f.write("\n} //lb\n")

def addServerNetWorkPacket (serverData):
    re_send = r"(\d{6})(\W\s\/{2}(?:^|\#{3})snwpackets(?:$|\#{3}))"

    with open(file_nwPackets) as f:
        lines = f.readlines()

    f = open(file_nwPackets, 'r')
    for line in lines:
        matches = re.findall(re_send, line)
        if(matches):
            f.close()
            serverPacketIndex = int(matches[0][0])
            for line in fileinput.input(file_nwPackets, inplace = 1):
                sys.stdout.write(line.replace(' //###snwpackets###', '\n\tS_Send{serverData} = {serverPacketIndex}, //###snwpackets###'.format(serverData = serverData, serverPacketIndex = serverPacketIndex + 1)))
  


        
def addHandlerNetworkDataString(HandlerDataType, functionName):
    addServerNetWorkPacket(HandlerDataType)
    newDataObject(HandlerDataType)
    addNetWorkPacket(HandlerDataType)
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):
        sys.stdout.write(line.replace("//###addboolevent###", "private static bool Event{HandlerDataType} = false;\n\t//###addboolevent###".format(HandlerDataType = HandlerDataType)))
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):
        sys.stdout.write(line.replace("//###addeventupdate###", 'else if (Event{HandlerDataType})'.format(HandlerDataType = HandlerDataType) + '\n\t\t{\n\t\t\tClientTCP.UpdateController();\n\t\t\tClientTCP.ClientController.SendMessage('+'"{functionName}");\n\t\t\tEvent{HandlerDataType} = false;'.format(functionName = functionName,  HandlerDataType = HandlerDataType)+ '\n\t\t}\n\t\t//###addeventupdate###')) 
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):
        sys.stdout.write(line.replace("//###inithandler###", "{\n\t\t\t(int) ServerPackets.S_Send"+"{HandlerDataType},\n\t\t\tHandle_{HandlerDataType}".format(functionName = functionName,  HandlerDataType = HandlerDataType)+"\n\t\t\t},\n\t\t\t//###inithandler###"))
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):        
        sys.stdout.write(line.replace("//###addhandler###","public static void Handle_{HandlerDataType}(byte[] data)".format(HandlerDataType = HandlerDataType)+"\n\t{\n\t\tPacketBuffer buffer = new PacketBuffer();\n\t\tbuffer.WriteBytes(data);\n\t\tint packetNum = buffer.ReadInteger();\n\t\tstring msg = buffer.ReadString();\n\t\tbuffer.Dispose();\n\n\t\t"+"Event{HandlerDataType} = true;".format(HandlerDataType = HandlerDataType)+"\n\t}\n\t//###addhandler###"))
    
              
def addHandlerNetworkDataObject(HandlerDataType, functionName):
    addServerNetWorkPacket(HandlerDataType)
    newDataObject(HandlerDataType)
    addNetWorkPacket(HandlerDataType)
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):
        sys.stdout.write(line.replace("//###addboolevent###", "private static bool Event{HandlerDataType} = false;\n\t//###addboolevent###".format(HandlerDataType = HandlerDataType)))        
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):  
        sys.stdout.write(line.replace("//###addobjectevent###", "private static {HandlerDataType} {HandlerDataName};\n\t//###addobjectevent###".format(HandlerDataType = HandlerDataType, HandlerDataName = uncapitalize(HandlerDataType))))          
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):            
        sys.stdout.write(line.replace("//###addeventupdate###", 'else if (Event{HandlerDataType})'.format(HandlerDataType = HandlerDataType) + '\n\t\t{\n\t\t\tClientTCP.UpdateController();\n\t\t\tClientTCP.ClientController.SendMessage('+'"{functionName}",{HandlerDataName});\n\t\t\tEvent{HandlerDataType} = false;'.format(functionName = functionName, HandlerDataName = uncapitalize(HandlerDataType),  HandlerDataType = HandlerDataType)+ '\n\t\t}\n\t\t//###addeventupdate###')) 
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):
        sys.stdout.write(line.replace("//###inithandler###", "{\n\t\t\t(int) ServerPackets.S_Send"+"{HandlerDataType},\n\t\t\tHandle_{HandlerDataType}".format(functionName = functionName,  HandlerDataType = HandlerDataType)+"\n\t\t\t},\n\t\t\t//###inithandler###"))
    for line in fileinput.input(file_cliHndlNwData, inplace = 1):
        sys.stdout.write(line.replace("//###addhandler###","public static void Handle_{HandlerDataType}(byte[] data)".format(HandlerDataType = HandlerDataType)+"\n\t{\n\t\tPacketBuffer buffer = new PacketBuffer();\n\t\tbuffer.WriteBytes(data);\n\t\tint packetNum = buffer.ReadInteger();\n\t\tstring msg = buffer.ReadString();\n\t\tbuffer.Dispose();\n\n\t\t"+"{HandlerDataName} = JsonConvert.DeserializeObject<{HandlerDataType}>(msg);\n\t\tEvent{HandlerDataType} = true;".format(HandlerDataName =uncapitalize(HandlerDataType), HandlerDataType = HandlerDataType)+"\n\t}\n\t//###addhandler###"))
    
                    
            

def cli():
    parser = argparse.ArgumentParser(description='CLI Api for Mobile RPG project.', epilog='send_data also create new network package and data object')   
    parser.add_argument('-sdo','--senddataobject', metavar='object name', help='add new send_dataobject in ClientTCP.cs')
    parser.add_argument('-sds','--senddatastring', metavar='string name', help='add new send_datastring in ClientTCP.cs')
    parser.add_argument('-npw','--networkpackage', metavar='c_name', help='add new network package in NetWorkPackets.cs')
    parser.add_argument('-do','--dataobject', metavar='object name', help='add new data object in DataObject.cs')    
    parser.add_argument('-hnds','--handlernetworkdatastring', nargs=2, metavar='string name', help='add new handler network data in ClientHandleNetworkData.cs')    
    parser.add_argument('-hndo','--handlernetworkdataobj', nargs=2, metavar='string name', help='add new handler network data in ClientHandleNetworkData.cs')    
    args = parser.parse_args()  
    if (args.senddataobject):
        addSendDataObj(args.senddataobject)
    elif (args.senddatastring):
        addSendDataStr(args.senddatastring)
    elif(args.networkpackage):
        addNetWorkPacket(args.networkpackage)
    elif(args.dataobject):
        newDataObject(args.dataobject)
    elif(args.handlernetworkdatastring):
        addHandlerNetworkDataString(args.handlernetworkdatastring[0],args.handlernetworkdatastring[1])
    elif(args.handlernetworkdataobj):
        addHandlerNetworkDataObject(args.handlernetworkdataobj[0],args.handlernetworkdataobj[1])
        
    print("done")


if __name__ == '__main__':
    cli()
    







              
            

    