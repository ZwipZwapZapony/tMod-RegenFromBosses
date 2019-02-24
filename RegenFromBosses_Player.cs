using Terraria;
using Terraria.ID; //For the NetmodeID constants
using Terraria.ModLoader;

//This file makes players regenerate some life as they should,
//and asks the server for the life regeneration amount when joining a server

namespace RegenFromBosses
{
	public class RegenFromBosses_Player : ModPlayer //Hook into stuff for every player
	{
		public override void UpdateLifeRegen() //Regenerate some life!
		{
			//Crude check to see if there's something that should prevent life regeneration
			if (!(player.lifeRegen<0 || player.bleed || player.burned || player.electrified || player.onFire || player.onFire2 || player.onFrostBurn || player.poisoned || player.suffocating || player.venom || (Main.expertMode && player.tongued)))
				player.lifeRegen+=RegenFromBosses.regenLife; //If nothing above says stop, let the player regenerate some life
		}

		public override void OnEnterWorld(Player player) //When a player enters a world...
		{
			if (Main.netMode==NetmodeID.MultiplayerClient) //...if playing as a client on a server...
			{
				RegenFromBosses.regenLife=0; //...reset the local life regeneration value...
				RegenFromBosses.NetGetRegen(); //...and ask the server for it instead
			}
		}
	}
}
