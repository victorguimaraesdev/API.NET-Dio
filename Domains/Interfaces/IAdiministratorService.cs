using API.NET.Domains.DTOs;
using API.NET.Entitys;

namespace API.NET.Domains.Interfaces
{
    public interface IAdiministratorService
    {
       Administrator Login (LoginDTO loginDTO);
    }
}