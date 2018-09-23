using System.IO; //For multiplayer synchronization(?)
using System.Linq; //For C#6 compiling
using Terraria;
using Terraria.ModLoader;

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		public RegenFromBosses()
		{
		}
		public static int lifeRegenFromBosses=0; //How much life is regenerated per second from slain bosses - Half of this is regenerated per second
		public static float lifeRegenFromBosses_perBoss=1f; //How much to add to the life regeneration per slain boss...
		//public static float lifeRegenFromBosses_perMiniBoss=0f; //...per mini-boss...
		//public static float lifeRegenFromBosses_perEvent=0f; //...and per event
		//public static int lifeRegenFromBosses_max=40; //Maximum life regeneration from this mod
		public static float lifeRegenFromBosses_tempCalc=0f; //Temporary life regeneration calculation, which is then turned from a float to an integer once calculation is done

		//public static int manaRegenFromBosses=0; //Same as life above, but for mana
		//public static float manaRegenFromBosses_perBoss=0f; //How much mana regeneration per slain boss...
		//public static float manaRegenFromBosses_perMiniBoss=0f; //...per mini-boss...
		//public static float manaRegenFromBosses_perEvent=0f; //...and per event
		//public static int manaRegenFromBosses_max=40; //Maximum mana regeneration from this mod
		//public static float manaRegenFromBosses_tempCalc=0f; //Temporary mana regeneration calculation

		public static bool calculateRegenFromBosses=false; //Whether or not to re-calculate the regeneration amount

		//The below function calculates how much life and mana regeneration should be given to players, based on the amount of slain bosses/mini-bosses/events
		//It resets the life and mana regeneration variables, iterates through everything in Boss Checklist's list (except not yet), checks if something has been slain, -
		//- checks what type it is (boss/mini-boss/event) (except not yet), and increases the life and mana regeneration variables based on the per-boss/-mini-boss/-event variables
		public static void CalculateRegen()
		{
			lifeRegenFromBosses_tempCalc=0;
			//manaRegenFromBosses_tempCalc=0;
			//Main.NewText("Old HP/S: " + ((float)(RegenFromBosses.lifeRegenFromBosses)/2)); //Debug stuff

			/*if (modLoadedBossChecklist) //If Boss Checklist is loaded, use that for high mod compatibility
			{
				//The below is currently(?) impossible(?), as Boss Checklist doesn't seem to have a way for other mods to check the list's beaten bosses and such
				foreach (BossInfo boss in BossChecklist.bossTracker.allBosses)
				{
					if (boss.available() && boss.downed())
					{
						switch (boss.type)
						{
							case BossChecklistType.Boss:
								lifeRegenFromBosses_tempCalc+=lifeRegenFromBosses_perBoss;
								manaRegenFromBosses_tempCalc+=manaRegenFromBosses_perBoss;
								break;
							case BossChecklistType.MiniBoss:
								lifeRegenFromBosses_tempCalc+=lifeRegenFromBosses_perMiniBoss;
								manaRegenFromBosses_tempCalc+=manaRegenFromBosses_perMiniBoss;
								break;
							case BossChecklistType.Event:
								lifeRegenFromBosses_tempCalc+=lifeRegenFromBosses_perEvent;
								manaRegenFromBosses_tempCalc+=manaRegenFromBosses_perEvent;
								break;
						}
					}
				}
				lifeRegenFromBosses=(int)lifeRegenFromBosses_tempCalc;
				//manaRegenFromBosses=(int)manaRegenFromBosses_tempCalc;
			}
			else //If Boss Checklist isn't loaded, use hardcoded boss support
			/*{*/
				lifeRegenFromBosses_tempCalc+=( //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
				(NPC.downedSlimeKing      ? 1f : 0f) + //Slime King
				(NPC.downedBoss1          ? 1f : 0f) + //Eye of Cthulhu
				(NPC.downedBoss2          ? 1f : 0f) + //Eater of Worlds and/or Brain of Cthulhu
				(NPC.downedQueenBee       ? 1f : 0f) + //Queen Bee
				(NPC.downedBoss3          ? 1f : 0f) + //Skeletron
				(Main.hardMode            ? 1f : 0f) + //Wall of Flesh
				(NPC.downedMechBoss1      ? 1f : 0f) + //The Destroyer
				(NPC.downedMechBoss2      ? 1f : 0f) + //The Twins
				(NPC.downedMechBoss3      ? 1f : 0f) + //Skeletron Prime
				(NPC.downedPlantBoss      ? 1f : 0f) + //Plantera
				(NPC.downedGolemBoss      ? 1f : 0f) + //Golem
				(NPC.downedFishron        ? 1f : 0f) + //Duke Fishron
				(NPC.downedAncientCultist ? 1f : 0f) + //Lunatic Cultist
				(NPC.downedMoonlord       ? 1f : 0f)   //Moon Lord
				)*lifeRegenFromBosses_perBoss;

				HardcodedModSupport(); //Handle hardcoded mod boss support - See the RegenFromBosses_ModSupport.cs file
			/*}*/
			lifeRegenFromBosses=(int)lifeRegenFromBosses_tempCalc; //Finally, set lifeRegenFromBosses to what we calculated
			//manaRegenFromBosses=(int)manaRegenFromBosses_tempCalc; //And do the same with manaRegenFromBosses
			//Main.NewText("New HP/S: " + ((float)(RegenFromBosses.lifeRegenFromBosses)/2f)); //Debug stuff
		}

		public override object Call(params object[] input) //Allow other mods to manually make this mod recalculate the regeneration amount with a Call
		{
			if (input[0] == "RegenFromBosses_Calculate")
			{
				calculateRegenFromBosses=true;
				return true;
			}
			else
				return false;
			//This doesn't need to be done after slaying a NPC with the "boss" variable set to true, in which case this happens automatically
			//But in other cases (such as after clearing an event), I think that the Call usage for other mods is as follows:
			//Mod RFB = ModLoader.GetMod("RegenFromBosses");
			//if (RFB != null)
			//	RFB.Call("RegenFromBosses_Calculate");
		}
	}

	public class RegenFromBosses_Player : ModPlayer //Hook into stuff for every player
	{
		public override void UpdateLifeRegen() //Regenerate some life and mana!
		{
			//Crude check to see if there's something that should prevent life regeneration
			if (!(player.lifeRegen<0 || player.bleed || player.burned || player.electrified || player.onFire || player.onFire2 || player.onFrostBurn || player.poisoned || player.suffocating || player.venom || (Main.expertMode && player.tongued)))
				player.lifeRegen+=RegenFromBosses.lifeRegenFromBosses;
			//if (player.manaRegen>=0)
				//player.manaRegen+=RegenFromBosses.manaRegenFromBosses;
		}

		public override void OnRespawn(Player player) //When a player dies and respawns...
		{
			RegenFromBosses.calculateRegenFromBosses=true; //...recalculate the regeneration (just as an extra measure to keep the regeneration updated without being too taxing)
		}
	}

	public class RegenFromBosses_NPC : GlobalNPC //Hook into stuff for all NPCs
	{
		public override void NPCLoot(NPC npc) //When a NPC dies...
		{
			if (npc.boss) //...if the NPC is an enemy boss...
				RegenFromBosses.calculateRegenFromBosses=true; //...recalculate the regeneration
		}
	}

	public class RegenFromBosses_World : ModWorld //Hook into stuff for the world
	{
		public override void Initialize() //When a world is loaded...
		{
			RegenFromBosses.lifeRegenFromBosses=0; //...reset the life regeneration amount...
			//manaRegenFromBosses=0; //...and the mana regeneration amount...
			RegenFromBosses.calculateRegenFromBosses=true; //...and then recalculate the regeneration (so that it matches the given world's defeated bosses/mini-bosses/events)
		}

		public override void PostUpdate() //At the end of every frame...
		{
			if (RegenFromBosses.calculateRegenFromBosses==true) //...if regeneration is to be recalculated...
			{
				RegenFromBosses.CalculateRegen(); //...recalculate it...
				RegenFromBosses.calculateRegenFromBosses=false; //...and make sure that we don't recalculate it the next frame
			}
		}

		public override void NetSend(BinaryWriter writer) //I don't know, can't test multiplayer... But the server side of when a client joins a server?
		{
			writer.Write(RegenFromBosses.lifeRegenFromBosses); //Hopefully send the current life regeneration variable value to the new client?
			//writer.Write(RegenFromBosses.manaRegenFromBosses); //And do the same for mana
		}

		public override void NetReceive(BinaryReader reader) //I don't know, can't test multiplayer... But the client side of when a client joins a server?
		{
			RegenFromBosses.lifeRegenFromBosses = reader.ReadInt32(); //Hopefully receive the current life regeneration variable value from the server?
			//RegenFromBosses.manaRegenFromBosses = reader.ReadInt32(); //And do the same for mana
		}
	}
}
