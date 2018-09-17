using Terraria;
using Terraria.ModLoader;
using System.Linq; //For C#6 compiling

namespace RegenFromBosses
{
	public class RegenFromBosses : Mod
	{
		public RegenFromBosses()
		{
		}
		public static int lifeRegenFromBosses=0; //How much life is regenerated per second from slain bosses - Half of this is regenerated per second
		public static float lifeRegenFromBosses_perBoss=1f; //How much to add to the life regeneration per slain boss...
		//public static float lifeRegenFromBosses_perMiniBoss=0f; //...per mini-boss...
		//public static float lifeRegenFromBosses_perEvent=0f; //...and per event
		//public static int lifeRegenFromBosses_max=40; //Maximum life regeneration from this mod
		static float lifeRegenFromBossesTemp=0f; //Temporary life regeneration calculation, which is then turned from a float to an integer once calculation is done

		//public static int manaRegenFromBosses=0; //Same as life above, but for mana
		//public static float manaRegenFromBosses_perBoss=0f; //How much mana regeneration per slain boss...
		//public static float manaRegenFromBosses_perMiniBoss=0f; //...per mini-boss...
		//public static float manaRegenFromBosses_perEvent=0f; //...and per event
		//public static int manaRegenFromBosses_max=40; //Maximum mana regeneration from this mod
		//static float manaRegenFromBossesTemp=0f; //Temporary mana regeneration calculation

		//static Mod bossChecklistMod = ModLoader.GetMod("BossChecklist"); //Fetch the Boss Checklist mod, for high mod boss compatibility (except not yet)
		static bool modLoadedAntiaris;    //Hardcoded Antiaris boss support
		static bool modLoadedCalamityMod; //Hardcoded Calamity Mod boss support
		static bool modLoadedOcram;       //Hardcoded Ocram 'n Stuff boss support
		static bool modLoadedThoriumMod;  //Hardcoded Thorium Mod boss support
		//static bool modLoadedTremor;      //Hardcoded Tremor boss support

		//The below function calculates how much life and mana regeneration should be given to players
		//It resets the life and mana regeneration variables, iterates through everything in Boss Checklist's list (except not yet), checks if something has been slain, -
		//- checks what type it is (boss/mini-boss/event) (except not yet), and increases the life and mana regeneration variables based on the per-boss/-mini-boss/-event variables
		public static void CalculateRegen()
		{
			//Main.NewText("Old HP/S: " + ((float)(RegenFromBosses.lifeRegenFromBosses)/2)); //Debug stuff
			//The below is currently(?) impossible(?), as Boss Checklist doesn't seem to have a way for other mods to check the list's beaten bosses and such
			/*lifeRegenFromBossesTemp=0;
			manaRegenFromBossesTemp=0;
			foreach (BossInfo boss in BossChecklist.bossTracker.allBosses)
			{
				if (boss.available() && boss.downed())
				{
					switch (boss.type)
					{
						case BossChecklistType.Boss:
							lifeRegenFromBossesTemp+=lifeRegenFromBosses_perBoss;
							manaRegenFromBossesTemp+=manaRegenFromBosses_perBoss;
							break;
						case BossChecklistType.MiniBoss:
							lifeRegenFromBossesTemp+=lifeRegenFromBosses_perMiniBoss;
							manaRegenFromBossesTemp+=manaRegenFromBosses_perMiniBoss;
							break;
						case BossChecklistType.Event:
							lifeRegenFromBossesTemp+=lifeRegenFromBosses_perEvent;
							manaRegenFromBossesTemp+=manaRegenFromBosses_perEvent;
							break;
					}
				}
			}
			lifeRegenFromBosses=(int)lifeRegenFromBossesTemp;
			manaRegenFromBosses=(int)manaRegenFromBossesTemp;*/
			//So instead, have this hardcoded vanilla Terraria-, Calamity Mod-, and Thorium Mod-only boss support
			lifeRegenFromBossesTemp= //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
			((NPC.downedSlimeKing     ? 1f : 0f) + //Slime King
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
			(NPC.downedMoonlord       ? 1f : 0f))  //Moon Lord
			*lifeRegenFromBosses_perBoss;
			if (modLoadedAntiaris) //If Antiaris is loaded
				ModSupportCalamityMod(); //Do some Antiaris boss check stuff... in a separate function to avoid crashes for people who don't have Antiaris loaded
			if (modLoadedCalamityMod) //And do the same for Calamity Mod
				ModSupportCalamityMod();
			if (modLoadedOcram) //And also for Ocram 'n Stuff
				ModSupportOcram();
			if (modLoadedThoriumMod) //And for Thorium Mod
				ModSupportThoriumMod();
			//if (modLoadedTremor) //As well as for Tremor
				//ModSupportTremor();
			lifeRegenFromBosses=(int)lifeRegenFromBossesTemp; //Finally, set lifeRegenFromBosses to what we calculated
			//Main.NewText("New HP/S: " + ((float)(RegenFromBosses.lifeRegenFromBosses)/2f)); //Debug stuff
		}
		public override object Call(params object[] args) //Allow other mods to manually make this mod recalculate the regeneration amount, for example right after clearing an event, with a Call
		{
			if (args[0] == "CalculateRegenFromBosses")
			{
				CalculateRegen();
				return true;
			}
			else
				return false;
			//Call usage for other mods, I think:
			//Mod RFB = ModLoader.GetMod("RegenFromBosses");
			//if (RFB != null)
			//	RFB.Call("CalculateRegenFromBosses");
		}
		public override void Load()
		{
			modLoadedAntiaris    = ModLoader.GetMod("Antiaris")    != null; //Hardcoded Antiaris boss support
			modLoadedCalamityMod = ModLoader.GetMod("CalamityMod") != null; //Hardcoded Calamity Mod boss support
			modLoadedOcram       = ModLoader.GetMod("Ocram")       != null; //Hardcoded Ocram 'n Stuff boss support
			modLoadedThoriumMod  = ModLoader.GetMod("ThoriumMod")  != null; //Hardcoded Thorium Mod boss support
			//modLoadedTremor      = ModLoader.GetMod("Tremor")      != null; //Hardcoded Tremor boss support
		}
		public static void ModSupportAntiaris() //Hardcoded Antiaris boss support
		{
			//Main.NewText("Pre-Antiaris HP/S: " + lifeRegenFromBosses/2f); //Debug stuff
			lifeRegenFromBossesTemp+= //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
			((Antiaris.AntiarisWorld.DownedAntlionQueen ? 1f : 0f) + //Antlion Queen
			(Antiaris.AntiarisWorld.DownedTowerKeeper   ? 1f : 0f))  //Tower Keeper
			*lifeRegenFromBosses_perBoss;
		}
		public static void ModSupportCalamityMod() //Hardcoded Calamity Mod boss support
		{
			//Main.NewText("Pre-Calamity HP/S: " + lifeRegenFromBossesTemp/2f); //Debug stuff
			lifeRegenFromBossesTemp+= //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
			((CalamityMod.CalamityWorld.downedDesertScourge     ? 1f : 0f) + //Desert Scourge
			(CalamityMod.CalamityWorld.downedCrabulon           ? 1f : 0f) + //Crabulon
			((CalamityMod.CalamityWorld.downedHiveMind ||                    //Hive Mind and/or Perforator
			CalamityMod.CalamityWorld.downedPerforator)         ? 1f : 0f) + //Hive Mind and/or Perforator
			(CalamityMod.CalamityWorld.downedSlimeGod           ? 1f : 0f) + //Slime God
			(CalamityMod.CalamityWorld.downedBrimstoneElemental ? 1f : 0f) + //Brimstone Elemental
			(CalamityMod.CalamityWorld.downedCryogen            ? 1f : 0f) + //Cryogen
			(CalamityMod.CalamityWorld.downedAquaticScourge     ? 1f : 0f) + //Aquatic Scourge
			(CalamityMod.CalamityWorld.downedCalamitas          ? 1f : 0f) + //Calamitas
			(CalamityMod.CalamityWorld.downedLeviathan          ? 1f : 0f) + //Leviathan
			(CalamityMod.CalamityWorld.downedAstrageldon        ? 1f : 0f) + //Astrageldon Slime
			//(CalamityMod.CalamityWorld.downedAstrumDeus???      ? 1f : 0f) + //Astrum Deus
			(CalamityMod.CalamityWorld.downedPlaguebringer      ? 1f : 0f) + //Plaguebringer Goliath
			//(CalamityMod.CalamityWorld.downedRavager???         ? 1f : 0f) + //Ravager
			(CalamityMod.CalamityWorld.downedGuardians          ? 1f : 0f) + //Profaned Guardians
			(CalamityMod.CalamityWorld.downedProvidence         ? 1f : 0f) + //Providence
			//(CalamityMod.CalamityWorld.downedCeaselessVoid???   ? 1f : 0f) + //Ceaseless Void
			//(CalamityMod.CalamityWorld.downedStormWorm???       ? 1f : 0f) + //Storm Weaver
			//(CalamityMod.CalamityWorld.downedSignus???          ? 1f : 0f) + //Signus
			(CalamityMod.CalamityWorld.downedPolterghast        ? 1f : 0f) + //Polterghast
			(CalamityMod.CalamityWorld.downedDoG                ? 1f : 0f) + //Devourer of Gods
			(CalamityMod.CalamityWorld.downedBumble             ? 1f : 0f) + //Bumblebirb
			(CalamityMod.CalamityWorld.downedYharon             ? 1f : 0f))  //Yharon
			//(CalamityMod.CalamityWorld.downedSupremeCalamitas??? ? 1f : 0f))  //Supreme Calamitas
			*lifeRegenFromBosses_perBoss;
		}
		public static void ModSupportOcram() //Hardcoded Ocram 'n Stuff boss support
		{
			//Main.NewText("Pre-Ocram HP/S: " + lifeRegenFromBosses/2f); //Debug stuff
			lifeRegenFromBossesTemp+= //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
			(Ocram.OcramWorld.downedOcram ? 1f : 0f) //Ocram
			*lifeRegenFromBosses_perBoss;
		}
		public static void ModSupportThoriumMod() //Hardcoded Thorium Mod boss support
		{
			//Main.NewText("Pre-Thorium HP/S: " + lifeRegenFromBosses/2f); //Debug stuff
			lifeRegenFromBossesTemp+= //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
			((ThoriumMod.ThoriumWorld.downedThunderBird   ? 1f : 0f) + //The Grand Thunder Bird
			(ThoriumMod.ThoriumWorld.downedJelly          ? 1f : 0f) + //The Queen Jellyfish
			//(ThoriumMod.ThoriumWorld.downedViscount???    ? 1f : 0f) + //Viscount
			(ThoriumMod.ThoriumWorld.downedStorm          ? 1f : 0f) + //Granite Energy Storm
			(ThoriumMod.ThoriumWorld.downedChampion       ? 1f : 0f) + //The Buried Champion
			(ThoriumMod.ThoriumWorld.downedScout          ? 1f : 0f) + //The Star Scouter
			(ThoriumMod.ThoriumWorld.downedStrider        ? 1f : 0f) + //Borean Strider
			(ThoriumMod.ThoriumWorld.downedFallenBeholder ? 1f : 0f) + //Coznix, the Fallen Beholder
			(ThoriumMod.ThoriumWorld.downedLich           ? 1f : 0f) + //The Lich
			(ThoriumMod.ThoriumWorld.downedDepthBoss      ? 1f : 0f) + //Abyssion, The Forgotten One
			(ThoriumMod.ThoriumWorld.downedRealityBreaker ? 1f : 0f))  //The Ragnarok
			*lifeRegenFromBosses_perBoss;
		}
		//Tremor doesn't want to cooperate for some reason
		/*public static void ModSupportTremor() //Hardcoded Tremor boss support
		{
			//Main.NewText("Pre-Tremor HP/S: " + lifeRegenFromBosses/2f); //Debug stuff
			lifeRegenFromBossesTemp+= //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
			((Tremor.TremorWorld.Boss.Rukh.IsDowned()          ? 1f : 0f) + //Rukh
			(Tremor.TremorWorld.Boss.TikiTotem.IsDowned()      ? 1f : 0f) + //Tiki Totem
			(Tremor.TremorWorld.Boss.EvilCorn.IsDowned()       ? 1f : 0f) + //Evil Corn
			(Tremor.TremorWorld.Boss.StormJellyfish.IsDowned() ? 1f : 0f) + //Storm Jellyfish
			(Tremor.TremorWorld.Boss.AncientDragon.IsDowned()  ? 1f : 0f) + //Ancient Dragon
			(Tremor.TremorWorld.Boss.FungusBeetle.IsDowned()   ? 1f : 0f) + //Fungus Beetle
			(Tremor.TremorWorld.Boss.HeaterofWorlds.IsDowned() ? 1f : 0f) + //Heater of Worlds
			(Tremor.TremorWorld.Boss.Alchemaster.IsDowned()    ? 1f : 0f) + //Alchemaster
			(Tremor.TremorWorld.Boss.Motherboard.IsDowned()    ? 1f : 0f) + //Motherboard
			(Tremor.TremorWorld.Boss.PixieQueen.IsDowned()     ? 1f : 0f) + //Pixie Queen
			(Tremor.TremorWorld.Boss.WallOfShadow.IsDowned()   ? 1f : 0f) + //Wall of Shadows
			(Tremor.TremorWorld.Boss.FrostKing.IsDowned()      ? 1f : 0f) + //Frost King
			(Tremor.TremorWorld.Boss.CogLord.IsDowned()        ? 1f : 0f) + //Cog Lord
			(Tremor.TremorWorld.Boss.CyberKing.IsDowned()      ? 1f : 0f) + //Mothership and Cyber King
			(Tremor.TremorWorld.Boss.DarkEmperor.IsDowned()    ? 1f : 0f) + //The Dark Emperor
			(Tremor.TremorWorld.Boss.Brutallisk.IsDowned()     ? 1f : 0f) + //Brutallisk
			(Tremor.TremorWorld.Boss.SpaceWhale.IsDowned()     ? 1f : 0f) + //Space Whale
			(Tremor.TremorWorld.Boss.Trinity.IsDowned()        ? 1f : 0f) + //The Trinity
			(Tremor.TremorWorld.Boss.Andas.IsDowned()          ? 1f : 0f))  //Andas
			*lifeRegenFromBosses_perBoss;
		}*/
	}
	public class RegenFromBosses_Player : ModPlayer //Hook into stuff for every player
	{
		public override void UpdateLifeRegen() //Regenerate some life and mana!
		{
			//Crude check to see if there's something that should prevent life regeneration
			if (!(player.lifeRegen<0 || player.bleed || player.burned || player.electrified || player.onFire || player.onFire2 || player.onFrostBurn || player.poisoned || player.suffocating || player.venom || (Main.expertMode && player.tongued)))
				player.lifeRegen += RegenFromBosses.lifeRegenFromBosses;
			//if (player.manaRegen>=0)
				//player.manaRegen += RegenFromBosses.manaRegenFromBosses;
		}
		public override void OnEnterWorld(Player player) //When a player spawns in a world...
		{
			RegenFromBosses.CalculateRegen(); //...recalculate the regeneration (so that it matches the given world's defeated bosses/mini-bosses/events)
		}
		public override void OnRespawn(Player player) //When a player dies and respawns...
		{
			RegenFromBosses.CalculateRegen(); //...recalculate the regeneration (just as an extra measure to keep the regeneration updated without being too taxing)
		}
	}
	public class RegenFromBosses_NPC : GlobalNPC //Hook into stuff for all NPCs
	{
		public override void NPCLoot(NPC npc) //When a NPC dies...
		{
			if (npc.boss) //...if the NPC is an enemy boss...
				RegenFromBosses.CalculateRegen(); //...recalculate the regeneration
		}
	}
}
