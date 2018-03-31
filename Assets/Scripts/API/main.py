import re
import os
import sys
import fileinput

fd_Network = "../NetWork/"
file_dataObject = fd_Network + "DataObjects.cs"
file_clienTCP = fd_Network + "ClientTCP.cs"
file_nwPackets = fd_Network + "NetWorkPackets.cs"

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


if __name__ == '__main__':
    
    addSendDataObj("EbalTvoiRot3")
    







              
            

    