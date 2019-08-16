using Newtonsoft.Json; //Needed for the "[JsonIgnore]" attribute for ModConfig support
using System.ComponentModel; //Needed for other attributes (e.g. "[DefaultValue(...)]") for ModConfig support
using Terraria;
using Terraria.ID; //For the NetmodeID constants
using Terraria.ModLoader;
using Terraria.ModLoader.Config; //Needed for ModConfig support

//This file handles ModConfig support
//It allows you to set the maximum amount of life regeneration from having beaten all bosses in the current world,
//and how much life regeneration from this mod stacks with other sources

namespace RegenFromBosses
{
	[Label("Configuration")] //Set the ModConfig page's header text
	public class Config_ServerSide : ModConfig
	{
		public override ConfigScope Mode=>ConfigScope.ServerSide; //These are server-side variables
		public static Config_ServerSide Instance; //Allow "shorthand" stuff like "Config_ServerSide.Instance.configLifeFromBosses__Halved"
		//public override void OnLoaded() //Until the next tModLoader update, this must be done manually...
		//{
			//Config_ServerSide.Instance=this; //...so, therefore, do it manually!
		//}

		[Header("Maximum Life Regeneration Per Second")] //Add a header

		[DefaultValue(10)] //Set the default to 10 life per second
		[Label("Maximum Life Regeneration from Bosses")] //Label the slider intuitively //TODO Figure out how to display the value divided by 2, then double the slider
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(230,10,57)] //Make the slider Heart-coloured
		[Tooltip("Default: 10\nThe amount of life regeneration per second\nwhen all bosses have been slain.\n\nScales linearly between 0 and this amount\nif only some of the bosses have been slain.")] //Set a descriptive tooltip
		public int configLifeFromBosses; //Add a slider for the amount of life per second from bosses

		[DefaultValue(0)] //Set the default to 0 life per second
		[Label("Maximum Life Regeneration from Mini-Bosses")] //Label the slider intuitively
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(230,10,57)] //Make the slider Heart-coloured
		[Tooltip("Default: 0\nThe amount of life regeneration per second\nwhen all mini-bosses have been slain.\n\nScales linearly between 0 and this amount\nif only some of the mini-bosses have been slain.")] //Set a descriptive tooltip
		public int configLifeFromMiniBosses; //Add a slider for the amount of life per second from mini-bosses

		[DefaultValue(0)] //Set the default to 0 life per second
		[Label("Maximum Life Regeneration from Events")] //Label the slider intuitively
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(230,10,57)] //Make the slider Heart-coloured
		[Tooltip("Default: 0\nThe amount of life regeneration per second\nwhen all events have been slain.\n\nScales linearly between 0 and this amount\nif only some of the events have been slain.")] //Set a descriptive tooltip
		public int configLifeFromEvents; //Add a slider for the amount of life per second from events

		[DefaultValue(0)] //Set the default to 10 life per second
		[Label("Additional Passive Flat Extra Life Regeneration")] //Label the slider intuitively //TODO Figure out how to display the value divided by 2, then double the slider
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(230,10,57)] //Make the slider Heart-coloured
		[Tooltip("Default: 0\nAn extra amount of life regeneration per second,\nwhich is always active, regardless of whether or not\nany bosses, mini-bosses, or events have been slain,\nand also regardless of how many have been slain if so.")] //Set a descriptive tooltip
		public int configLifePassive; //Add a slider for the amount of passive life per second

		[JsonIgnore] //Prevent the following from being saved in the configuration file
		[Label("Total Maximum Life Regeneration")] //Label the auto-value intuitively
		[Tooltip("Default: 10\nThe potential amount of life regeneration per second\nwhen all bosses, mini-bosses, and events have all been slain.")] //Set a descriptive tooltip
		public int config__MaxLifeRegen=>(configLifeFromBosses+configLifeFromMiniBosses+configLifeFromEvents+configLifePassive); //Add an auto-calculated value box

		[Header("Maximum Mana Regeneration Per Second")] //Add a header

		[DefaultValue(0)] //Set the default to 10 mana per second
		[Label("Maximum Mana Regeneration from Bosses")] //Label the slider intuitively //TODO Figure out how to display the value divided by 2, then double the slider
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(11,61,245)] //Make the slider Star-coloured
		[Tooltip("Default: 0\nThe amount of mana regeneration per second\nwhen all bosses have been slain.\n\nScales linearly between 0 and this amount\nif only some of the bosses have been slain.")] //Set a descriptive tooltip
		public int configManaFromBosses; //Add a slider for the amount of mana per second from bosses

		[DefaultValue(0)] //Set the default to 0 mana per second
		[Label("Maximum Mana Regeneration from Mini-Bosses")] //Label the slider intuitively
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(11,61,245)] //Make the slider Star-coloured
		[Tooltip("Default: 0\nThe amount of mana regeneration per second\nwhen all mini-bosses have been slain.\n\nScales linearly between 0 and this amount\nif only some of the mini-bosses have been slain.")] //Set a descriptive tooltip
		public int configManaFromMiniBosses; //Add a slider for the amount of mana per second from mini-bosses

		[DefaultValue(0)] //Set the default to 0 mana per second
		[Label("Maximum Mana Regeneration from Events")] //Label the slider intuitively
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(11,61,245)] //Make the slider Star-coloured
		[Tooltip("Default: 0\nThe amount of mana regeneration per second\nwhen all events have been slain.\n\nScales linearly between 0 and this amount\nif only some of the events have been slain.")] //Set a descriptive tooltip
		public int configManaFromEvents; //Add a slider for the amount of mana per second from events

		[DefaultValue(0)] //Set the default to 10 mana per second
		[Label("Additional Passive Flat Extra Mana Regeneration")] //Label the slider intuitively //TODO Figure out how to display the value divided by 2, then double the slider
		[Range(0,100)] //Limit the slider to "sane" values
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(11,61,245)] //Make the slider Star-coloured
		[Tooltip("Default: 0\nAn extra amount of mana regeneration per second,\nwhich is always active, regardless of whether or not\nany bosses, mini-bosses, or events have been slain,\nand also regardless of how many have been slain if so.")] //Set a descriptive tooltip
		public int configManaPassive; //Add a slider for the amount of passive mana per second

		[JsonIgnore] //Prevent the following from being saved in the configuration file
		[Label("Total Maximum Mana Regeneration")] //Label the auto-value intuitively
		[Tooltip("Default: 0\nThe potential amount of mana regeneration per second\nwhen all bosses, mini-bosses, and events have all been slain.")] //Set a descriptive tooltip
		public int config__MaxManaRegen=>(configManaFromBosses+configManaFromMiniBosses+configManaFromEvents+configManaPassive); //Add an auto-calculated value box

		[Header("Other Options")] //Add a header

		[DefaultValue(100)] //Set the default to 100
		[Label("Life Regeneration Stacks With Other Sources (%)")] //Label the option intuitively //TODO Figure out how to add the % sign -after- the value
		[Range(0,100)] //Limit the slider from 0 to 100
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(230,10,57)] //Make the slider Heart-coloured
		[Tooltip("Default: 100%\nAt 100%, life regeneration from this mod will add on top of\nlife regeneration from other sources (natural life regeneration,\narmour/buffs that regenerate life, et cetera).\n\nAt 0%, you will only regenerate the higher value of\nlife regeneration from this mod and from other sources.\n\nScales linearly between the two when neither 100% nor 0%.")] //Set a descriptive tooltip
		public int configLifeRegenStacks; //Add a slider for how much this mod's life regeneration stacks with other sources

		[DefaultValue(100)] //Set the default to 100
		[Label("Mana Regeneration Stacks With Other Sources (%)")] //Label the option intuitively //TODO Figure out how to add the % sign -after- the value
		[Range(0,100)] //Limit the slider from 0 to 100
		[Slider] //Make sure that it's an intuitive slider, not a number input box
		[SliderColor(11,61,245)] //Make the slider Star-coloured
		[Tooltip("Default: 100%\nAt 100%, mana regeneration from this mod will add on top of\nmana regeneration from other sources (natural mana regeneration,\narmour/buffs that regenerate mana, et cetera).\n\nAt 0%, you will only regenerate the higher value of\nmana regeneration from this mod and from other sources.\n\nScales linearly between the two when neither 100% nor 0%.")] //Set a descriptive tooltip
		public int configManaRegenStacks; //Add a slider for how much this mod's life regeneration stacks with other sources

		public override bool AcceptClientChanges(ModConfig pendingConfig,int whoAmI,ref string message) //Deny multiplayer clients from changing Regen From Bosses' configuration
		{
			if (Main.netMode==NetmodeID.MultiplayerClient) //If playing multiplayer, but not being the server host...
			{
				message="Only the server host may change Regen From Bosses' configuration."; //...tell the player that they're not allowed to change Regen From Bosses' configuration...
				return false; //...and deny the configuration change
			}
			return true; //If playing singleplayer or being the server host, allow the configuration change
		}

		public override void OnChanged() //Check if the configuration was changed...
		{
			RegenFromBosses.calculateRegen=true; //...and then calculate the regeneration with the new values in mind if it was - I don't know if this is unsafe for multiplayer synchronization, though?
		}
	}
}