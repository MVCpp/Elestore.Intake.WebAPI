using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elestor.Intake.API.Models;

namespace Elestor.Intake.API.Interfaces
{
    public interface ILogin
    {
        Task<IEnumerable<Usuario>> Login(Usuario usuario);
    }
}
