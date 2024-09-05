using AutoMapper;
using BAL.Requests;
using BAL.Response;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            #region Mapper_Response
            CreateMap<Account, AccountResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            #endregion
            #region Mapper_Request
            CreateMap<RegisterAccountRequest, Account>();
            #endregion
        }
    }
}
