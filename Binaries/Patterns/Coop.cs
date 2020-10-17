
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot;
using LeagueBot.Patterns;
using LeagueBot.Game.Enums;
using LeagueBot.Game.Misc;

namespace LeagueBot
{
    public class Coop : PatternScript
    {

        private Point CastTargetPoint
        {
            get;
            set;
        }
        private int AllyIndex
        {
            get;
            set;
        }

        private Item[] Items = new Item[]
        {
            new Item("Dorans Ring",400),
            new Item("Health Potion",50),
            new Item("Warding Totem",0),
            new Item("Boots of Speed",300),
            new Item("Lost Chapter",1300),
            new Item("Sorcerers Shoes",800),
            new Item("Blasting Wand",850),
            new Item("Ludens Echo",1050), // <--- Cost when Lost Chapter & Blasting Wand were bought
            new Item("Needlessly Large Rod",1250),
            new Item("Needlessly Large Rod",1250),
            new Item("Rabadons Deathcap",1100),
            new Item("Amplifying tome", 435),
            new Item("Blasting wand", 850),
            new Item("void staff", 1365)
        };

<<<<<<< Updated upstream
        public override bool ThrowException 
=======
        public override bool ThrowException
>>>>>>> Stashed changes
        {
            get
            {
                return false;
            }
        }

        public override void Execute()
        {
            bot.log("Waiting for league of legends process...");

<<<<<<< Updated upstream
            bot.waitProcessOpen(Constants.GameProcessName); 

            bot.waitUntilProcessBounds(Constants.GameProcessName, 1030, 797);
=======
            bot.waitProcessOpen(GameProcessName); // 120 seconds timeout

            bot.waitUntilProcessBounds(GameProcessName, 1030, 797);
>>>>>>> Stashed changes

            bot.wait(200);

            bot.log("Waiting for game to load.");

<<<<<<< Updated upstream
            bot.bringProcessToFront(Constants.GameProcessName);
            bot.centerProcess(Constants.GameProcessName);
=======
            bot.bringProcessToFront(GameProcessName);
            bot.centerProcess(GameProcessName);
>>>>>>> Stashed changes

            game.waitUntilGameStart();

            bot.log("Game Started");

<<<<<<< Updated upstream
            bot.bringProcessToFront(Constants.GameProcessName);
            bot.centerProcess(Constants.GameProcessName);
=======
            bot.bringProcessToFront(GameProcessName);
            bot.centerProcess(GameProcessName);
>>>>>>> Stashed changes

            bot.wait(3000);

            if (game.getSide() == SideEnum.Blue)
            {
                CastTargetPoint = new Point(1084, 398);
                bot.log("We are blue side !");
            }
            else
            {
                CastTargetPoint = new Point(644, 761);
                bot.log("We are red side !");
            }

            game.player.upgradeSpellOnLevelUp();

            OnSpawnJoin();

<<<<<<< Updated upstream
            bot.log("Playing...");

=======
>>>>>>> Stashed changes
            GameLoop();

            this.End();
        }
        private void BuyItems()
        {
            int golds = game.player.getGolds();

            game.shop.toogle();

            foreach (Item item in Items)
            {
                if (item.Cost > golds)
                {
                    break;
                }
                if (!item.Buyed)
                {
                    game.shop.searchItem(item.Name);

                    game.shop.buySearchedItem();

                    item.Buyed = true;

                    golds -= item.Cost;
                }
            }

            game.shop.toogle();

        }
        private void GameLoop()
        {
<<<<<<< Updated upstream
=======
            game.camera.lockAlly(1);

            bot.log("Following me");

>>>>>>> Stashed changes
            int level = game.player.getLevel();

            bool dead = false;

            bool isRecalling = false;

<<<<<<< Updated upstream
            while (bot.isProcessOpen(Constants.GameProcessName))
            {
                bot.bringProcessToFront(Constants.GameProcessName);

                bot.centerProcess(Constants.GameProcessName);
=======
            while (bot.isProcessOpen(GameProcessName))
            {
                bot.bringProcessToFront(GameProcessName);

                bot.centerProcess(GameProcessName);
>>>>>>> Stashed changes

                int newLevel = game.player.getLevel();

                if (newLevel != level)
                {
                    level = newLevel;
                    game.player.upgradeSpellOnLevelUp();
                }


                if (game.player.dead())
                {
                    if (!dead)
                    {
                        dead = true;
                        isRecalling = false;
                        OnDie();
                    }

                    bot.wait(4000);
                    continue;
                }

                if (dead)
                {
                    dead = false;
                    OnRevive();
                    continue;
                }

                if (isRecalling)
                {
                    game.player.recall();
                    bot.wait(4000);

                    if ((game.player.getManaPercent() == 1) & (game.player.getHealthPercent() == 1))
                    {
                        OnSpawnJoin();
                        isRecalling = false;
                    }
                    continue;
                }



                if (game.player.getManaPercent() <= 0.10d)
                {
                    isRecalling = true;
                    continue;
                }

                if (game.player.getHealthPercent() <= 0.3d)
                {
                    game.player.FlashDown(6);
                    isRecalling = true;
                    continue;
                }


                CastAndMove();


            }
        }
        private void OnDie()
        {
            BuyItems();
        }
        private void OnSpawnJoin()
        {
            BuyItems();
            game.camera.lockAlly(1);
        }
        private void OnRevive()
        {
<<<<<<< Updated upstream
            AllyIndex = game.getAllyIdToFollow();
            game.camera.lockAlly(AllyIndex);
=======
            game.camera.lockAlly(1);

            bot.log("i die");
>>>>>>> Stashed changes
        }

        private void CastAndMove() // Replace this by Champion pattern script.
        {
            game.moveUpRightScreen();

            //game.player.tryCastSpellOnTarget(4); 

            //game.player.tryCastSpellOnTarget(2);

            //game.player.tryCastSpellOnTarget(3); 

            //game.player.tryCastSpellOnTarget(1); 

            //game.player.tryCastSpellOnTarget(5); 

            //game.player.tryCastSpellOnTarget(6);

            // fast spell cast - currently for Ryze combo
            game.player.FakerSpellCast(1);
            game.player.FakerSpellCast(2);
            game.player.FakerSpellCast(1);
            game.player.FakerSpellCast(3);
            game.player.FakerSpellCast(1);
            game.player.FakerSpellCast(5);

        }

        public override void End()
        {
            bot.executePattern("EndCoop");
            base.End();
        }
    }
}
