using System.Linq; //For C#6 compiling
using Terraria;
using Terraria.ModLoader;

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		//static bool modLoadedBossChecklist; //Automatic high mod compatibility using Boss Checklist (except not yet)
		//static int  modBossChecklist;       //Handle for the Boss Checklist mod (except not yet)
		//Below is a lot of hardcoded mod boss support
		static bool modLoadedExampleMod;          //Example Mod
		static bool modLoadedAntiaris;            //The Antiaris
		static bool modLoadedCalamityMod;         //Calamity Mod
		static bool modLoadedCrystiliumMod;       //Crystilium
		static bool modLoadedEchoesoftheAncients; //Echoes of the Ancients
		static bool modLoadedExodus;              //Exodus Mod
		static bool modLoadedOcram;               //Ocram 'n Stuff
		static bool modLoadedPumpking;            //Pumpking's Mod
		static bool modLoadedRedemption;          //Mod of Redemption
		static bool modLoadedSpiritMod;           //Spirit Mod
		static bool modLoadedThoriumMod;          //Thorium Mod
		//static bool modLoadedTremor;              //Tremor Mod Remastered
		static bool modLoadedW1KModRedux;         //W1K's Mod Redux

		public override void Load()
		{
			//modBossChecklist       = ModLoader.GetMod("BossChecklist"); //Handle for the Boss Checklist mod (except not yet)
			//modLoadedBossChecklist = modBossChecklist != null;          //Automatic high mod compatibility using Boss Checklist (except not yet)
			modLoadedExampleMod          = ModLoader.GetMod("ExampleMod")          != null; //Check if Example Mod is loaded, for hardcoded Example Mod boss compatibility if Boss Checklist isn't
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
			//modLoadedTremor              = ModLoader.GetMod("Tremor")            != null;
			modLoadedW1KModRedux         = ModLoader.GetMod("W1KModRedux")         != null;
		}

		//The below function handles hardcoded mod boss support, which is used if Boss Checklist isn't loaded
		//(Except that this mod can't load Boss Checklist correctly yet, so all mod boss supported is hardcoded in any case)
		public static void HardcodedModSupport()
		{
			if (modLoadedExampleMod) //If Example Mod is loaded...
				ModSupportExampleMod(); //...do some Example Mod boss check stuff, in a separate function to avoid crashes for people who don't have Example Mod loaded
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
			//if (modLoadedTremor)
				//ModSupportTremor();
			if (modLoadedW1KModRedux)
				ModSupportW1KModRedux();
		}

		public static void ModSupportExampleMod() //Hardcoded Example Mod boss support
		{
			//Main.NewText("Pre-Example Mod HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			//Let's check which bosses have been slain, and let's increase the regeneration amount appropriately
			lifeRegenFromBosses_tempCalc+=( //The "? 1f : 0f" parts below basically convert bools to floats, as you can't add bools together
			(ExampleMod.ExampleWorld.downedAbomination  ? 1f : 0f) + //Abomination
			(ExampleMod.ExampleWorld.downedPuritySpirit ? 1f : 0f)   //Purity Spirit
			)*lifeRegenFromBosses_perBoss; //Make sure to multiply it by however much health per second you should get per boss
		}

		public static void ModSupportAntiaris() //Hardcoded Antiaris boss support
		{
			//Main.NewText("Pre-Antiaris HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(Antiaris.AntiarisWorld.DownedAntlionQueen ? 1f : 0f) + //Antlion Queen
			(Antiaris.AntiarisWorld.DownedTowerKeeper  ? 1f : 0f)   //Tower Keeper
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportCalamityMod() //Hardcoded Calamity Mod boss support
		{
			//Main.NewText("Pre-Calamity HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(CalamityMod.CalamityWorld.downedDesertScourge      ? 1f : 0f) + //Desert Scourge
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
			(CalamityMod.CalamityWorld.downedYharon             ? 1f : 0f)   //Yharon
			//(CalamityMod.CalamityWorld.downedSupremeCalamitas??? ? 1f : 0f)  //Supreme Calamitas
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportCrystiliumMod() //Hardcoded Crystilium boss support
		{
			//Main.NewText("Pre-Crystilium HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(CrystiliumMod.CrystalWorld.downedCrystalKing ? 1f : 0f) //Crystal King
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportEchoesoftheAncients() //Hardcoded Echoes of the Ancients boss support
		{
			//Main.NewText("Pre-EotA HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(EchoesoftheAncients.AncientWorld.downedWyrms ? 1f : 0f) //Creation and Destruction
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportExodus() //Hardcoded Exodus Mod boss support
		{
			//Main.NewText("Pre-Exodus HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(Exodus.ExodusWorld.downedExodusAbomination   ? 1f : 0f) + //Abomination
			(Exodus.ExodusWorld.downedExodusEvilBlob      ? 1f : 0f) + //Evil Blob
			(Exodus.ExodusWorld.downedExodusColossus      ? 1f : 0f) + //Colossus
			(Exodus.ExodusWorld.downedExodusDesertEmperor ? 1f : 0f) + //Desert Emperor
			(Exodus.ExodusWorld.downedExodusHivemind      ? 1f : 0f) + //Mindflayer
			(Exodus.ExodusWorld.downedExodusMaster        ? 1f : 0f) + //Master of Possession
			(Exodus.ExodusWorld.downedExodusSludgeheart   ? 1f : 0f) + //Sludgeheart
			(Exodus.ExodusWorld.downedExodusTheAncient    ? 1f : 0f)   //The Ancient
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportOcram() //Hardcoded Ocram 'n Stuff boss support
		{
			//Main.NewText("Pre-Ocram HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(Ocram.OcramWorld.downedOcram ? 1f : 0f) //Ocram
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportPumpking() //Hardcoded Pumpking's Mod boss support
		{
			//Main.NewText("Pre-Pumpking HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(Pumpking.PumpkingWorld.downedPumpkingHorseman ? 1f : 0f) + //Pumpking Horseman
			(Pumpking.PumpkingWorld.downedTerraLord        ? 1f : 0f)   //Terra Lord
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportRedemption() //Hardcoded Mod of Redemption boss support
		{
			//Main.NewText("Pre-Redemption HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(Redemption.RedeWorld.downedTheKeeper       ? 1f : 0f) + //The Keeper
			(Redemption.RedeWorld.downedXenomiteCrystal ? 1f : 0f) + //Xenomite Crystal
			(Redemption.RedeWorld.downedInfectedEye     ? 1f : 0f) + //Infected Eye
			(Redemption.RedeWorld.downedSlayer          ? 1f : 0f) + //King Slayer III
			(Redemption.RedeWorld.downedVlitch1         ? 1f : 0f) + //Vlitch Cleaver
			(Redemption.RedeWorld.downedVlitch2         ? 1f : 0f)   //Vlitch Gigipede
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportSpiritMod() //Hardcoded Spirit Mod boss support
		{
			//Main.NewText("Pre-Spirit HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(SpiritMod.MyWorld.downedScarabeus        ? 1f : 0f) + //Scarabeus
			//(SpiritMod.MyWorld.downedVinewrathBane??? ? 1f : 0f) + //Vinewrath Bane
			(SpiritMod.MyWorld.downedAncientFlier     ? 1f : 0f) + //Ancient Flier
			(SpiritMod.MyWorld.downedRaider           ? 1f : 0f) + //Starplate Raider???
			(SpiritMod.MyWorld.downedInfernon         ? 1f : 0f) + //Infernon
			(SpiritMod.MyWorld.downedDusking          ? 1f : 0f) + //Dusking
			//(SpiritMod.MyWorld.downedEtherealUmbra??? ? 1f : 0f) + //Ethereal Umbra
			(SpiritMod.MyWorld.downedIlluminantMaster ? 1f : 0f) + //Illuminant Master
			(SpiritMod.MyWorld.downedAtlas            ? 1f : 0f) + //Atlas
			(SpiritMod.MyWorld.downedOverseer         ? 1f : 0f)   //Overseer
			)*lifeRegenFromBosses_perBoss;
		}

		public static void ModSupportThoriumMod() //Hardcoded Thorium Mod boss support
		{
			//Main.NewText("Pre-Thorium HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(ThoriumMod.ThoriumWorld.downedThunderBird    ? 1f : 0f) + //The Grand Thunder Bird
			(ThoriumMod.ThoriumWorld.downedJelly          ? 1f : 0f) + //The Queen Jellyfish
			//(ThoriumMod.ThoriumWorld.downedViscount???    ? 1f : 0f) + //Viscount
			(ThoriumMod.ThoriumWorld.downedStorm          ? 1f : 0f) + //Granite Energy Storm
			(ThoriumMod.ThoriumWorld.downedChampion       ? 1f : 0f) + //The Buried Champion
			(ThoriumMod.ThoriumWorld.downedScout          ? 1f : 0f) + //The Star Scouter
			(ThoriumMod.ThoriumWorld.downedStrider        ? 1f : 0f) + //Borean Strider
			(ThoriumMod.ThoriumWorld.downedFallenBeholder ? 1f : 0f) + //Coznix, the Fallen Beholder
			(ThoriumMod.ThoriumWorld.downedLich           ? 1f : 0f) + //The Lich
			(ThoriumMod.ThoriumWorld.downedDepthBoss      ? 1f : 0f) + //Abyssion, The Forgotten One
			(ThoriumMod.ThoriumWorld.downedRealityBreaker ? 1f : 0f)   //The Ragnarok
			)*lifeRegenFromBosses_perBoss;
		}

		//I don't know what the array values for each boss are, and I don't want to just guess, so this is commented out for now
		/*public static void ModSupportTremor() //Hardcoded Tremor boss support
		{
			//Main.NewText("Pre-Tremor HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Rukh
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Tiki Totem
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Evil Corn
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Storm Jellyfish
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Ancient Dragon
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Fungus Beetle
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Heater of Worlds
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Alchemaster
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Motherboard
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Pixie Queen
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Wall of Shadows
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Frost King
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Cog Lord
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Mothership and Cyber King
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //The Dark Emperor
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Brutallisk
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //Space Whale
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f) + //The Trinity
			(Tremor.TremorWorld.downedBoss[???] ? 1f : 0f)   //Andas
			)*lifeRegenFromBosses_perBoss;
		}*/

		public static void ModSupportW1KModRedux() //Hardcoded W1K's Mod Redux boss support
		{
			//Main.NewText("Pre-W1K HP/S: " + lifeRegenFromBosses_tempCalc/2f); //Debug stuff
			lifeRegenFromBosses_tempCalc+=(
			(W1KModRedux.MWorld.downedKutKu    ? 1f : 0f) + //Yian Kut-Ku
			(W1KModRedux.MWorld.downedIvy      ? 1f : 0f) + //Ivy Plant
			(W1KModRedux.MWorld.downedAquatix  ? 1f : 0f) + //Aquatix
			(W1KModRedux.MWorld.downedArborix  ? 1f : 0f) + //Arborix
			(W1KModRedux.MWorld.downedArdorix  ? 1f : 0f) + //Ardorix
			(W1KModRedux.MWorld.downedRidley   ? 1f : 0f) + //Ridley
			(W1KModRedux.MWorld.downedRathalos ? 1f : 0f) + //Rathalos
			(W1KModRedux.MWorld.downedOkiku    ? 1f : 0f) + //Okiku
			(W1KModRedux.MWorld.downedDeath    ? 1f : 0f)   //Death
			)*lifeRegenFromBosses_perBoss;
		}
	}
}
