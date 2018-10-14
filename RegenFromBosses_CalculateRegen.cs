using Terraria;
using Terraria.ID; //For the NetmodeID constants
using Terraria.ModLoader;

//This file only contains a single method (sometimes called a "function"), but it's a very important one
//It calculates how much life regeneration should be given to players, based on the amount of slain bosses/mini-bosses/events
//It counts how many bosses/mini-bosses/events there are and how many of them have been slain in the world,
//multiplies the percentage of cleared things by the max regeneration value in the configuration file,
//and transmits the new regeneration value to all clients if hosting a server

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		public static void CalculateRegen()
		{
			if (Main.netMode!=NetmodeID.MultiplayerClient) //If playing singleplayer or hosting a server
			{
				tempSlainBosses=0; //Reset the amount of slain bosses...
				tempCountBosses=0; //...and the total amount of bosses
				//tempSlainMiniBosses=0; //Do the same for mini-bosses...
				//tempCountMiniBosses=0;
				//tempSlainEvents=0; //...and also for events
				//tempCountEvents=0;

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
					(NPC.downedSlimeKing      ? 1 : 0) + //Slime King
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

					HardcodedModSupport(); //Handle hardcoded mod boss support - See the RegenFromBosses_ModSupport.cs file
				/*}*/

				if (modLoadedModdersToolkit) //If Modder's Toolkit is loaded...
				{
					Main.NewText("[RegenFromBosses] Old HP/S: " + ((float)(regenLife)/2)); //...display the old regeneration per second...
					if (tempCountBosses>0) //Safeguard - Don't divide by less than one!
						Main.NewText("[RegenFromBosses] HP/S from Bosses: " + ((float)((tempSlainBosses*configLifeFromBosses)/tempCountBosses)/2)); //...the new regeneration per second from bosses...
					//if (tempCountMiniBosses>0) //Safeguard
						//Main.NewText("[RegenFromBosses] HP/S from Mini-Bosses: " + ((float)((tempSlainMiniBosses*configLifeFromMiniBosses)/tempCountMiniBosses)/2)); //...from mini-bosses...
					//if (tempCountEvents>0) //Safeguard
						//Main.NewText("[RegenFromBosses] HP/S from Events: " + ((float)((tempSlainEvents*configLifeFromEvents)/tempCountEvents)/2)); //...and from events
				}

				//Finally, set regenLife to the slain things, multiplied by the regeneration from things, divided by the amount of things
				regenLife=0; //First, reset the life regeneration variable...
				if (tempCountBosses>0) //Safeguard - Don't divide by less than one!
					regenLife+=(tempSlainBosses*configLifeFromBosses)/tempCountBosses; //...then add the life regeneration from bosses...
				//if (tempCountMiniBosses>0) //Safeguard
					//regenLife+=(tempSlainMiniBosses*configLifeFromMiniBosses)/tempCountMiniBosses; //...from mini-bosses...
				//if (tempCountEvents>0) //Safeguard
					//regenLife+=(tempSlainEvents*configLifeFromEvents)/tempCountEvents; //...and from events
				if (modLoadedModdersToolkit) //If Modder's Toolkit is loaded...
					Main.NewText("[RegenFromBosses] New HP/S: " + ((float)(regenLife)/2)); //...display the new regeneration per second

				if (Main.netMode==NetmodeID.Server) //And lastly, if hosting a server...
					NetSendRegen(); //...send the new life regeneration value to all clients - See the RegenFromBosses_NetSynch.cs file
			}
		}
	}
}
