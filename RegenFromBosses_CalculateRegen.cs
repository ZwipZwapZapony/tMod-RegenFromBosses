using Terraria;
//using Terraria.ID; //For the NetmodeID constants
using Terraria.ModLoader;

//This file only contains a single method (sometimes called a "function"), but it's a very important one
//It calculates how much life regeneration should be given to players, based on the amount of slain bosses/mini-bosses/events
//It counts how many bosses/mini-bosses/events there are and how many of them have been slain in the world,
//and multiplies the percentage of cleared things by the max regeneration value in the configuration file

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		public static void CalculateRegen()
		{
			//if (Main.netMode!=NetmodeID.MultiplayerClient) //If playing singleplayer or hosting a server
			//{
				tempSlainBosses=0; //Reset the amount of slain bosses...
				tempCountBosses=0; //...and the total amount of bosses
				tempSlainMiniBosses=0; //Do the same for mini-bosses...
				tempCountMiniBosses=0;
				tempSlainEvents=0; //...and also for events
				tempCountEvents=0;

				/*if (modLoadedBossChecklist) //If Boss Checklist is loaded, use that for high mod compatibility
				{
					//The below is currently(?) impossible(?), as Boss Checklist doesn't seem to have a way for other mods to check the list's beaten bosses and such
					foreach (BossInfo bossinfo in BossChecklist.bossTracker.allBosses)
					{
						if (bossinfo.available())
						{
							switch (bossinfo.type)
							{
								case BossChecklistType.Boss:
									tempSlainBosses+=bossinfo.downed();
									tempCountBosses+=1;
									break;
								case BossChecklistType.MiniBoss:
									tempSlainMiniBosses+=bossinfo.downed();
									tempCountMiniBosses+=1;
									break;
								case BossChecklistType.Event:
									tempSlainEvents+=bossinfo.downed();
									tempCountEvents+=1;
									break;
							}
						}
					}
				}
				else //If Boss Checklist isn't loaded, use hardcoded boss support
				/*{*/
					//Let's check which bosses have been slain, and increase the amount of slain bosses appropriately
					tempSlainBosses+=( //The "? 1 : 0" parts below basically convert bools to ints, as you can't add bools together
					(NPC.downedSlimeKing      ? 1 : 0) + //King Slime
					(NPC.downedBoss1          ? 1 : 0) + //Eye of Cthulhu
					(NPC.downedBoss2          ? 1 : 0) + //Eater of Worlds and/or Brain of Cthulhu
					(NPC.downedQueenBee       ? 1 : 0) + //Queen Bee
					(NPC.downedBoss3          ? 1 : 0) + //Skeletron
					(Main.hardMode            ? 1 : 0) + //Wall of Flesh
					(NPC.downedMechBoss1      ? 1 : 0) + //The Destroyer
					(NPC.downedMechBoss2      ? 1 : 0) + //The Twins
					(NPC.downedMechBoss3      ? 1 : 0) + //Skeletron Prime
					(NPC.downedPlantBoss      ? 1 : 0) + //Plantera
					(NPC.downedGolemBoss      ? 1 : 0) + //Golem
					(NPC.downedFishron        ? 1 : 0) + //Duke Fishron
					(NPC.downedAncientCultist ? 1 : 0) + //Lunatic Cultist
					(NPC.downedMoonlord       ? 1 : 0)   //Moon Lord
					);
					tempCountBosses+=14; //Terraria has 14 bosses

					//Now, let's do the same with mini-bosses
					tempSlainMiniBosses+=(
					(NPC.downedClown             ? 1 : 0) + //Clown
					(NPC.downedChristmasIceQueen ? 1 : 0) + //Ice Queen
					(NPC.downedChristmasSantank  ? 1 : 0) + //Santa-NK1
					(NPC.downedChristmasTree     ? 1 : 0) + //Everscream
					(NPC.downedHalloweenKing     ? 1 : 0) + //Pumpking
					(NPC.downedHalloweenTree     ? 1 : 0)   //Mourning Wood
					);
					tempCountMiniBosses+=6; //Terraria has 6 mini-bosses... That excludes Betsy, which is not in the Boss Checklist mod by default, making me not want to add it here...

					//And lastly, do the same with events
					tempSlainEvents+=(
					(NPC.downedGoblins                                                ? 1 : 0) + //Goblin Army
					(NPC.downedFrost                                                  ? 1 : 0) + //Frost Legion
					(NPC.downedPirates                                                ? 1 : 0) + //Pirate Invasion
					(NPC.downedMartians                                               ? 1 : 0) + //Martian Madness
					(Terraria.GameContent.Events.DD2Event.DownedInvasionAnyDifficulty ? 1 : 0) + //Old One's Army, any tier
					(NPC.downedTowerNebula                                            ? 1 : 0) + //Nebula Pillar
					(NPC.downedTowerVortex                                            ? 1 : 0) + //Vortex Pillar
					(NPC.downedTowerSolar                                             ? 1 : 0) + //Solar Pillar
					(NPC.downedTowerStardust                                          ? 1 : 0)   //Stardust Pillar
					);
					tempCountEvents+=9; //Here are 9 events - Though, that includes the Lunar Events pillars separately, and excludes the Old One's Army's separate tiers and some other events...

					HardcodedModSupport(); //Handle hardcoded mod boss support - See the RegenFromBosses_ModSupport.cs file
				/*}*/

				if (modLoadedModdersToolkit) //If Modder's Toolkit is loaded...
				{
					Main.NewText("[RegenFromBosses] Old regeneration: [c/FF92A9:" + ((float)(regenLife)/2) + " HP/S,] [c/92A9FF:" + ((float)(regenMana)/2) + " MP/S]"); //...display the old regeneration per second
					if (tempCountBosses>0) //Safeguard - Don't divide by less than one!
					{
						Main.NewText("[RegenFromBosses] Bosses slain: " + (tempSlainBosses) + "/" + (tempCountBosses) + ": [c/FF92A9:" + //...how many bosses are slain and the total amount of bosses...
						((float)((tempSlainBosses*Config_ServerSide.Instance.configLifeFromBosses*2)/tempCountBosses)/2) + " HP/S,] [c/92A9FF:" +
						((float)((tempSlainBosses*Config_ServerSide.Instance.configManaFromBosses*2)/tempCountBosses)/2) + " MP/S]"); //...the new regeneration per second from bosses...
					}
					if (tempCountMiniBosses>0) //Safeguard
					{
						Main.NewText("[RegenFromBosses] Mini-bosses slain: " + (tempSlainMiniBosses) + "/" + (tempCountMiniBosses) + ": [c/FF92A9:" + //...and mini-bosses...
						((float)((tempSlainMiniBosses*Config_ServerSide.Instance.configLifeFromMiniBosses)/tempCountMiniBosses)/2) + " HP/S,] [c/92A9FF:" +
						((float)((tempSlainMiniBosses*Config_ServerSide.Instance.configManaFromMiniBosses)/tempCountMiniBosses)/2) + " MP/S]");
					}
					if (tempCountEvents>0) //Safeguard
					{
						Main.NewText("[RegenFromBosses] Events slain: " + (tempSlainEvents) + "/" + (tempCountEvents) + ": [c/FF92A9:" + //...and events...
						((float)((tempSlainEvents*Config_ServerSide.Instance.configLifeFromMiniBosses*2)/tempCountEvents)/2) + " HP/S,] [c/92A9FF:" +
						((float)((tempSlainEvents*Config_ServerSide.Instance.configManaFromMiniBosses*2)/tempCountEvents)/2) + " MP/S]");
					}
					if (Config_ServerSide.Instance.configLifePassive!=0 || Config_ServerSide.Instance.configManaPassive!=0) //If a passive regeneration bonus is active...
					{
						Main.NewText("[RegenFromBosses] Passive bonus: [c/FF92A9:" + //...also display that
						(Config_ServerSide.Instance.configLifePassive) + " HP/S,] [c/92A9FF:" +
						(Config_ServerSide.Instance.configManaPassive) + " MP/S]");
					}
				}

				//Finally, set regenLife/regenMana to the slain things, multiplied by the regeneration from things, divided by the amount of things
				regenLife=Config_ServerSide.Instance.configLifePassive*2; //First, reset the life regeneration variable to the flat passive bonus...
				regenMana=Config_ServerSide.Instance.configManaPassive*2; //...do the same for mana...
				if (tempCountBosses>0) //Safeguard - Don't divide by less than one!
				{
					regenLife+=(tempSlainBosses*(Config_ServerSide.Instance.configLifeFromBosses)*2)/tempCountBosses; //...then add the life regeneration from bosses...
					regenMana+=(tempSlainBosses*(Config_ServerSide.Instance.configManaFromBosses)*2)/tempCountBosses; //...do the same for mana...
				}
				if (tempCountMiniBosses>0) //Safeguard
				{
					regenLife+=(tempSlainMiniBosses*(Config_ServerSide.Instance.configLifeFromMiniBosses)*2)/tempCountMiniBosses; //...from mini-bosses...
					regenMana+=(tempSlainMiniBosses*(Config_ServerSide.Instance.configManaFromMiniBosses)*2)/tempCountMiniBosses; //...do the same for mana...
				}
				if (tempCountEvents>0) //Safeguard
				{
					regenLife+=(tempSlainEvents*(Config_ServerSide.Instance.configLifeFromEvents)*2)/tempCountEvents; //...and from events...
					regenMana+=(tempSlainEvents*(Config_ServerSide.Instance.configManaFromEvents)*2)/tempCountEvents; //...do the same for mana
				}

				if (modLoadedModdersToolkit) //If Modder's Toolkit is loaded...
					Main.NewText("[RegenFromBosses] New regeneration: [c/FF92A9:" + ((float)(regenLife)/2) + " HP/S,] [c/92A9FF:" + ((float)(regenMana)/2) + " MP/S]"); //...display the new regeneration per second

				//if (Main.netMode==NetmodeID.Server) //And lastly, if hosting a server...
					//NetSendRegen(); //...send the new life regeneration value to all clients - See the RegenFromBosses_NetSynch.cs file
			//}
		}
	}
}