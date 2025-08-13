using API.NET.Domains.Interfaces;
using API.NET.Entitys;
using API.NET.Infrastructure.Db;
using API.NET.Domains.DTOs;

namespace API.NET.Domains.Services;

public class AdministratorService : IAdiministratorService
{
    private readonly DataBase _db;

    public AdministratorService(DataBase db)
    {
        _db = db;
    }

    public Administrator Login(LoginDTO loginDTO)
    {
        var adm = _db.Administrators.Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault();
        if (adm is null)
        {
            throw new Exception("Usuário não encontrado");
        }
        return adm;
    }
}