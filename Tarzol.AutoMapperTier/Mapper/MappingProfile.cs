using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tarzol.AutoMapperTier.ViewModels;
using Tarzol.Entity;

namespace Tarzol.AutoMapperTier.Mapper
{
   public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductAddVM>().ReverseMap();
        }
    }
}
