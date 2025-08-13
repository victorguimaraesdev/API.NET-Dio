using API.NET.Domains.DTOs;
using API.NET.Entitys;

namespace API.NET.Domains.Interfaces
{
    public interface IAdministratorService
    {
       Administrator Login (LoginDTO loginDTO);
    }
}