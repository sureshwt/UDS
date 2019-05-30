using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtileDigitalSecurity.Models;
using UtileDigitalSecurity.ViewModels;

namespace UtileDigitalSecurity.Mappings
{
  public class UserMapping : Profile
  {
    public UserMapping()
    {
      CreateMap<AddUserViewModel, ApplicationUser>();
      CreateMap<ApplicationUser, AddUserViewModel>();
    }

  }
}
