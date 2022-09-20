using AutoMapper;
using SAT.Recruitment.Business.DTO;
using SAT.Recruitment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.Recruitment.Business.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
