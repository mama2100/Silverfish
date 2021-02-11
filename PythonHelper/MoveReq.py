import html
import os

#原来的卡牌数据xml
old_xml = os.path.abspath('..') + "\\Routines\\DefaultRoutine\\Silverfish\\data\\CardDefs.xml"
#新的卡牌数据xml
new_xml = os.path.abspath('.') + "\\src\\CardDefs.xml"
#输出目录
out_dir = os.path.abspath('.') + "\\output\\"
#合并后的卡牌数据xml
out_xml = out_dir + "CardDefs.xml"

def set2str(lines: set):
    out_str = ""
    for data_line in lines:
        out_str += "\t\t" + data_line.strip() + "\n"
    return out_str


playreq = {}
playname = {}
with open(old_xml, 'r', encoding='utf-8') as f:
    for line in f:
        if r"</Entity>" in line:
            if cardID in playreq:
                playname[cardnamecn] = playreq[cardID]
            cardID = ""
            enumID = 0
            cardnamecn = ""
        if "<Entity CardID=\"" in line:
            index1 = line.find("<Entity CardID=\"")
            index2 = line.find("\"", index1 + 16)
            if index1 == -1 or index2 == -1:
                print(line)
                exit(0)
            cardID = line[index1 + 16: index2]

        if "<PlayRequirement" in line:
            if cardID not in playreq:
                playreq[cardID] = set()
            playreq[cardID].add(line)

        if "<Tag enumID=\"" in line:
            enumIDl = line.find("<Tag enumID=\"")
            enumIDr = line.find("\"", enumIDl + 13)
            if enumIDl == -1 or enumIDr == -1:
                print(line)
                exit(0)
            enumID = int(line[enumIDl + 13: enumIDr])
        if "<zhCN>" in line:
            if enumID != 185 and enumID != 184:
                continue
            index1 = line.find("<zhCN>")
            index2 = line.find("</zhCN>", index1 + 6)
            text = ""
            while (index2 == -1):
                text += line[index1 + 6:-1]
                line = f.readline()
                index2 = line.find("</zhCN>")
                index1 = -6
            text += line[index1 + 6: index2]
            if index1 == -1 or index2 == -1:
                print("错误：" + line)
                exit(0)
            if enumID == 185:
                cardnamecn = html.unescape(text)

if not os.path.exists(out_dir):
    os.makedirs(out_dir)
with open(out_xml, 'w', encoding='utf-8') as fc:
    with open(new_xml, 'r', encoding='utf-8') as f:
        for line in f:
            if r"</Entity>" in line:
                if cardID in playreq:
                    fc.write(set2str(playreq[cardID]))
                else:
                    if cardnamecn in playname and CARDTYPE != 6 and CARDTYPE != 3:
                        print(cardID, cardnamecn)
                        print(playname[cardnamecn])
                        fc.write(set2str(playname[cardnamecn]))
                cardID = ""
            if "<Entity CardID=\"" in line:
                index1 = line.find("<Entity CardID=\"")
                index2 = line.find("\"", index1 + 16)
                if index1 == -1 or index2 == -1:
                    print(line)
                    exit(0)
                cardID = line[index1 + 16: index2]
            fc.write(line)
            if "<Tag enumID=\"" in line:
                enumIDl = line.find("<Tag enumID=\"")
                enumIDr = line.find("\"", enumIDl + 13)
                if enumIDl == -1 or enumIDr == -1:
                    print(line)
                    exit(0)
                enumID = int(line[enumIDl + 13: enumIDr])
                valuel = line.find("value=\"")
                valuer = line.find("\"", valuel + 7)
                if valuel == -1 or valuer == -1:
                    continue
                value = int(line[valuel + 7: valuer])
                # CARDTYPE
                if enumID == 202:
                    CARDTYPE = value
            if "<zhCN>" in line:
                if enumID != 185 and enumID != 184:
                    continue
                index1 = line.find("<zhCN>")
                index2 = line.find("</zhCN>", index1 + 6)
                text = ""
                while (index2 == -1):
                    text += line[index1 + 6:-1]
                    line = f.readline()
                    fc.write(line)
                    index2 = line.find("</zhCN>")
                    index1 = -6
                text += line[index1 + 6: index2]
                if index1 == -1 or index2 == -1:
                    print("错误：" + line)
                    exit(0)
                if enumID == 185:
                    cardnamecn = html.unescape(text)
