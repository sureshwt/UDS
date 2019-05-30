using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtileDigitalSecurity.Models;
using UtileDigitalSecurity.ViewModels;

namespace UtileDigitalSecurity.Mappings
{
  public class UserTokenDetailsMapping : Profile
  {
    public UserTokenDetailsMapping()
    {
      CreateMap<AppDetails, UserTokenDetails>()
          .ForMember(d => d.DateCreated, o => o.Ignore())
          .ForMember(d => d.DateUpdated, o => o.Ignore());
      CreateMap<UserTokenDetails, AppDetails>();
    }
  }
}
