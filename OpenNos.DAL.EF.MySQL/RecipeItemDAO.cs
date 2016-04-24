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

using AutoMapper;

using OpenNos.DAL.EF.MySQL.Helpers;
using OpenNos.DAL.Interface;
using OpenNos.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OpenNos.DAL.EF.MySQL
{
    public class RecipeItemDAO : IRecipeItemDAO
    {
        #region Methods

        public RecipeItemDTO Insert(RecipeItemDTO recipeItem)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                RecipeItem entity = Mapper.DynamicMap<RecipeItem>(recipeItem);
                context.RecipeItem.Add(entity);
                context.SaveChanges();
                return Mapper.DynamicMap<RecipeItemDTO>(entity);
            }
        }

        public IEnumerable<RecipeItemDTO> LoadAll()
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                foreach (RecipeItem rec in context.RecipeItem)
                {
                    yield return Mapper.DynamicMap<RecipeItemDTO>(rec);
                }
            }
        }

        public RecipeItemDTO LoadById(int recipeItemId)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                return Mapper.DynamicMap<RecipeItemDTO>(context.RecipeItem.FirstOrDefault(s => s.RecipeItemId.Equals(recipeItemId)));
            }
        }

        public IEnumerable<RecipeItemDTO> LoadByRecipe(short recipeId)
        {
            using (var context = DataAccessHelper.CreateContext())
            {
                foreach (RecipeItem RecipeItem in context.RecipeItem.Where(s => s.RecipeId.Equals(recipeId)))
                {
                    yield return Mapper.DynamicMap<RecipeItemDTO>(RecipeItem);

                }
            }
        }

        #endregion
    }
}