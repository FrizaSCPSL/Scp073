using System;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Permissions.Extensions;
using MEC;
using Scp073;
using RemoteAdmin;
using Exiled.CustomRoles;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Respawning;
using UnityEngine;

namespace Scp073.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnSCP273 : ICommand
    {
        
       
        public string Command { get; } = "spawnscp073";

        public string[] Aliases { get; } = new string[]
        {
            "scp073",
            "spawn073"
        };

        public string Description { get; } = "Смена класса на Scp-073";
        
        
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Player player = Player.Get(arguments.At(0));
            if (player == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }

            else
            {
                player.CustomInfo = "Каин";
                player.Role.Set(RoleTypeId.Scientist);
                player.RankColor = "yellow";
                player.RankName = "Scp-073";
                player.MaxHealth = 140;
                player.Health = 140;
                player.AddItem(ItemType.Medkit);
                player.ShowHint("SCP-073 -каин. Научный сотрудник только сильнее..", 5f);
                Cassie.Message("<split>Attention SCP 0 7 3 has been contained is error ");
                Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SСP-073 нарушены... <size=0> . SCP 0 7 3 has been contained is error</size>");
                response = "Теперь вы Каин!";
                return true;
            }

        }
    }
}