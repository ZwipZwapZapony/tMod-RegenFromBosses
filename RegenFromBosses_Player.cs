using Terraria;
using Terraria.ID; //For the NetmodeID constants
using Terraria.ModLoader;

//This file makes players regenerate some life as they should,
//and asks the server for the life regeneration amount when joining a server

namespace RegenFromBosses
{
	public class RegenFromBosses_Player : ModPlayer //Hook into stuff for every player
	{
		public override void UpdateLifeRegen() //Regenerate some life and/or mana!
		{
			//Crude check to see if there's something that should prevent life regeneration
			if (!(player.lifeRegen<0 || player.bleed || player.burned || player.electrified || player.onFire || player.onFire2 || player.onFrostBurn || player.poisoned || player.suffocating || player.venom || (Main.expertMode && player.tongued)))
			{
				player.lifeRegen=(int)(((player.lifeRegen>=RegenFromBosses.regenLife) ? player.lifeRegen : RegenFromBosses.regenLife)*(1f-((float)(Config_ServerSide.Instance.configLifeRegenStacks)/100f)))+
				(int)((player.lifeRegen+RegenFromBosses.regenLife)*(int)((float)(Config_ServerSide.Instance.configLifeRegenStacks)/100f)); //Linearly interpolate between the "don't stack" and "do stack" life regeneration options
			}
			//Extremely crude check to see if there's something that should prevent mana regeneration
			if (!(player.manaRegen<0 || player.manaRegenDelay>0))
			{
				player.manaRegen=(int)(((player.manaRegen>=RegenFromBosses.regenMana) ? player.manaRegen : RegenFromBosses.regenMana)*(1f-((float)(Config_ServerSide.Instance.configManaRegenStacks)/100f)))+
				(int)((player.manaRegen+RegenFromBosses.regenMana)*(int)((float)(Config_ServerSide.Instance.configManaRegenStacks)/100f)); //Linearly interpolate between the "don't stack" and "do stack" mana regeneration options
			}
		}

		public override void OnEnterWorld(Player player) //When a player enters a world...
		{
			if (Main.netMode==NetmodeID.MultiplayerClient) //...if playing as a client on a server...
			{
				RegenFromBosses.regenLife=0; //...reset the local life regeneration value...
				//RegenFromBosses.NetGetRegen(); //...and ask the server for it instead
				RegenFromBosses.calculateRegen=true; //Actually, calculate it ourselves instead - I can't figure out manual synchronization yet, and the server synchronizes all variables needed for calculating it, so why not?
			}
		}
	}
}