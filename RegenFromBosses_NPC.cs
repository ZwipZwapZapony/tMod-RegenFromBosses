using Terraria;
using Terraria.ModLoader;

//This file simply makes the life regeneration amount get recalculated when a boss is slain

namespace RegenFromBosses
{
	public class RegenFromBosses_NPC : GlobalNPC //Hook into stuff for all NPCs
	{
		public override void NPCLoot(NPC npc) //When a NPC dies...
		{
			if (npc.boss) //...if the NPC is an enemy boss...
				RegenFromBosses.calculateRegen=true; //...recalculate the regeneration
		}
	}
}
