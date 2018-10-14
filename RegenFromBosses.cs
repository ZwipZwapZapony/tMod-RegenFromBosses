using System.IO; //For configuration file reading/writing
using System.Linq; //For C#6 compiling?
using Terraria;
using Terraria.IO; //For configuration file reading/writing
using Terraria.ModLoader;

//This file is for the "main" stuff as well as stuff not handled in other files
//It defines some of the important variables related to life regeneration,
//and also handles loading the mod's configuration file

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		public RegenFromBosses()
		{
		}
		public static int regenLife=0; //How much life is regenerated per second from slain bosses - Half of this is regenerated per second

		public static int configLifeFromBosses=20; //The maximum health regeneration obtainable by slaying all bosses...
		//public static int configLifeFromMiniBosses=0; //...mini-bosses...
		//public static int configLifeFromEvents=0; //...and events
		static Preferences configFile=new Preferences(Path.Combine(Main.SavePath,"Mod Configs","RegenFromBosses.json")); //The configuration file handle thingie

		public static int tempSlainBosses; //Amount of bosses currently slain
		public static int tempCountBosses; //Amount of bosses in total, both slain and un-slain
		//public static int tempSlainMiniBosses; //Slain mini-bosses
		//public static int tempCountMiniBosses; //Amount of mini-bosses
		//public static int tempSlainEvents; //Slain mini-bosses
		//public static int tempCountEvents; //Amount of mini-bosses

		public static bool calculateRegen=false; //Whether or not to re-calculate the regeneration amount


		//static bool modLoadedBossChecklist;  //Automatic high mod compatibility using Boss Checklist (except not yet)
		static bool modLoadedModdersToolkit; //Display some debug text if Modder's Toolkit is loaded

		public override void Load() //This gets run when the mod is first loaded
		{
			//modLoadedBossChecklist  = ModLoader.GetMod("BossChecklist")  != null; //Automatic high mod compatibility using Boss Checklist (except not yet)
			modLoadedModdersToolkit = ModLoader.GetMod("ModdersToolkit") != null; //Display some debug text if Modder's Toolkit is loaded
			//if (!modLoadedBossChecklist) //If Boss Checklist is not loaded
				Load__HardcodedModSupport(); //Load other hardcoded mods - See the RegenFromBosses_ModSupport.cs file

			//Now, for all of the scary configuration file support
			if (configFile.Load()) //If the configuration file is loaded successfully
			{
				if (configFile.Contains("LifeFromBosses")) //If the life regeneration amount from bosses is in the configuration file...
					configFile.Get("LifeFromBosses",ref configLifeFromBosses); //...load it
				else
					configFile.Put("LifeFromBosses",20); //Otherwise, add it to the configuration file

				//if (configFile.Contains("LifeFromMiniBosses")) //Do the same for mini-bosses...
					//configFile.Get("LifeFromMiniBosses",ref configLifeFromMiniBosses);
				//else
					//configFile.Put("LifeFromMiniBosses",0);

				//if (configFile.Contains("LifeFromEvents")) //...and for events
					//configFile.Get("LifeFromEvents",ref configLifeFromEvents);
				//else
					//configFile.Put("LifeFromEvents",0);

				configFile.Save(); //Finally, save the configuration file with all things in it, in case that some of the above options don't already exist in it
			}
			else //If the configuration file didn't load successfully
			{
				configFile.Clear(); //Clear the configuration file, just in case...
				configFile.Put("LifeFromBosses",20); //...add variables for the the life regeneration from bosses...
				//configFile.Put("LifeFromMiniBosses",0); //...mini-bosses...
				//configFile.Put("LifeFromEvents",0); //...and events...
				configFile.Save(); //...and then save the default-values configuration file
			}
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
