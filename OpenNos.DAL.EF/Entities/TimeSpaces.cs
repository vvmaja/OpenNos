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

using System.ComponentModel.DataAnnotations;

namespace OpenNos.DAL.EF
{
    public class TimeSpace
    {
        #region Properties

        public virtual Map Map { get; set; }

        public short TimespaceId { get; set; }

        public short MapId { get; set; }

        public short PositionX { get; set; }

        public short PositionY { get; set; }

        [MaxLength(255)]
        public string Winner { get; set; }

        [MaxLength(int.MaxValue)]
        public string Script { get; set; }

        public int WinnerScore { get; set; }

        #endregion
    }
}