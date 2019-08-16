using Terraria.ModLoader; //For the ModWorld class type

//This file makes sure that the life regeneration is re-calculated when loading a world,
//as well as when other files say that it should be re-calculated

namespace RegenFromBosses
{
	public class RegenFromBosses_World : ModWorld //Hook into stuff for the world
	{
		public override void Initialize() //When a world is loaded...
		{
			RegenFromBosses.regenLife=0; //...reset the life regeneration amount...
			RegenFromBosses.calculateRegen=true; //...and then calculate the regeneration (so that it matches the given world's defeated bosses/mini-bosses/events)
		}

		public override void PostUpdate() //At the end of every frame...
		{
			if (RegenFromBosses.calculateRegen==true) //...if regeneration is to be recalculated...
			{
				RegenFromBosses.calculateRegen=false; //...make sure that we don't recalculate it the next frame...
				RegenFromBosses.CalculateRegen(); //...and recalculate the regeneration - See the RegenFromBosses_CalculateRegen.cs file
			}
		}
	}
}