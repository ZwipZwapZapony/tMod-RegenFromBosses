using System.IO; //For reading/writing stuff in network packets
using Terraria;
using Terraria.ID; //For the NetmodeID constants
using Terraria.ModLoader;

//This file is all about synchronizing the regenLife variable in multiplayer
//It has a function that sends regenLife from the server to all clients (NetSendRegen()),
//and a function that lets clients ask the server to do the above (NetGetRegen())

//Note that despite what this file is for, this mod's multiplayer synchronization compatibility is still completely unknown at the time

namespace RegenFromBosses
{
	public partial class RegenFromBosses : Mod
	{
		public static void NetSendRegen() //Send the current life regeneration value from the server to all clients
		{
			if (Main.netMode==NetmodeID.Server) //If hosting the server...
			{
				RegenFromBosses temp=new RegenFromBosses(); //...generate an object reference to the RegenFromBosses class...
				ModPacket packet=temp.NetGetPacket(5); //...create a new 5-byte network packet...
				packet.Write((byte)PacketType.ServerSendRegenToClient); //...write what the packet is for in 1 byte...
				packet.Write((int)regenLife); //...write the current life regeneration value in 4 bytes...
				packet.Send(); //...and send the packet
			}
		}

		public static void NetGetRegen() //Ask the server to transmit the current life regeneration value
		{
			if (Main.netMode==NetmodeID.MultiplayerClient) //If playing as a client on a server...
			{
				RegenFromBosses temp=new RegenFromBosses(); //...generate an object reference to the RegenFromBosses class...
				ModPacket packet=temp.NetGetPacket(1); //...create a new 1-byte network packet...
				packet.Write((byte)PacketType.ClientRequestRegenFromServer); //...ask for the life regeneration value in 1 byte...
				packet.Send(); //...and send the packet
			}
		}

		public ModPacket NetGetPacket(int capacity=256) //Terraria.ModLoader.Mod.GetPacket can't be called from a static method, so this is a non-static one
		{
			return GetPacket(capacity); //Create a new network packet, and instantly return it
		}

		public override void HandlePacket(BinaryReader reader,int whoAmI) //Do stuff with network packets
		{
			switch ((PacketType)reader.ReadByte()) //Check what sort of packet is received - The "(PacketType)" in front of the value turns the value from a byte to the below PacketType enum
			{
				case PacketType.ServerSendRegenToClient: //Used when the server sends the current life regeneration value to a client
					if (Main.netMode==NetmodeID.MultiplayerClient) //If playing as a client on a server
					{
						if (modLoadedModdersToolkit) //If Modder's Toolkit is loaded...
						{
							Main.NewText("[RegenFromBosses] Old HP/S: " + ((float)(regenLife)/2)); //...display the old regeneration per second...
							regenLife=reader.ReadInt32(); //...update the life regeneration variable to what was sent by the server...
							Main.NewText("[RegenFromBosses] New HP/S: " + ((float)(regenLife)/2)); //...and display the new regeneration per second
						}
						else //If not...
							regenLife=reader.ReadInt32(); //...just update the life regeneration variable to what was sent by the server
					}
					break;

				case PacketType.ClientRequestRegenFromServer: //Used when a client requests the current life regeneration value from the server
					calculateRegen=true; //Recalculate the life regeneration, which will also transmit that value - See the RegenFromBosses_CalculateRegen.cs file
					break;
			}
		}
	}

	enum PacketType : byte //Multiplayer synchronization network packet types
	{
		ServerSendRegenToClient,     //Used when the server sends the current life regeneration value to a client
		ClientRequestRegenFromServer //Used when a client requests the current life regeneration value from the server
	}
}
