namespace HREngine.Bots
{
    using System.Collections.Generic;

    public class Handmanager
    {

        public class Handcard
        {
            /// <summary>
            /// 手牌的位置
            /// </summary>
            public int position = 0;
            /// <summary>
            /// 实体ID
            /// </summary>
            public int entity = -1;
            /// <summary>
            /// 法力值消耗
            /// </summary>
            public int manacost = 1000;
            /// <summary>
            /// 额外攻击力
            /// <para>如污手党buff后的手牌</para>
            /// </summary>
            public int addattack = 0;
            /// <summary>
            /// 额外生命值/耐久度
            /// <para>如污手党buff后的手牌</para>
            /// </summary>
            public int addHp = 0;
            /// <summary>
            /// 卡牌
            /// </summary>
            public CardDB.Card card;
            /// <summary>
            /// 目标
            /// </summary>
            public Minion target;
            /// <summary>
            /// 上回合使用过元素的高亮
            /// </summary>
            public int elemPoweredUp = 0;
            /// <summary>
            /// 扩展参数2
            /// </summary>
            public int extraParam2 = 0;
            /// <summary>
            /// 扩展参数3
            /// </summary>
            public bool extraParam3 = false;

            public Handcard()
            {
                card = CardDB.Instance.unknownCard;
            }
            public Handcard(Handcard hc)
            {
                this.position = hc.position;
                this.entity = hc.entity;
                this.manacost = hc.manacost;
                this.card = hc.card;
                this.addattack = hc.addattack;
                this.addHp = hc.addHp;
                this.elemPoweredUp = hc.elemPoweredUp;
            }
            public Handcard(CardDB.Card c)
            {
                this.position = 0;
                this.entity = -1;
                this.card = c;
                this.addattack = 0;
                this.addHp = 0;
            }
            public void setHCtoHC(Handcard hc)
            {
                this.manacost = hc.manacost;
                this.addattack = hc.addattack;
                this.addHp = hc.addHp;
                this.card = hc.card;
                this.elemPoweredUp = hc.elemPoweredUp;
            }

            public int getManaCost(Playfield p)
            {
                return this.card.getManaCost(p, this.manacost);
            }
            public bool canplayCard(Playfield p, bool own)
            {
                return this.card.canplayCard(p, this.manacost, own);
            }
        }

        public List<Handcard> handCards = new List<Handcard>();

        public int anzcards = 0;

        public int enemyAnzCards = 0;

        private int ownPlayerController = 0;

        Helpfunctions help;
        CardDB cdb = CardDB.Instance;

        private static Handmanager instance;

        public static Handmanager Instance
        {
            get
            {
                return instance ?? (instance = new Handmanager());
            }
        }


        private Handmanager()
        {
            this.help = Helpfunctions.Instance;

        }

        public void clearAllRecalc()
        {
            this.handCards.Clear();
            this.anzcards = 0;
            this.enemyAnzCards = 0;
            this.ownPlayerController = 0;
        }

        public void setOwnPlayer(int player)
        {
            this.ownPlayerController = player;
        }




        public void setHandcards(List<Handcard> hc, int anzown, int anzenemy)
        {
            this.handCards.Clear();
            foreach (Handcard h in hc)
            {
                this.handCards.Add(new Handcard(h));
            }
            //this.handCards.AddRange(hc);
            this.handCards.Sort((a, b) => a.position.CompareTo(b.position));
            this.anzcards = anzown;
            this.enemyAnzCards = anzenemy;
        }
        
        public void printcards()
        {
            help.logg("Own Handcards: ");
            foreach (Handmanager.Handcard hc in this.handCards)
            {
                help.logg("pos " + hc.position + " " + hc.card.name + " " + hc.manacost + " entity " + hc.entity + " " + hc.card.cardIDenum + " " + hc.addattack + " " + hc.addHp + " " + hc.elemPoweredUp);
            }
            help.logg("Enemy cards: " + this.enemyAnzCards);
        }


    }

}