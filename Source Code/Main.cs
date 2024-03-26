using CommandSystem.Commands.RemoteAdmin.ServerEvent;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using Exiled.Events;
using Exiled.Events.EventArgs.Player;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features.Roles;
using Exiled.Permissions.Extensions;
using MEC;
using RemoteAdmin;
using Exiled.CustomRoles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using Respawning;
using UnityEngine;
using Item = PluginAPI.Core.Items.Item;
using Random = System.Random;

namespace Scp073
{
    public class Plugin:Plugin<Config>
    {
        public override string Author { get; } = "Friza";

        public override string Name { get; } = "SCP-073";

        public override string Prefix { get; } = "SCP-073";

        public override Version Version { get; } = new Version(1, 0, 1);

        public override Version RequiredExiledVersion { get; } = new Version(8, 8, 0);
        
        public Random random = new Random();
        private CoroutineHandle _healCoroutine;
        public Plugin plugin;
        public string SCP073ID = "";
        public override void OnEnabled()
        {
            plugin = this;
            Exiled.Events.Handlers.Server.RoundStarted += this.RoundStarted;
            Exiled.Events.Handlers.Player.Hurting += this.OnHurt;
            Exiled.Events.Handlers.Player.Died += this.OnDied;
            Exiled.Events.Handlers.Player.UsingItem += this.OnUsingItem;
            Exiled.Events.Handlers.Player.Joined += this.OnPlayerJoin;
            Exiled.Events.Handlers.Player.Left += this.OnPlayerLeave;
                
            Log.Info("");
            base.OnEnabled();
        }
        public void RoundStarted()
        {
              int rand = random.Next(1, 100);

              if (rand < Config.spawnChance)
              {
                  Timing.CallDelayed(2f, () =>
                  {
                      SCP073ID = Player.Get(PlayerRoles.RoleTypeId.Scientist).ToList().RandomItem().UserId;
                  });
                  Timing.CallDelayed(2f, () => {
                      var player = Player.Get(SCP073ID);
                      player.CustomInfo = "Каин";
                      player.Role.Set(RoleTypeId.Scientist);
                      player.RankColor = "yellow";
                      player.RankName = "Scp-073";
                      player.MaxHealth = 140;
                      player.Health = 140;
                      player.AddItem(ItemType.Medkit);
                      player.ShowHint(Config.Hint1, 5f);
                      Cassie.Message("<split>Attention SCP 0 7 3 has been contained is error ");
                      Cassie.MessageTranslated(String.Empty,"<split>Внимание! <size=0> pitch_0.97 . . . Attention </size><split>Условия содержания SСP-073 нарушены... <size=0> . SCP 0 7 3 has been contained is error</size>");
                  });
              }
        }
        
        public void OnPlayerJoin(JoinedEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Каин")
            {
                _healCoroutine = Timing.RunCoroutine(HealPlayer(ev.Player), "healCoroutine");
            }
        }

        public void OnPlayerLeave(LeftEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Каин")
            {
                Timing.KillCoroutines(_healCoroutine);
            }
        }

        private IEnumerator<float> HealPlayer(Player player)
        {
            while (player.Health < player.MaxHealth)
            {
                yield return Timing.WaitForSeconds(2f);
                player.Health += 1f;
            }
        }
        
        public void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.Medkit)
            {
                if (ev.Player.CustomInfo == "Каин")
                {
                    ev.IsAllowed = false;
                }
            }
            if (ev.Item.Type == ItemType.SCP500)
            {
                if (ev.Player.CustomInfo == "Каин")
                {
                    ev.IsAllowed = false;
                }
            }
        }
        
        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player.CustomInfo == "Каин")
            {
                ev.Player.CustomInfo = "";
                ev.Player.RankColor = "";
                ev.Player.RankName = "";
            }
        }
        
        public void OnHurt(HurtingEventArgs ev)
        {
            if (ev.Player != null && ev.Player.CustomInfo == "Каин")
            {
                if (ev.Attacker != null)
                {
                    ev.Attacker.Hurt(ev.Amount, DamageType.Com15);
                    ev.Amount = 5;
                }
            }
        }
    }
}