using System;
using DTO;
using DTO.BusinessEntities;
using Repository.Repositories.PersonalRepository;
using Services.AdministracionService;
using Services.Interfaces;

namespace Services.PersonalService
{
    public class PersonalService : PersonaService<PersonalData, IPersonalRepository>, IDefaultable<PersonalData>
    { 
    public PersonalService(IPersonalRepository repo)
        : base(repo)
    {
            _repo = repo;
            
        }

    public PersonalService(bool local = true)
         {
             _repo = new PersonalRepository(local);
           
              
         }

        public override PersonalData getPropertiesInfo(PersonalData n)
        {
            if (IsEmpty(n))
                n = GetDefault();

            return n;
        }

        public override void GetCC(PersonaData persona, out DateTime maxDateRecibo, out DateTime maxDateVenta, out decimal subt)
        {
            throw new NotImplementedException();
        }

        public PersonalData GetDefault()
        {
            PersonalData p = new PersonalData();
            p.Enable = true;
            p.RazonSocial = "Sin Descripcion";
            p.NombreContacto = "Sin Descripcion";
            p.Description = "Sin Descripcion";


            return p;
        }

     

        
    }
}
