import html
import os
import re

#工程策略目录
sourceDir = os.path.abspath('..') + "\\Routines\\DefaultRoutine\\Silverfish\\"
#输出目录
targetDir = os.path.abspath('.') + "\\output\\"
#最新的卡牌数据xml：https://github.com/HearthSim/hsdata/blob/master/CardDefs.xml
cardDefXmlFile = os.path.abspath('.') + "\\src\\CardDefs.xml"
TAG_CARDTYPE = {
    0: "INVALID",
    1: "GAME",
    2: "PLAYER",
    3: "英雄",
    4: "随从",
    5: "法术",
    6: "附魔",
    7: "武器",
    8: "ITEM",
    9: "TOKEN",
    10: "英雄技能",
    11: "BLANK",
    12: "GAME_MODE_BUTTON",
    22: "MOVE_MINION_HOVER_TARGET"
}

TAG_CLASS = {
    0: "INVALID",
    1: "巫妖王",
    2: "德鲁伊",
    3: "猎人",
    4: "法师",
    5: "圣骑士",
    6: "潜行者",
    7: "牧师",
    8: "萨满祭司",
    9: "术士",
    10: "战士",
    11: "梦境",
    12: "中立",
    13: "威兹班",
    14: "恶魔猎手"
}
TAG_CARD_SET = {
    0: "INVALID",
    1: "TEST_TEMPORARY",
    2: "0001CORE基本",
    3: "0002EX经典",
    4: "0003HOF荣誉室",
    5: "MISSIONS新手训练",
    6: "DEMO",
    7: "NONE",
    8: "CHEAT",
    9: "BLANK",
    10: "DEBUG_SP",
    11: "PROMO",
    12: "0012NAX纳克萨玛斯",
    13: "0013GVG地精大战侏儒",
    14: "0014BRM黑石山的火焰",
    15: "0015AT冠军的试炼",
    16: "CREDITS暴雪制作人员",
    17: "0017英雄皮肤和技能",
    18: "0018TB乱斗模式",
    19: "SLUSH",
    20: "0020LOE探险者协会",
    21: "0021OG上古之神的低语",
    22: "OG_RESERVE",
    23: "0023KAR卡拉赞之夜",
    24: "KARA_RESERVE",
    25: "0025CFM龙争虎斗加基森",
    26: "GANGS_RESERVE",
    27: "0027UNG勇闯安戈洛",
    1001: "1001ICC冰封王座的骑士",
    1004: "1004LOOT狗头人与地下世界",
    1125: "1125GIL女巫森林",
    1127: "1127BOT砰砰计划",
    1129: "1129TRL拉斯塔哈的大乱斗",
    1130: "1130DAL暗影崛起",
    1158: "1158ULD奥丹姆奇兵",
    1347: "1347DRG巨龙降临",
    1439: "1439WE狂野限时回归",
    1403: "1403YOD迦拉克隆的觉醒",
    1453: "BATTLEGROUNDS",
    1414: "1414BT外域的灰烬",
    1443: "1443SCH通灵学院",
    1463: "1463BT恶魔猎手新兵",
    1466: "1466DMF暗月马戏团",
}


def find_all_file(base):
    for root, ds, fs in os.walk(base):
        for f in fs:
            yield f

if not os.path.exists(targetDir):
    os.makedirs(targetDir)
cop = re.compile("[^a-zA-Z0-9]")  # 匹配不是中文、大小写、数字的其他字符
fb = open(targetDir + "CardDB_cardIDEnum.txt", 'w', encoding='utf-8')
fn = open(targetDir + "CardDB_cardName.txt", 'w', encoding='utf-8')
cardname = ""
cardnamecn = ""
cardtext = ""
cardtextcn = ""
cardID = ""
enumID = 0
numberID = 0
CARDTYPE = 0
CLASS = 0
CARD_SET = 0
COST = 0
ATK = 0
HEALTH = 0
DURABILITY = 0
existsimcards = set()
existcardnames = set()
existcardids = set()
tmp_cards = {}
for fname in find_all_file(sourceDir + "cards"):
    if fname.startswith("Sim_"):
        existsimcards.add(fname[4:-3])

with open(sourceDir + "ai\\CardDB_cardName.cs", 'r', encoding='utf-8') as f:
    while 1:
        line = f.readline()
        if not line:
            break
        if r"///" in line:
            continue
        if "," in line:
            existcardnames.add(line.strip()[0:-1])
existcardnames.add("continue")
existcardnames.add("protected")
existcardnames.add("1level")

with open(sourceDir + "ai\\CardDB_cardIDEnum.cs", 'r', encoding='utf-8') as f:
    while 1:
        line = f.readline()
        if not line:
            break
        if r"///" in line:
            continue
        if "," in line:
            existcardids.add(line.split("=")[0].strip())

new_cards_dir = targetDir + "new_cards\\"
if not os.path.exists(new_cards_dir):
    os.mkdir(new_cards_dir)

with open(cardDefXmlFile, 'r', encoding='utf-8') as f:
    while 1:
        line = f.readline()
        if not line:
            break
        if r"</Entity>" in line:
            # BATTLEGROUNDS TB
            if CARD_SET != 1453 and CARD_SET != 1143:
                other = ""
                if TAG_CARDTYPE[CARDTYPE] == "法术":
                    other += " " + TAG_CLASS[CLASS] + " 费用：" + str(COST)
                if TAG_CARDTYPE[CARDTYPE] == "随从":
                    other += " " + TAG_CLASS[CLASS] + " 费用：" + str(COST) + " 攻击力：" + str(ATK) + " 生命值：" + str(HEALTH)
                if TAG_CARDTYPE[CARDTYPE] == "武器":
                    other += " " + TAG_CLASS[CLASS] + " 费用：" + str(COST) + " 攻击力：" + str(ATK) + " 耐久度：" + str(
                        DURABILITY)
                if TAG_CARDTYPE[CARDTYPE] == "英雄技能":
                    other += " " + TAG_CLASS[CLASS] + " 费用："
                mycardname = cop.sub('', cardname).lower()  # 有问题没去重
                comment = "/// <summary>\n" \
                          + "/// <para>" + TAG_CARDTYPE[CARDTYPE] + other + "</para>\n" \
                          + "/// <para>" + cardID + "</para>\n" \
                          + "/// <para>" + cardname + "</para>\n" \
                          + "/// <para>" + mycardname + "</para>\n" \
                          + "/// <para>" + cardnamecn + "</para>\n"\
                          + "/// <para>" + cardtext + "</para>\n"\
                          + "/// <para>" + cardtextcn + "</para>\n"\
                          + "/// </summary>\n"
                if mycardname != "" and mycardname not in existcardnames:
                    tmp_cards[mycardname] = comment + mycardname + ",\n"
                if cardID not in existcardids:
                    fb.write(comment)
                    fb.write(cardID + " = " + str(numberID) + ",\n")
                if TAG_CARDTYPE[CARDTYPE] != "附魔" and cardID not in existsimcards:
                    directory = new_cards_dir + TAG_CARD_SET[CARD_SET]
                    if not os.path.exists(directory):
                        os.mkdir(directory)
                    with open(directory + "\\Sim_" + cardID + ".cs", 'w', encoding='utf-8') as sim:
                        sim.write("using System;\nusing System.Collections.Generic;\nusing System.Text;\n\n")
                        sim.write("namespace HREngine.Bots\n{\n")
                        sim.write(
                            "\tclass Sim_" + cardID + " : SimTemplate //* " + cardnamecn + " " + cardname + "\n\t{\n")
                        sim.write("\t\t//" + cardtext + "\n")
                        sim.write("\t\t//" + cardtextcn + "\n")
                        sim.write("\t\t\n\t\t\n\t}\n}\n")

                # print(cardname, cardnamecn, cardID, TAG_CARDTYPE[CARDTYPE], TAG_CLASS[CLASS], cardtext, cardtextcn)
            cardname = ""
            cardnamecn = ""
            cardtext = ""
            cardtextcn = ""
            cardID = ""
            enumID = 0
            numberID = 0
            CARDTYPE = 0
            CLASS = 0
            CARD_SET = 0
            COST = 0
            ATK = 0
            HEALTH = 0
            DURABILITY = 0

        if "<Entity CardID=\"" in line:
            index1 = line.find("<Entity CardID=\"")
            index2 = line.find("\"", index1 + 16)
            if index1 == -1 or index2 == -1:
                print(line)
                exit(0)
            cardID = line[index1 + 16: index2]
            index1 = line.find("ID=\"", index2)
            index2 = line.find("\"", index1 + 4)
            if index1 == -1 or index2 == -1:
                print(line)
                exit(0)
            numberID = line[index1 + 4: index2]

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
            # CLASS
            if enumID == 199:
                CLASS = value
            # CARD_SET
            if enumID == 183:
                CARD_SET = value
            # COST
            if enumID == 48:
                COST = value
            # HEALTH
            if enumID == 45:
                HEALTH = value
            # ATK
            if enumID == 47:
                ATK = value
            # DURABILITY
            if enumID == 187:
                DURABILITY = value
        if "<enUS>" in line:
            if enumID != 185 and enumID != 184:
                continue
            index1 = line.find("<enUS>")
            index2 = line.find("</enUS>", index1 + 6)
            text = ""
            while (index2 == -1):
                text += line[index1 + 6:-1]
                line = f.readline()
                index2 = line.find("</enUS>")
                index1 = -6
            text += line[index1 + 6: index2]

            if index1 == -1 or index2 == -1:
                print("错误：" + line)
                exit(0)
            if enumID == 185:
                cardname = html.unescape(text)
            if enumID == 184:
                cardtext = html.unescape(text)

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
            if enumID == 184:
                cardtextcn = html.unescape(text)
keys = tmp_cards.keys()
for key in sorted(keys):
    fn.write(tmp_cards[key])
