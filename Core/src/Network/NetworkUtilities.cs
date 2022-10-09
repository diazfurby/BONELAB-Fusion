﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LabFusion.Representation;
using LabFusion.Utilities;

namespace LabFusion.Network {
    public static class NetworkUtilities {
        public static bool HasServer => FusionMod.CurrentNetworkLayer.IsServer || FusionMod.CurrentNetworkLayer.IsClient;
        public static bool IsServer => FusionMod.CurrentNetworkLayer.IsServer;
        public static bool IsClient => FusionMod.CurrentNetworkLayer.IsClient && !FusionMod.CurrentNetworkLayer.IsServer;

        public const string InvalidAvatarId = "BONELABFUSION.NONE";

        public static void RemoveUser(ulong longId) {
            var id = PlayerId.GetPlayerId(longId);

            if (id != null) {
                if (PlayerRep.Representations.ContainsKey(id.SmallId))
                    PlayerRep.Representations[id.SmallId].Dispose();

                id.Dispose();

#if DEBUG
                FusionLogger.Log($"User with long id {longId} was removed.");
#endif
            }
        }

        public static void RemoveAllUsers() {
            foreach (var id in PlayerId.PlayerIds.ToList()) {
                RemoveUser(id.LongId);
            }
        }

        public static void OnDisconnect() {
            RemoveAllUsers();
        }
    }
}
