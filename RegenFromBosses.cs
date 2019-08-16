using Terraria.ModLoader;

//This file is for the "main" stuff as well as stuff not handled in other files
//It defines some of the important variables related to life regeneration

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		public RegenFromBosses()
		{
		}
		public static int regenLife=0; //How much life is regenerated per second from slain bosses/mini-bosses/events - Half of this is regenerated per second
		public static int regenMana=0; //Same, for mana

		public static int tempSlainBosses; //Amount of bosses currently slain
		public static int tempCountBosses; //Amount of bosses in total, both slain and un-slain
		public static int tempSlainMiniBosses; //Slain mini-bosses
		public static int tempCountMiniBosses; //Amount of mini-bosses
		public static int tempSlainEvents; //Slain events
		public static int tempCountEvents; //Amount of events

		public static bool calculateRegen=false; //Whether or not to re-calculate the regeneration amount this frame


		//static bool modLoadedBossChecklist;  //Automatic high mod compatibility using Boss Checklist (except not yet)
		static bool modLoadedModdersToolkit; //Display some debug text if Modder's Toolkit is loaded

		public override void Load() //This gets run when the mod is first loaded
		{
			//modLoadedBossChecklist =ModLoader.GetMod("BossChecklist") !=null; //Automatic high mod compatibility using Boss Checklist (except not yet)
			modLoadedModdersToolkit=ModLoader.GetMod("ModdersToolkit")!=null; //Display some debug text if Modder's Toolkit is loaded
			//if (!modLoadedBossChecklist) //If Boss Checklist is not loaded
				Load__HardcodedModSupport(); //Load other hardcoded mods - See the RegenFromBosses_ModSupport.cs file
		}

		public override object Call(params object[] input) //Allow other mods to manually make this mod recalculate the regeneration amount with a Call
		{
			if (input[0]=="CalculateRegen")
			{
				calculateRegen=true;
				return true;
			}
			else
				return false;
			//This doesn't need to be done after slaying a NPC with the "boss" variable set to true, in which case this happens automatically
			//But in other cases (such as after clearing an event), I think that the Call usage for other mods is as follows:
			//Mod RFB = ModLoader.GetMod("RegenFromBosses");
			//if (RFB != null)
			//	RFB.Call("CalculateRegen");
		}
	}
}