using API.NET.Domains.DTOs;
using API.NET.Domains.Entitys;

namespace API.NET.Domains.Interfaces
{
    public interface IAdministratorService
    {
        Administrator Login (LoginDTO loginDTO);
        Administrator Save (Administrator administrator);
        Administrator? Find(int id);
        List<Administrator> All (int? page);
    }
}