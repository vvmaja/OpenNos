﻿/*
 * This file is part of the OpenNos Emulator Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 */

using OpenNos.Core;
using OpenNos.Data;
using OpenNos.Domain;
using System;
using System.Linq;

namespace OpenNos.GameObject
{
    public class NRunHandler
    {
        #region Methods

        public static void NRun(ClientSession Session, byte type, short runner, short data3, short npcid)
        {
            switch (runner)
            {
                case 1:
                    if (Session.Character.Class != (byte)ClassType.Adventurer)
                    {
                        Session.Client.SendPacket(Session.Character.GenerateMsg(Language.Instance.GetMessageFromKey("NOT_ADVENTURER"), 0));
                        return;
                    }
                    if (Session.Character.Level < 15 || Session.Character.JobLevel < 20)
                    {
                        Session.Client.SendPacket(Session.Character.GenerateMsg(Language.Instance.GetMessageFromKey("LOW_LVL"), 0));
                        return;
                    }

                    if (Session.Character.EquipmentList.isEmpty())
                    {
                        ClientLinkManager.Instance.ClassChange(Session.Character.CharacterId, Convert.ToByte(type));
                    }
                    else
                    {
                        Session.Client.SendPacket(Session.Character.GenerateMsg(Language.Instance.GetMessageFromKey("EQ_NOT_EMPTY"), 0));
                    }
                    break;

                case 2:
                    Session.Client.SendPacket($"wopen 1 0");
                    break;

                case 10:
                    Session.Client.SendPacket($"wopen 3 0");
                    break;

                case 12:
                    Session.Client.SendPacket($"wopen {type} 0");
                    break;

                case 14:
                    // m_list 2 1002 1003 1004 1005 1006 1007 1008 1009 1010 180 181 2127 2178 1242 1243 1244 2504 2505 - 100
                    Session.Client.SendPacket($"wopen 27 0");
                    break;
                case 16:
                    MapNpc npc = Session.CurrentMap.Npcs.FirstOrDefault(s => s.MapNpcId == npcid);
                    if (npc != null)
                    {
                        TeleporterDTO tp = npc.Teleporters?.FirstOrDefault(s => s.Index == type);
                        if (tp != null)
                        {
                            Session.Character.MapY = tp.MapY;
                            Session.Character.MapX = tp.MapX;
                            Session.Character.MapId = tp.MapId;
                            ClientLinkManager.Instance.ChangeMap(Session.Character.CharacterId);
                        }
                    }
                    break;
                default:
                    Logger.Log.Warn(String.Format(Language.Instance.GetMessageFromKey("NO_NRUN_HANDLER"), runner));
                    break;
            }
        }

        #endregion
    }
}