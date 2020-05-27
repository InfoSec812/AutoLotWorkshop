﻿using System.Collections.Generic;
using System.Linq;
using AutoLot.Dal.EfStructures;
using AutoLot.Dal.Models.Entities;
using AutoLot.Dal.Repos.Base;
using AutoLot.Dal.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Dal.Repos
{
    public class CarRepo : BaseRepo<Car>, ICarRepo
    {
        public CarRepo(ApplicationDbContext context) : base(context)
        {
        }

        internal CarRepo(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override IEnumerable<Car> GetAll() 
            => Table.Include(c => c.MakeNavigation).OrderBy(o => o.PetName);

        public IEnumerable<Car> GetAllBy(int makeId)
        {
            return Table.Include(c => c.MakeNavigation).Where(x => x.MakeId == makeId).OrderBy(c => c.PetName);
        }

        public override Car Find(int? id)
            => Table.Where(x => x.Id == id).Include(m => m.MakeNavigation).FirstOrDefault();
    }
}
