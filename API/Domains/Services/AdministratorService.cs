using API.NET.Domains.Interfaces;
using API.NET.Domains.Entitys;
using API.NET.Infrastructure.Db;
using API.NET.Domains.DTOs;

namespace API.NET.Domains.Services;

public class AdministratorService : IAdministratorService
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

    public Administrator Save(Administrator administrator)
    {
        _db.Administrators.Add(administrator);
        _db.SaveChanges();

        return administrator;
    }

    public Administrator? Find(int id)
    {
       return _db.Administrators.Where(v => v.Id == id).FirstOrDefault();
    }
    
    public List<Administrator> All(int? page)
    {
        var query = _db.Administrators.AsQueryable();

        int itensForPage = 10;

        if (page != null)
        {
            query = query.Skip(((int)page - 1) * itensForPage).Take(itensForPage);
        }


        return query.ToList();
    }

}