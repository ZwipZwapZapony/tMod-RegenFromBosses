using Terraria;
using Terraria.ModLoader;

//This file handles hardcoded support for mods' bosses
//If Boss Checklist is loaded, nothing in this file will do anything important (except not yet)

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		//Below are some variables that are false if a given mod is not loaded, or true if it is
		static bool modLoadedExampleMod;          //Example Mod
		static bool modLoadedAntiaris;            //The Antiaris
		static bool modLoadedCalamityMod;         //Calamity Mod (No Calamity Music)
		static bool modLoadedCrystiliumMod;       //Crystilium
		static bool modLoadedEchoesoftheAncients; //Echoes of the Ancients
		static bool modLoadedExodus;              //Exodus Mod
		static bool modLoadedOcram;               //Ocram 'n Stuff
		static bool modLoadedPumpking;            //Pumpking's Mod
		static bool modLoadedRedemption;          //Mod of Redemption
		static bool modLoadedSpiritMod;           //Spirit Mod
		static bool modLoadedThoriumMod;          //Thorium Mod
		static bool modLoadedTremor;              //Tremor Mod Remastered
		static bool modLoadedVaria;               //Varia (Open Beta)
		static bool modLoadedW1KModRedux;         //W1K's Mod Redux

		public static void Load__HardcodedModSupport() //This is run when the mod is loaded, if Boss Checklist is not loaded (except not yet) - Run by Load() in the RegenFromBosses.cs file
		{
			modLoadedExampleMod          = ModLoader.GetMod("ExampleMod")          != null; //Set the "Example Mod is loaded" variable to whether or not Example Mod is loaded
			modLoadedAntiaris            = ModLoader.GetMod("Antiaris")            != null;
			modLoadedCalamityMod         = ModLoader.GetMod("CalamityMod")         != null;
			modLoadedCrystiliumMod       = ModLoader.GetMod("CrystiliumMod")       != null;
			modLoadedEchoesoftheAncients = ModLoader.GetMod("EchoesoftheAncients") != null;
			modLoadedExodus              = ModLoader.GetMod("Exodus")              != null;
			modLoadedOcram               = ModLoader.GetMod("Ocram")               != null;
			modLoadedPumpking            = ModLoader.GetMod("Pumpking")            != null;
			modLoadedRedemption          = ModLoader.GetMod("Redemption")          != null;
			modLoadedSpiritMod           = ModLoader.GetMod("SpiritMod")           != null;
			modLoadedThoriumMod          = ModLoader.GetMod("ThoriumMod")          != null;
			modLoadedTremor              = ModLoader.GetMod("Tremor")              != null;
			modLoadedVaria               = ModLoader.GetMod("Varia")               != null;
			modLoadedW1KModRedux         = ModLoader.GetMod("W1KModRedux")         != null;
		}

		//The below method(/"function") handles hardcoded mod boss support, which is used if Boss Checklist isn't loaded
		//(Except that this mod can't load Boss Checklist correctly yet, so all mod boss supported is hardcoded in any case)
		public static void HardcodedModSupport()
		{
			if (modLoadedExampleMod) //If Example Mod is loaded...
				ModSupportExampleMod(); //...do some Example Mod boss check stuff, in a separate method to avoid crashes for people who don't have Example Mod loaded
			if (modLoadedAntiaris)
				ModSupportAntiaris();
			if (modLoadedCalamityMod)
				ModSupportCalamityMod();
			if (modLoadedCrystiliumMod)
				ModSupportCrystiliumMod();
			if (modLoadedEchoesoftheAncients)
				ModSupportEchoesoftheAncients();
			if (modLoadedExodus)
				ModSupportExodus();
			if (modLoadedOcram)
				ModSupportOcram();
			if (modLoadedPumpking)
				ModSupportPumpking();
			if (modLoadedRedemption)
				ModSupportRedemption();
			if (modLoadedSpiritMod)
				ModSupportSpiritMod();
			if (modLoadedThoriumMod)
				ModSupportThoriumMod();
			if (modLoadedTremor)
				ModSupportTremor();
			if (modLoadedVaria)
				ModSupportVaria();
			if (modLoadedW1KModRedux)
				ModSupportW1KModRedux();
		}

		public static void ModSupportExampleMod() //Example Mod
		{
			//Let's check which bosses have been slain, and increase the amount of slain bosses appropriately
			tempSlainBosses+=( //The "? 1 : 0" parts below basically convert bools to ints, as you can't add bools together
			(ExampleMod.ExampleWorld.downedAbomination  ? 1 : 0) + //Abomination
			(ExampleMod.ExampleWorld.downedPuritySpirit ? 1 : 0)   //Purity Spirit
			);
			tempCountBosses+=2; //Example Mod has 2 bosses, so increase the total boss count by 2
		}

		public static void ModSupportAntiaris() //The Antiaris
		{
			//Bosses
			tempSlainBosses+=(
			(Antiaris.AntiarisWorld.DownedAntlionQueen ? 1 : 0) + //Antlion Queen
			(Antiaris.AntiarisWorld.DownedTowerKeeper  ? 1 : 0)   //Tower Keeper
			);
			tempCountBosses+=2;
		}

		public static void ModSupportCalamityMod() //Calamity Mod
		{
			//Bosses
			tempSlainBosses+=(
			(CalamityMod.World.CalamityWorld.downedDesertScourge      ? 1 : 0) + //Desert Scourge
			(CalamityMod.World.CalamityWorld.downedCrabulon           ? 1 : 0) + //Crabulon
			((CalamityMod.World.CalamityWorld.downedHiveMind ||                  //Hive Mind / Perforator
			CalamityMod.World.CalamityWorld.downedPerforator)         ? 1 : 0) + //Hive Mind / Perforator
			(CalamityMod.World.CalamityWorld.downedSlimeGod           ? 1 : 0) + //Slime God
			(CalamityMod.World.CalamityWorld.downedCryogen            ? 1 : 0) + //Cryogen
			(CalamityMod.World.CalamityWorld.downedBrimstoneElemental ? 1 : 0) + //Brimstone Elemental
			(CalamityMod.World.CalamityWorld.downedAquaticScourge     ? 1 : 0) + //Aquatic Scourge
			(CalamityMod.World.CalamityWorld.downedCalamitas          ? 1 : 0) + //Calamitas
			(CalamityMod.World.CalamityWorld.downedLeviathan          ? 1 : 0) + //Leviathan
			(CalamityMod.World.CalamityWorld.downedAstrageldon        ? 1 : 0) + //Astrum Aureus
			(CalamityMod.World.CalamityWorld.downedStarGod            ? 1 : 0) + //Astrum Deus
			(CalamityMod.World.CalamityWorld.downedPlaguebringer      ? 1 : 0) + //Plaguebringer Goliath
			(CalamityMod.World.CalamityWorld.downedScavenger          ? 1 : 0) + //Ravager
			(CalamityMod.World.CalamityWorld.downedGuardians          ? 1 : 0) + //Profaned Guardians
			(CalamityMod.World.CalamityWorld.downedProvidence         ? 1 : 0) + //Providence
			(CalamityMod.World.CalamityWorld.downedSentinel1          ? 1 : 0) + //Ceaseless Void
			(CalamityMod.World.CalamityWorld.downedSentinel2          ? 1 : 0) + //Storm Weaver
			(CalamityMod.World.CalamityWorld.downedSentinel3          ? 1 : 0) + //Signus
			(CalamityMod.World.CalamityWorld.downedPolterghast        ? 1 : 0) + //Polterghast
			(CalamityMod.World.CalamityWorld.downedDoG                ? 1 : 0) + //Devourer of Gods
			(CalamityMod.World.CalamityWorld.downedBumble             ? 1 : 0) + //Bumblebirb
			(CalamityMod.World.CalamityWorld.downedYharon             ? 1 : 0) + //Yharon
			(CalamityMod.World.CalamityWorld.downedSCal               ? 1 : 0)   //Supreme Calamitas
			);
			tempCountBosses+=23;
		}

		public static void ModSupportCrystiliumMod() //Crystilium
		{
			//Bosses
			tempSlainBosses+=(
			(CrystiliumMod.CrystalWorld.downedCrystalKing ? 1 : 0) //Crystal King
			);
			tempCountBosses+=1;
		}

		public static void ModSupportEchoesoftheAncients() //Echoes of the Ancients
		{
			//Bosses
			tempSlainBosses+=(
			(EchoesoftheAncients.AncientWorld.downedWyrms ? 1 : 0) //Creation and Destruction
			);
			tempCountBosses+=1;
		}

		public static void ModSupportExodus() //Exodus Mod
		{
			//Bosses
			tempSlainBosses+=(
			(Exodus.ExodusWorld.downedExodusAbomination   ? 1 : 0) + //Abomination
			(Exodus.ExodusWorld.downedExodusEvilBlob      ? 1 : 0) + //Evil Blob
			(Exodus.ExodusWorld.downedExodusColossus      ? 1 : 0) + //Colossus
			(Exodus.ExodusWorld.downedExodusDesertEmperor ? 1 : 0) + //Desert Emperor
			(Exodus.ExodusWorld.downedExodusHivemind      ? 1 : 0) + //Mindflayer
			(Exodus.ExodusWorld.downedExodusMaster        ? 1 : 0) + //Master of Possession
			(Exodus.ExodusWorld.downedExodusSludgeheart   ? 1 : 0) + //Sludgeheart
			(Exodus.ExodusWorld.downedExodusTheAncient    ? 1 : 0)   //The Ancient
			);
			tempCountBosses+=8;
		}

		public static void ModSupportOcram() //Ocram 'n Stuff
		{
			//Bosses
			tempSlainBosses+=(
			(Ocram.OcramWorld.downedOcram ? 1 : 0) //Ocram
			);
			tempCountBosses+=1;
		}

		public static void ModSupportPumpking() //Pumpking's Mod
		{
			//Bosses
			tempSlainBosses+=(
			(Pumpking.PumpkingWorld.downedPumpkingHorseman ? 1 : 0) + //Pumpking Horseman
			(Pumpking.PumpkingWorld.downedTerraLord        ? 1 : 0)   //Terra Lord
			);
			tempCountBosses+=2;
		}

		public static void ModSupportRedemption() //Mod of Redemption
		{
			//Bosses
			tempSlainBosses+=(
			(Redemption.RedeWorld.downedThorn             ? 1 : 0) + //Thorn, Bane of the Forest
			(Redemption.RedeWorld.downedTheKeeper         ? 1 : 0) + //The Keeper
			(Redemption.RedeWorld.downedXenomiteCrystal   ? 1 : 0) + //Xenomite Crystal
			(Redemption.RedeWorld.downedInfectedEye       ? 1 : 0) + //Infected Eye
			(Redemption.RedeWorld.downedIBehemoth         ? 1 : 0) + //The Abandoned Lab
			(Redemption.RedeWorld.downedSlayer            ? 1 : 0) + //King Slayer III
			(Redemption.RedeWorld.downedVlitch1           ? 1 : 0) + //1st Vlitch Overlord
			(Redemption.RedeWorld.downedVlitch2           ? 1 : 0) + //2nd Vlitch Overlord
			(Redemption.RedeWorld.downedVlitch3           ? 1 : 0) + //3rd Vlitch Overlord
			(Redemption.RedeWorld.downedPatientZero       ? 1 : 0) + //Patient Zero
			(Redemption.RedeWorld.downedEaglecrestGolemPZ ? 1 : 0) + //Thorn & Eaglecrest Re[?]
			(Redemption.RedeWorld.downedNebuleus          ? 1 : 0)   //Nebuleus, Angel of the Cosmos
			);
			tempCountBosses+=12;
			//Mini-Bosses
			tempSlainMiniBosses+=(
			(Redemption.RedeWorld.downedKingChicken     ? 1 : 0) + //The Mighty King Chicken
			(Redemption.RedeWorld.downedSunkenCaptain   ? 1 : 0) + //Sunken Captain
			(Redemption.RedeWorld.downedSkullDigger     ? 1 : 0) + //Skull Digger
			(Redemption.RedeWorld.downedStrangePortal   ? 1 : 0) + //Strange Portal
			(Redemption.RedeWorld.downedEaglecrestGolem ? 1 : 0)   //Eaglecrest Golem
			);
			tempCountMiniBosses+=3;
			//Events
			tempSlainEvents+=(
			(Redemption.RedeWorld.downedChickenInvPZ ? 1 : 0) + //Chicken Invasion
			(Redemption.RedeWorld.downedChickenInvPZ ? 1 : 0)   //King Chicken's Royal A[?]
			);
			tempCountEvents+=2;
		}

		public static void ModSupportSpiritMod() //Spirit Mod
		{
			//Bosses
			tempSlainBosses+=(
			(SpiritMod.MyWorld.downedScarabeus        ? 1 : 0) + //Scarabeus
			(SpiritMod.MyWorld.downedReachBoss        ? 1 : 0) + //Vinewrath Bane
			(SpiritMod.MyWorld.downedAncientFlier     ? 1 : 0) + //Ancient Flier
			(SpiritMod.MyWorld.downedRaider           ? 1 : 0) + //Starplate Raider
			(SpiritMod.MyWorld.downedInfernon         ? 1 : 0) + //Infernon
			(SpiritMod.MyWorld.downedDusking          ? 1 : 0) + //Dusking
			(SpiritMod.MyWorld.downedSpiritCore       ? 1 : 0) + //Ethereal Umbra
			(SpiritMod.MyWorld.downedIlluminantMaster ? 1 : 0) + //Illuminant Master
			(SpiritMod.MyWorld.downedAtlas            ? 1 : 0) + //Atlas
			(SpiritMod.MyWorld.downedOverseer         ? 1 : 0)   //Overseer
			);
			tempCountBosses+=10;
		}

		public static void ModSupportThoriumMod() //Thorium Mod
		{
			//Bosses
			tempSlainBosses+=(
			(ThoriumMod.ThoriumWorld.downedThunderBird    ? 1 : 0) + //The Grand Thunder Bird
			(ThoriumMod.ThoriumWorld.downedJelly          ? 1 : 0) + //The Queen Jellyfish
			(ThoriumMod.ThoriumWorld.downedBat            ? 1 : 0) + //Viscount
			(ThoriumMod.ThoriumWorld.downedStorm          ? 1 : 0) + //Granite Energy Storm
			(ThoriumMod.ThoriumWorld.downedChampion       ? 1 : 0) + //The Buried Champion
			(ThoriumMod.ThoriumWorld.downedScout          ? 1 : 0) + //The Star Scouter
			(ThoriumMod.ThoriumWorld.downedStrider        ? 1 : 0) + //Borean Strider
			(ThoriumMod.ThoriumWorld.downedFallenBeholder ? 1 : 0) + //Coznix, the Fallen Beholder
			(ThoriumMod.ThoriumWorld.downedLich           ? 1 : 0) + //The Lich
			(ThoriumMod.ThoriumWorld.downedDepthBoss      ? 1 : 0) + //Abyssion, The Forgotten One
			(ThoriumMod.ThoriumWorld.downedRealityBreaker ? 1 : 0)   //The Ragnarok
			);
			tempCountBosses+=11;
		}

		public static void ModSupportTremor() //Tremor Mod Remastered
		{
			//Bosses
			tempSlainBosses+=(
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.Rukh]           ? 1 : 0) + //Rukh
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.TikiTotem]      ? 1 : 0) + //Tiki Totem
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.EvilCorn]       ? 1 : 0) + //Evil Corn
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.StormJellyfish] ? 1 : 0) + //Storm Jellyfish
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.AncientDragon]  ? 1 : 0) + //Ancient Dragon
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.HeaterofWorlds] ? 1 : 0) + //Heater of Worlds
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.FungusBeetle]   ? 1 : 0) + //Fungus Beetle
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.Alchemaster]    ? 1 : 0) + //Alchemaster
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.Motherboard]    ? 1 : 0) + //Motherboard (Destroyer alt)
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.PixieQueen]     ? 1 : 0) + //Pixie Queen
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.FrostKing]      ? 1 : 0) + //Frost King
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.WallOfShadow]   ? 1 : 0) + //Wall of Shadows
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.CogLord]        ? 1 : 0) + //Cog Lord
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.CyberKing]      ? 1 : 0) + //Mothership and Cyber King
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.DarkEmperor]    ? 1 : 0) + //The Dark Emperor
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.Brutallisk]     ? 1 : 0) + //Brutallisk
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.SpaceWhale]     ? 1 : 0) + //Space Whale
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.Trinity]        ? 1 : 0) + //The Trinity
			(Tremor.TremorWorld.downedBoss[Tremor.TremorWorld.Boss.Andas]          ? 1 : 0)   //Andas
			);
			tempCountBosses+=19;
		}

		public static void ModSupportVaria() //Varia (Open Beta)
		{
			//Bosses
			tempSlainBosses+=(
			(Varia.VariaWorld.downedSotG        ? 1 : 0) + //Soul of the Guide
			(Varia.VariaWorld.downedSpoderQueen ? 1 : 0) + //Spider Queen
			(Varia.VariaWorld.downedAngel       ? 1 : 0) + //Fallen Angel
			(Varia.VariaWorld.downedOptime      ? 1 : 0) + //Nice Guy
			(Varia.VariaWorld.downedAnomaly     ? 1 : 0)   //The Anomaly
			);
			tempCountBosses+=5;
		}

		public static void ModSupportW1KModRedux() //W1K's Mod Redux
		{
			//Bosses
			tempSlainBosses+=(
			(W1KModRedux.MWorld.downedKutKu    ? 1 : 0) + //Yian Kut-Ku
			(W1KModRedux.MWorld.downedIvy      ? 1 : 0) + //Ivy Plant
			(W1KModRedux.MWorld.downedArdorix  ? 1 : 0) + //Ardorix
			(W1KModRedux.MWorld.downedArborix  ? 1 : 0) + //Arborix
			(W1KModRedux.MWorld.downedAquatix  ? 1 : 0) + //Aquatix
			(W1KModRedux.MWorld.downedRidley   ? 1 : 0) + //Ridley
			(W1KModRedux.MWorld.downedRathalos ? 1 : 0) + //Rathalos
			(W1KModRedux.MWorld.downedOkiku    ? 1 : 0) + //Okiku
			(W1KModRedux.MWorld.downedDeath    ? 1 : 0)   //Death
			);
			tempCountBosses+=9;
		}
	}
}