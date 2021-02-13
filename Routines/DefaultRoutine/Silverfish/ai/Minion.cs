using Triton.Game.Mapping;

namespace HREngine.Bots
{
    using System;
    using System.Collections.Generic;

    public class miniEnch
    {
        public CardDB.cardIDEnum CARDID = CardDB.cardIDEnum.None;
        public int creator = 0; // the minion
        public int controllerOfCreator = 0; // own or enemys buff?
        public int copyDeathrattle = 0;

        public miniEnch(CardDB.cardIDEnum id, int crtr, int controler, int copydr)
        {
            this.CARDID = id;
            this.creator = crtr;
            this.controllerOfCreator = controler;
            this.copyDeathrattle = copydr;
        }

    }

    public class Minion
    {
        //dont silence----------------------------
        /// <summary>
        /// 受到伤害次数
        /// </summary>
        public int anzGotDmg = 0;
        /// <summary>
        /// 受到伤害总和
        /// </summary>
        public int GotDmgValue = 0;
        /// <summary>
        /// 受到治疗次数
        /// </summary>
        public int anzGotHealed = 0;
        /// <summary>
        /// 受到治疗总和
        /// </summary>
        public int GotHealedValue = 0;
        /// <summary>
        /// 是否进行过激励
        /// </summary>
        public bool gotInspire = false;
        /// <summary>
        /// 是否为英雄
        /// <para>英雄和随从在HearthBuddy中都是用Minion来表示</para>
        /// </summary>
        public bool isHero = false;
        /// <summary>
        /// 是否为己方
        /// </summary>
        public bool own;
        public int pID = 0;

        /// <summary>
        /// 名称
        /// </summary>
        public CardDB.cardName name = CardDB.cardName.unknown;
        /// <summary>
        /// 职业
        /// <para>只用于英雄</para>
        /// </summary>
        public TAG_CLASS cardClass = TAG_CLASS.INVALID;
        /// <summary>
        /// 职业契合度
        /// <para>各种族（鱼人恶魔等）与职业相关性</para>
        /// </summary>
        public int synergy = 0;
        /// <summary>
        /// 手牌信息
        /// <para>如果Minion中储存的内容不够的话，可以调用这个获取更多的信息</para>
        /// <para>例如<c>m.handcard.card</c></para>
        /// </summary>
        public Handmanager.Handcard handcard;
        /// <summary>
        /// 实体ID
        /// </summary>
        public int entitiyID = -1;

        /// <summary>
        /// 在场上的位置
        /// </summary>
        public int zonepos = 0;
        public CardDB.Card deathrattle2;

        /// <summary>
        /// 是否在本回合使用
        /// </summary>
        public bool playedThisTurn = false;
        /// <summary>
        /// 是否在上回合使用
        /// </summary>
        public bool playedPrevTurn = false;
        /// <summary>
        /// 本回合攻击次数
        /// </summary>
        public int numAttacksThisTurn = 0;
        /// <summary>
        /// 是否攻击时免疫
        /// <para>暂时只对角斗士的长弓存在时有用。TODO为金牌猎手克里而添加随从的部分</para>
        /// </summary>
        public bool immuneWhileAttacking = false;

        public bool allreadyAttacked = false;

        /// <summary>
        /// 暗影狂乱
        /// <para>直到回合结束，获得一个攻击力小于或等于3的敌方随从的控制权</para>
        /// </summary>
        public bool shadowmadnessed = false;//´can be silenced :D

        /// <summary>
        /// 我方回合开始被消灭
        /// </summary>
        public bool destroyOnOwnTurnStart = false; // depends on own!
        /// <summary>
        /// 敌方回合开始被消灭
        /// </summary>
        public bool destroyOnEnemyTurnStart = false; // depends on own!
        /// <summary>
        /// 我方回合结束被消灭
        /// </summary>
        public bool destroyOnOwnTurnEnd = false; // depends on own!
        /// <summary>
        /// 敌方回合结束被消灭
        /// </summary>
        public bool destroyOnEnemyTurnEnd = false; // depends on own!
        /// <summary>
        /// 回合开始时变更所有权
        /// <para>例如：暗影狂乱结束后的归还</para>
        /// </summary>
        public bool changeOwnerOnTurnStart = false;

        /// <summary>
        /// 拥有潜行，直到你的下个回合
        /// </summary>
        public bool conceal = false;
        /// <summary>
        /// 先祖之魂
        /// <para>亡语：再次召唤该随从。</para>
        /// </summary>
        public int ancestralspirit = 0;
        /// <summary>
        /// 救赎
        /// <para>亡语：回到战场，并具有1点生命值。</para>
        /// </summary>
        public int desperatestand = 0;
        /// <summary>
        /// 丛林之魂
        /// <para>亡语：召唤一个2/2的树人。</para>
        /// </summary>
        public int souloftheforest = 0;
        /// <summary>
        /// 骑乘剑龙
        /// <para>(+2/+6并具有嘲讽。)亡语：召唤一个剑龙。</para>
        /// </summary>
        public int stegodon = 0;
        /// <summary>
        /// 活性孢子
        /// <para>亡语：召唤两个1/1的植物。</para>
        /// </summary>
        public int livingspores = 0;
        /// <summary>
        /// 探险帽
        /// <para>(+1/+1。)亡语：将一张“探险帽”置入你的手牌。</para>
        /// </summary>
        public int explorershat = 0;
        /// <summary>
        /// 回响之魂
        /// <para>亡语：将该随从移回你的手牌。</para>
        /// </summary>
        public int returnToHand = 0;
        /// <summary>
        /// 蛛魔孢子
        /// <para>亡语：随机将一张野兽牌置入你的手牌。</para>
        /// </summary>
        public int infest = 0;

        /// <summary>
        /// 我方智慧祝福
        /// <para>选择一个随从，每当其进行攻击，便抽一张牌。</para>
        /// </summary>
        public int ownBlessingOfWisdom = 0;
        /// <summary>
        /// 敌方智慧祝福
        /// <para>选择一个随从，每当其进行攻击，便抽一张牌。</para>
        /// </summary>
        public int enemyBlessingOfWisdom = 0;
        /// <summary>
        /// 我方真言术：耀
        /// <para>选择一个随从。每当其进行攻击，为你的英雄恢复4点生命值。</para>
        /// </summary>
        public int ownPowerWordGlory = 0;
        /// <summary>
        /// 敌方真言术：耀
        /// <para>选择一个随从。每当其进行攻击，为你的英雄恢复4点生命值。</para>
        /// </summary>
        public int enemyPowerWordGlory = 0;
        /// <summary>
        /// 法术强度
        /// </summary>
        public int spellpower = 0;

        /// <summary>
        /// 无法成为法术或英雄技能的目标
        /// </summary>
        public bool cantBeTargetedBySpellsOrHeroPowers = false;
        /// <summary>
        /// 无法攻击英雄
        /// </summary>
        public bool cantAttackHeroes = false;
        /// <summary>
        /// 无法攻击
        /// </summary>
        public bool cantAttack = false;

        /// <summary>
        /// 生命值
        /// </summary>
        public int Hp = 0;
        /// <summary>
        /// 最大生命值
        /// </summary>
        public int maxHp = 0;
        /// <summary>
        /// 护甲值
        /// <para>只用于英雄</para>
        /// </summary>
        public int armor = 0;

        /// <summary>
        /// 攻击力
        /// </summary>
        public int Angr = 0;
        public int AdjacentAngr = 0;
        /// <summary>
        /// 临时攻击力
        /// <para>本回合额外攻击力</para>
        /// </summary>
        public int tempAttack = 0;
        public int justBuffed = 0;

        /// <summary>
        /// 准备状态
        /// <para>泛绿光/可进行攻击</para>
        /// </summary>
        public bool Ready = false;

        /// <summary>
        /// 嘲讽
        /// </summary>
        public bool taunt = false;
        /// <summary>
        /// 受伤状态
        /// <para>hp red</para>
        /// </summary>
        public bool wounded = false;

        /// <summary>
        /// 圣盾
        /// </summary>
        public bool divineshild = false;
        /// <summary>
        /// 风怒
        /// </summary>
        public bool windfury = false;
        /// <summary>
        /// 冻结
        /// </summary>
        public bool frozen = false;
        /// <summary>
        /// 潜行
        /// </summary>
        public bool stealth = false;
        /// <summary>
        /// 免疫
        /// </summary>
        public bool immune = false;
        public bool untouchable = false;
        public bool exhausted = false;
        /// <summary>
        /// 吸血
        /// </summary>
        public bool lifesteal = false;
        public bool dormant = false;
        public bool outcast = false;//流放
		public bool reborn = false;
        public bool Spellburst = false;

        /// <summary>
        /// 冲锋
        /// </summary>
        public int charge = 0;
        /// <summary>
        /// 突袭
        /// </summary>
        public int rush = 0;
        /// <summary>
        /// 秘密抉择亡语
        /// <para>目前仅用于命运织网蛛，ICC_047t</para>
        /// </summary>
        public int hChoice = 0;
        /// <summary>
        /// 剧毒
        /// </summary>
        public bool poisonous = false;
        /// <summary>
        /// 生命值无法被降到1点以下
        /// </summary>
        public bool cantLowerHPbelowONE = false;

        /// <summary>
        /// 被沉默
        /// </summary>
        public bool silenced = false;
        /// <summary>
        /// 从手牌中打出
        /// </summary>
        public bool playedFromHand = false;
        public bool extraParam = false;
        public int extraParam2 = 0;

        public Minion()
        {
            this.handcard = new Handmanager.Handcard();
        }

        public Minion(Minion m)
        {
            //dont silence----------------------------
            //this.anzGotDmg = m.anzGotDmg;
            //this.GotDmgValue = m.GotDmgValue;
            //this.anzGotHealed = m.anzGotHealed;
            this.gotInspire = m.gotInspire;
            this.isHero = m.isHero;
            this.own = m.own;

            this.name = m.name;
            this.cardClass = m.cardClass;
            this.synergy = m.synergy;
            this.handcard = m.handcard;
            this.deathrattle2 = m.deathrattle2;
            this.entitiyID = m.entitiyID;
            this.zonepos = m.zonepos;

            this.allreadyAttacked = m.allreadyAttacked;


            this.playedThisTurn = m.playedThisTurn;
            this.playedPrevTurn = m.playedPrevTurn;
            this.numAttacksThisTurn = m.numAttacksThisTurn;
            this.immuneWhileAttacking = m.immuneWhileAttacking;

            
            this.shadowmadnessed = m.shadowmadnessed;

            this.ancestralspirit = m.ancestralspirit;
            this.desperatestand = m.desperatestand;
            this.destroyOnOwnTurnStart = m.destroyOnOwnTurnStart; // depends on own!
            this.destroyOnEnemyTurnStart = m.destroyOnEnemyTurnStart; // depends on own!
            this.destroyOnOwnTurnEnd = m.destroyOnOwnTurnEnd; // depends on own!
            this.destroyOnEnemyTurnEnd = m.destroyOnEnemyTurnEnd; // depends on own!
            this.changeOwnerOnTurnStart = m.changeOwnerOnTurnStart;

            this.conceal = m.conceal;
            this.souloftheforest = m.souloftheforest;
            this.stegodon = m.stegodon;
            this.livingspores = m.livingspores;
            this.explorershat = m.explorershat;
            this.returnToHand = m.returnToHand;
            this.infest = m.infest;

            this.ownBlessingOfWisdom = m.ownBlessingOfWisdom;
            this.enemyBlessingOfWisdom = m.enemyBlessingOfWisdom;
            this.ownPowerWordGlory = m.ownPowerWordGlory;
            this.enemyPowerWordGlory = m.enemyPowerWordGlory;
            this.spellpower = m.spellpower;

            this.Hp = m.Hp;
            this.maxHp = m.maxHp;
            this.armor = m.armor;

            this.Angr = m.Angr;
            this.AdjacentAngr = m.AdjacentAngr;
            this.tempAttack = m.tempAttack;
            this.justBuffed = m.justBuffed;

            this.Ready = m.Ready;

            this.taunt = m.taunt;
            this.wounded = m.wounded;

            this.divineshild = m.divineshild;
            this.windfury = m.windfury;
            this.frozen = m.frozen;
            this.stealth = m.stealth;
            this.immune = m.immune;
            this.untouchable = m.untouchable;
            this.exhausted = m.exhausted;

            this.charge = m.charge;
            this.rush = m.rush;
            this.hChoice = m.hChoice;
            this.poisonous = m.poisonous;
            this.lifesteal = m.lifesteal;
            this.dormant = m.dormant;
            this.outcast = m.outcast;
			this.reborn = m.reborn;
            this.cantLowerHPbelowONE = m.cantLowerHPbelowONE;
            this.Spellburst = m.Spellburst;

            this.silenced = m.silenced;
            this.cantBeTargetedBySpellsOrHeroPowers = m.cantBeTargetedBySpellsOrHeroPowers;
            this.cantAttackHeroes = m.cantAttackHeroes;
            this.cantAttack = m.cantAttack;
        }

        public void setMinionToMinion(Minion m)
        {
            //dont silence----------------------------
            this.anzGotDmg = m.anzGotDmg;
            this.GotDmgValue = m.GotDmgValue;
			this.anzGotHealed = m.anzGotHealed;
            this.gotInspire = m.gotInspire;
            this.isHero = m.isHero;
            this.own = m.own;

            this.name = m.name;
            this.cardClass = m.cardClass;
            this.synergy = m.synergy;
            this.handcard = m.handcard;
            this.deathrattle2 = m.deathrattle2;

            this.zonepos = m.zonepos;


            this.allreadyAttacked = m.allreadyAttacked;


            this.numAttacksThisTurn = m.numAttacksThisTurn;
            this.immuneWhileAttacking = m.immuneWhileAttacking;

            
            this.shadowmadnessed = m.shadowmadnessed;

            this.ancestralspirit = m.ancestralspirit;
            this.desperatestand = m.desperatestand;
            this.destroyOnOwnTurnStart = m.destroyOnOwnTurnStart; // depends on own!
            this.destroyOnEnemyTurnStart = m.destroyOnEnemyTurnStart; // depends on own!
            this.destroyOnOwnTurnEnd = m.destroyOnOwnTurnEnd; // depends on own!
            this.destroyOnEnemyTurnEnd = m.destroyOnEnemyTurnEnd; // depends on own!
            this.changeOwnerOnTurnStart = m.changeOwnerOnTurnStart;

            this.conceal = m.conceal;
            this.souloftheforest = m.souloftheforest;
            this.stegodon = m.stegodon;
            this.livingspores = m.livingspores;
            this.explorershat = m.explorershat;
            this.returnToHand = m.returnToHand;
            this.infest = m.infest;

            this.ownBlessingOfWisdom = m.ownBlessingOfWisdom;
            this.enemyBlessingOfWisdom = m.enemyBlessingOfWisdom;
            this.ownPowerWordGlory = m.ownPowerWordGlory;
            this.enemyPowerWordGlory = m.enemyPowerWordGlory;
            this.spellpower = m.spellpower;

            this.Hp = m.Hp;
            this.maxHp = m.maxHp;
            this.armor = m.armor;

            this.Angr = m.Angr;
            this.AdjacentAngr = m.AdjacentAngr;
            this.tempAttack = m.tempAttack;
            

            this.taunt = m.taunt;
            this.wounded = m.wounded;

            this.divineshild = m.divineshild;
            this.windfury = m.windfury;
            this.frozen = m.frozen;
            this.stealth = m.stealth;
            this.immune = m.immune;
            this.untouchable = m.untouchable;
            this.exhausted = m.exhausted;
            this.Spellburst = m.Spellburst;

            this.charge = m.charge;
            this.rush = m.rush;
            this.hChoice = m.hChoice;
            if ((m.charge > 0 ||m.rush > 0) && !m.frozen && !m.silenced) this.Ready = true;
            else this.Ready = false;
            if (m.rush > 0 && m.playedThisTurn) this.cantAttackHeroes = true;
            this.poisonous = m.poisonous;
            this.lifesteal = m.lifesteal;
            this.dormant = m.dormant;
            this.outcast = m.outcast;
			this.reborn = m.reborn;
            this.cantLowerHPbelowONE = m.cantLowerHPbelowONE;

            this.silenced = m.silenced;

            this.cantBeTargetedBySpellsOrHeroPowers = m.cantBeTargetedBySpellsOrHeroPowers;
            this.cantAttackHeroes = m.cantAttackHeroes;
            this.cantAttack = m.cantAttack;
        }

        public int getRealAttack()
        {
            return this.Angr;
        }

        public void getDamageOrHeal(int dmg, Playfield p, bool isMinionAttack, bool dontCalcLostDmg)
        {
            if (this.Hp <= 0) return;

            if (this.immune && dmg > 0 || this.untouchable) return;
            
            int damage = dmg;
            int heal = 0;
            if (dmg < 0) heal = -dmg;

            if (this.isHero)
            {
                if (this.own)
                {
                    if (p.ownWeapon.name == CardDB.cardName.cursedblade) dmg += dmg;
                    if (p.anzOwnAnimatedArmor > 0 && dmg > 0) dmg = 1;
                    if (p.anzOwnBolfRamshield > 0 && dmg > 0)


                    {
                        int rest = this.armor - dmg;
                        this.armor = Math.Max(0, rest);
                        if (rest < 0)
                        {
                            foreach (Minion m in p.ownMinions)
                            {
                                if (m.name == CardDB.cardName.bolframshield)
                                {
                                    m.getDamageOrHeal(-rest, p, true, false);
                                    break;
                                }
                            }
                        }
                        return;
                    }
                }
                else
                {
                    if (p.anzEnemyAnimatedArmor > 0 && dmg > 0) dmg = 1;
                    if (p.enemyWeapon.name == CardDB.cardName.cursedblade) dmg += dmg;
                    if (p.anzEnemyBolfRamshield > 0 && dmg > 0)
                    {
                        int rest = this.armor - dmg;
                        this.armor = Math.Max(0, rest);
                        if (rest < 0)
                        {
                            foreach (Minion m in p.enemyMinions)
                            {
                                if (m.name == CardDB.cardName.bolframshield)
                                {
                                    m.getDamageOrHeal(-rest, p, true, false);
                                    break;
                                }
                            }
                        }
                        return;
                    }
                }

                int copy = this.Hp;
                if (heal > 0)
                {
                    this.Hp = Math.Min(this.maxHp, this.Hp + heal);
                    if (copy < this.Hp)
                    {
                        p.tempTrigger.charsGotHealed++;
                        this.anzGotHealed++;
                        this.GotHealedValue += heal;
                    }
                }
                else if (dmg > 0)
                {
                    int rest = this.armor - dmg;
                    if (rest < 0) this.Hp += rest;
                    this.armor = Math.Max(0, this.armor - dmg);


                    if (this.cantLowerHPbelowONE && this.Hp <= 0) this.Hp = 1;
                    if (copy > this.Hp)
                    {
                        this.anzGotDmg++;
                        this.GotDmgValue += dmg;
                        if (this.own)
                        {
                            p.tempTrigger.ownMinionsGotDmg++;
                            p.tempTrigger.ownHeroGotDmg++;
                        }
                        else
                        {
                            p.tempTrigger.enemyMinionsGotDmg++;
                            p.tempTrigger.enemyHeroGotDmg++;
                        }
                        p.secretTrigger_HeroGotDmg(this.own, copy - this.Hp);
                    }
                }
                return;
            }

            //its a Minion--------------------------------------------------------------
            
            bool woundedbefore = this.wounded;
            if (damage > 0) this.allreadyAttacked = true;

            if (damage > 0 && this.divineshild)
            {
                p.minionLosesDivineShield(this);
                if (!own && !dontCalcLostDmg && p.turnCounter == 0)
                {
                    if (isMinionAttack)
                    {
                        p.lostDamage += damage - 1;
                    }
                    else
                    {
                        p.lostDamage += (damage - 1) * (damage - 1);
                    }
                }
                return;
            }

            if (this.cantLowerHPbelowONE && damage >= 1 && damage >= this.Hp) damage = this.Hp - 1;

            if (!own && !dontCalcLostDmg && this.Hp < damage && p.turnCounter == 0)
            {
                if (isMinionAttack)
                {
                    p.lostDamage += (damage - this.Hp);
                }
                else
                {
                    p.lostDamage += (damage - this.Hp) * (damage - this.Hp);
                }
            }

            int hpcopy = this.Hp;

            if (damage >= 1)
            {
                this.Hp = this.Hp - damage;
            }

            if (heal >= 1)
            {
                if (own && !dontCalcLostDmg && heal <= 999 && this.Hp + heal > this.maxHp) p.lostHeal += this.Hp + heal - this.maxHp;

                this.Hp = this.Hp + Math.Min(heal, this.maxHp - this.Hp);
            }



            if (this.Hp > hpcopy)
            {
                //minionWasHealed
                p.tempTrigger.minionsGotHealed++;
                p.tempTrigger.charsGotHealed++;
                this.anzGotHealed++;
                this.GotHealedValue += heal;
            }
            else if (this.Hp < hpcopy)
            {
                if (this.own) p.tempTrigger.ownMinionsGotDmg++;
                else p.tempTrigger.enemyMinionsGotDmg++;

                if (p.anzAcidmaw > 0)
                {
                    if (p.anzAcidmaw == 1)
                    {
                        if (this.name != CardDB.cardName.acidmaw) this.Hp = 0;
                    }
                    else this.Hp = 0;
                }

                this.anzGotDmg++;
                this.GotDmgValue += dmg;
            }

            if (this.maxHp == this.Hp)
            {
                this.wounded = false;
            }
            else
            {
                this.wounded = true;
            }



            if (this.name == CardDB.cardName.lightspawn && !this.silenced)
            {
                this.Angr = this.Hp;
            }

            if (woundedbefore && !this.wounded)
            {
                this.handcard.card.sim_card.onEnrageStop(p, this);
            }

            if (!woundedbefore && this.wounded)
            {
                this.handcard.card.sim_card.onEnrageStart(p, this);
            }
            
            if (this.Hp <= 0)
            {
                this.minionDied(p);
            }

        }

        public void minionDied(Playfield p)
        {
            if (this.name == CardDB.cardName.stalagg)
            {
                p.stalaggDead = true;
            }
            else
            {
                if (this.name == CardDB.cardName.feugen) p.feugenDead = true;
            }

            

            if (own)
            {

                p.tempTrigger.ownMinionsDied++;
                if (this.taunt) p.anzOwnTaunt--;
                if (this.handcard.card.race == 20)
                {
                    p.tempTrigger.ownBeastDied++;
                }
                else if (this.handcard.card.race == 17)
                {
                    p.tempTrigger.ownMechanicDied++;
                }
                else if (this.handcard.card.race == 14)
                {
                    p.tempTrigger.ownMurlocDied++;
                }
            }
            else
            {
                p.tempTrigger.enemyMinionsDied++;
                if (this.taunt) p.anzEnemyTaunt--;
                if (this.handcard.card.race == 20)
                {
                    p.tempTrigger.enemyBeastDied++;
                }
                else if (this.handcard.card.race == 17)
                {
                    p.tempTrigger.enemyMechanicDied++;
                }
                else if (this.handcard.card.race == 14)
                {
                    p.tempTrigger.enemyMurlocDied++;
                }
            }

            if (p.diedMinions != null)
            {
                GraveYardItem gyi = new GraveYardItem(this.handcard.card.cardIDenum, this.entitiyID, this.own);
                p.diedMinions.Add(gyi);
            }
        }

        public void updateReadyness()
        {
            Ready = false;
            if (cantAttack) return;

            if (isHero)
            {
				if (!frozen && Angr != 0 && ((charge >= 1 && playedThisTurn) || !playedThisTurn) && (numAttacksThisTurn == 0 || (numAttacksThisTurn == 1 && windfury))) Ready = true;
                return;
            }

			if (!frozen && (((charge >= 1 && playedThisTurn)) || !playedThisTurn || shadowmadnessed) && (numAttacksThisTurn == 0 || (numAttacksThisTurn == 1 && windfury) || (!silenced && this.name == CardDB.cardName.v07tr0n && numAttacksThisTurn <= 3))) Ready = true;
			if (!frozen && (((charge == 0 && rush >= 1 && playedThisTurn)) || !playedThisTurn || shadowmadnessed) && (numAttacksThisTurn == 0 || (numAttacksThisTurn == 1 && windfury) || (!silenced && this.name == CardDB.cardName.v07tr0n && numAttacksThisTurn <= 3)))
			{
				Ready = true;
				cantAttackHeroes = true;
		 
			}
            if (!frozen && (((charge > 0 && rush >= 1 && playedThisTurn)) || !playedThisTurn || shadowmadnessed) && (numAttacksThisTurn == 0 || (numAttacksThisTurn == 1 && windfury) || (!silenced && this.name == CardDB.cardName.v07tr0n && numAttacksThisTurn <= 3)))
            {
                Ready = true;
                cantAttackHeroes = false;

            }
        }

        public void becomeSilence(Playfield p)
        {
            if (this.untouchable) return;
            if (own)
            {
                p.spellpower -= spellpower;
                if (this.taunt) p.anzOwnTaunt--;
            }
            else
            {
                p.enemyspellpower -= spellpower;
                if (this.taunt) p.anzEnemyTaunt--;
            }
            spellpower = 0;

            deathrattle2 = null;
            p.minionGetOrEraseAllAreaBuffs(this, false);
            //buffs
            ancestralspirit = 0;
            desperatestand = 0;
            destroyOnOwnTurnStart = false;
            destroyOnEnemyTurnStart = false;
            destroyOnOwnTurnEnd = false;
            destroyOnEnemyTurnEnd = false;
            changeOwnerOnTurnStart = false;
            conceal = false;
            souloftheforest = 0;
            stegodon = 0;
            livingspores = 0;
            explorershat = 0;
            returnToHand = 0;
            infest = 0;
            deathrattle2 = null;
            if (this.name == CardDB.cardName.moatlurker && p.LurkersDB.ContainsKey(this.entitiyID)) p.LurkersDB.Remove(this.entitiyID);

            ownBlessingOfWisdom = 0;
            enemyBlessingOfWisdom = 0;
            ownPowerWordGlory = 0;
            enemyPowerWordGlory = 0;

            cantBeTargetedBySpellsOrHeroPowers = false;
            cantAttackHeroes = false;
            cantAttack = false;

            charge = 0;
            rush = 0;
            hChoice = 0;
            taunt = false;
            divineshild = false;
            windfury = false;
            frozen = false;
            stealth = false;
            immune = false;
            poisonous = false;
            cantLowerHPbelowONE = false;
            lifesteal = false;
            dormant = false;
            outcast = false;
			reborn = false;
            Spellburst = false;
            

            //delete enrage (if minion is silenced the first time)
            if (wounded && handcard.card.Enrage && !silenced)
            {
                handcard.card.sim_card.onEnrageStop(p, this);
            }

            //reset attack
            Angr = handcard.card.Attack;
            tempAttack = 0;//we dont toutch the adjacent buffs!


            //reset hp and heal it
            if (maxHp < handcard.card.Health)//minion has lower maxHp as his card -> heal his hp
            {
                Hp += handcard.card.Health - maxHp; //heal minion
            }
            maxHp = handcard.card.Health;
            if (Hp > maxHp) Hp = maxHp;

            if (!silenced)//minion WAS not silenced, deactivate his aura
            {
                handcard.card.sim_card.onAuraEnds(p, this);
            }

            silenced = true;
            this.updateReadyness();
            p.minionGetOrEraseAllAreaBuffs(this, true);
            if (own)
            {
                p.tempTrigger.ownMinionsChanged = true;
            }
            else
            {
                p.tempTrigger.enemyMininsChanged = true;
            }
            if (this.shadowmadnessed)
            {
                this.shadowmadnessed = false;
                p.shadowmadnessed--;
                p.minionGetControlled(this, !own, false);
            }
        }

        public Minion GetTargetForMinionWithSurvival(Playfield p, bool own)
        {
            if (this.Angr == 0) return null;
            if ((own ? p.enemyMinions.Count : p.ownMinions.Count) < 1) return (own ? p.enemyHero : p.ownHero);
            Minion target = new Minion();
            Minion targetTaumt = new Minion();
            foreach (Minion m in own ? p.enemyMinions : p.ownMinions)
            {
                if (m.taunt && !m.silenced)
                {
                    if (this.Hp > m.Hp && (m.Hp + m.Angr + m.Angr * (m.windfury ? 1 : 0)) > (targetTaumt.Hp + targetTaumt.Angr + targetTaumt.Angr * (targetTaumt.windfury ? 1 : 0))) targetTaumt = m;
                }
                else
                {
                    if (this.Hp > m.Hp && (m.Hp + m.Angr + m.Angr * (m.windfury ? 1 : 0)) > (target.Hp + target.Angr + target.Angr * (target.windfury ? 1 : 0))) target = m;
                }
            }
            if (targetTaumt.Hp > 0) return targetTaumt;
            if (target.Hp > 0) return target;
            return null;
        }

        public void loadEnchantments(List<miniEnch> enchants, int ownPlayerControler)
        {
            foreach (miniEnch me in enchants)
            {
                // reborns and destoyings----------------------------------------------

                
                if (me.CARDID == CardDB.cardIDEnum.EX1_363e || me.CARDID == CardDB.cardIDEnum.EX1_363e2) //BlessingOfWisdom
                {
                    if (me.controllerOfCreator == ownPlayerControler)
                    {
                        this.ownBlessingOfWisdom++;
                    }
                    else
                    {
                        this.enemyBlessingOfWisdom++;
                    }
                }

                if (me.CARDID == CardDB.cardIDEnum.AT_013e) //PowerWordGlory
                {
                    if (me.controllerOfCreator == ownPlayerControler)
                    {
                        this.ownPowerWordGlory++;
                    }
                    else
                    {
                        this.enemyPowerWordGlory++;
                    }
                }


                if (me.CARDID == CardDB.cardIDEnum.EX1_316e) //overwhelmingpower
                {
                    if (me.controllerOfCreator == ownPlayerControler)
                    {
                        this.destroyOnOwnTurnEnd = true;
                    }
                    else
                    {
                        this.destroyOnEnemyTurnEnd = true;
                    }
                }

                if (me.CARDID == CardDB.cardIDEnum.EX1_334e) //Dark Command
                {
                    this.shadowmadnessed = true;
                }

                if (me.CARDID == CardDB.cardIDEnum.FP1_030e) //Necrotic Aura
                {
                    //todo Eure Zauber kosten in diesem Zug (5) mehr.
                }
                if (me.CARDID == CardDB.cardIDEnum.NEW1_029t) //death to millhouse!
                {
                    // todo spells cost (0) this turn!
                }
                if (me.CARDID == CardDB.cardIDEnum.EX1_612o) //Power of the Kirin Tor
                {
                    // todo Your next Secret costs (0).
                }
               // if (me.CARDID == CardDB.cardIDEnum.EX1_084e) //warsongcommander
               // {
              //      this.charge++;
              //  }
                
                switch(me.CARDID)
                {
                    //ToDo: TBUD_1	Each turn, if you have less health then a your opponent, summon a free minion


                    // destroy-------------------------------------------------
                    case CardDB.cardIDEnum.CS2_063e:
                        if (me.controllerOfCreator == ownPlayerControler) this.destroyOnOwnTurnStart = true;
                        else this.destroyOnEnemyTurnStart = true;   //corruption
                        continue;
                    case CardDB.cardIDEnum.DREAM_05e:
                        if (me.controllerOfCreator == ownPlayerControler) this.destroyOnOwnTurnStart = true;
                        else this.destroyOnEnemyTurnStart = true;   //nightmare
                        continue;

                    // deathrattles-------------------------------------------------
                    case CardDB.cardIDEnum.LOE_105e: this.explorershat++; continue;
                    case CardDB.cardIDEnum.UNG_956e: this.returnToHand++; continue;
                        
                    case CardDB.cardIDEnum.CS2_038e: this.ancestralspirit++; continue;
                    case CardDB.cardIDEnum.ICC_244e: this.desperatestand++; continue;
                    case CardDB.cardIDEnum.EX1_158e: this.souloftheforest++; continue;
                    case CardDB.cardIDEnum.UNG_952e: this.stegodon++; continue;
                    case CardDB.cardIDEnum.UNG_999t2e: this.livingspores++; continue;
                        
                    case CardDB.cardIDEnum.OG_045a: this.infest++; continue;
                    case CardDB.cardIDEnum.LOE_019e: this.extraParam2 = me.copyDeathrattle; continue; //unearthedraptor
                   // case CardDB.cardIDEnum.LOE_012e: this.deathrattle2 = ; continue; //zzdeletetombexplorer


                    //conceal-------------------------------------------------
                    case CardDB.cardIDEnum.EX1_128e: this.conceal = true; continue;
                    case CardDB.cardIDEnum.NEW1_014e: this.conceal = true; continue;
                    case CardDB.cardIDEnum.PART_004e: this.conceal = true; continue;
                    case CardDB.cardIDEnum.OG_080de: this.conceal = true; continue; 

                    //cantLowerHPbelowONE-------------------------------------------------
                    case CardDB.cardIDEnum.NEW1_036e: this.cantLowerHPbelowONE = true; continue; //commandingshout
                    case CardDB.cardIDEnum.NEW1_036e2: this.cantLowerHPbelowONE = true; continue; //commandingshout

                    //spellpower-------------------------------------------------
                    case CardDB.cardIDEnum.GVG_010b: this.spellpower++; continue; //velenschosen
                    case CardDB.cardIDEnum.AT_006e: this.spellpower++; continue; //dalaran
                    case CardDB.cardIDEnum.EX1_584e: this.spellpower++; continue; //ancient mage

                    //charge-------------------------------------------------
                    case CardDB.cardIDEnum.AT_071e: this.charge++; continue;
                    case CardDB.cardIDEnum.CS2_103e2: this.charge++; continue;
                    case CardDB.cardIDEnum.TB_AllMinionsTauntCharge: this.charge++; continue;
                    case CardDB.cardIDEnum.DS1_178e: this.charge++; continue;

                    //adjacentbuffs-------------------------------------------------
                    case CardDB.cardIDEnum.EX1_565o: this.AdjacentAngr += 2; continue; //flametongue
                    case CardDB.cardIDEnum.EX1_162o: this.AdjacentAngr += 1; continue; //dire wolf alpha

                    //tempbuffs-------------------------------------------------
                    case CardDB.cardIDEnum.CS2_083e: this.tempAttack += 1; continue;
                    case CardDB.cardIDEnum.EX1_549o: this.tempAttack += 2; this.immune = true; continue;
                    case CardDB.cardIDEnum.AT_057o: this.immune = true; continue;
                    case CardDB.cardIDEnum.AT_039e: this.tempAttack += 2; continue;
                    case CardDB.cardIDEnum.AT_132_DRUIDe: this.tempAttack += 2; continue;
                    case CardDB.cardIDEnum.CS2_005o: this.tempAttack += 2; continue;
                    case CardDB.cardIDEnum.CS2_011o: this.tempAttack += 2; continue;
                    case CardDB.cardIDEnum.EX1_046e: this.tempAttack += 2; continue;
                    case CardDB.cardIDEnum.GVG_057a: this.tempAttack += 2; continue;
                    case CardDB.cardIDEnum.CS2_046e: this.tempAttack += 3; continue;
                    case CardDB.cardIDEnum.CS2_105e: this.tempAttack += 4; continue;
                    case CardDB.cardIDEnum.EX1_570e: this.tempAttack += 4; continue;
                    case CardDB.cardIDEnum.OG_047e: this.tempAttack += 4; continue;  
                    case CardDB.cardIDEnum.NAX12_04e: this.tempAttack += 6; continue;
                    case CardDB.cardIDEnum.GVG_011a: this.tempAttack += -2; continue;
                    case CardDB.cardIDEnum.CFM_661e: this.tempAttack += -3; continue;
                    case CardDB.cardIDEnum.BRM_001e: this.tempAttack += -1000; continue;
                    case CardDB.cardIDEnum.TU4c_008e: this.tempAttack += 8; continue;
                    case CardDB.cardIDEnum.CS2_045e: this.tempAttack += 3; continue;
                    case CardDB.cardIDEnum.CS2_188o: this.tempAttack += 2; continue;
                    case CardDB.cardIDEnum.CS2_017o: this.tempAttack += 1; continue;


                        
                    

                   




                }
            }
        }

    }

}