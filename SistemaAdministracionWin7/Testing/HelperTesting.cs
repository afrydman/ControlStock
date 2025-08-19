using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DTO;
using DTO.BusinessEntities;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Services.AdministracionService;
using Services.BancoService;
using Services.ChequeraService;
using Services.ChequeService;
using Services.ClienteService;
using Services.ColorService;
using Services.CuentaService;
using Services.FormaPagoService;
using Services.LineaService;
using Services.LocalService;
using Services;
using Services.PersonalService;
using Services.ProductoService;
using Services.ProveedorService;
using Services.TemporadaService;
using Services.TributoService;

namespace Testing
{
    public class HelperTesting
    {

        public enum ServicesEnum//esto esta feo pero no encontre otra forma
        {
            IngresoService,
            RetiroService,
            TipoIngresoService,
            TipoRetiroService,
            BancoService,
            CajaService,
            ChequeraService,
            ChequeService,
            ClienteService,
            ColorService,
            ComprasProveedoresService,
            ComprasProveedoresDetalleService,
            CuentaService,
            LocalService,
            PersonalService,
            ProveedorService,
            TemporadaService,
            personaData,
            LineaService,
            ProductoService,
            Codigo,
            ListaPrecioService,
            FormaPagoService,
            condicionIvaService,
            TributoService,
            tipoNota,
            claseDocumento
        }

        public static Fixture SetUp(List<ServicesEnum> notForThisServices, bool personaDataAsClient = true,bool metrosMode = false,tipoNota notastipo =tipoNota.CreditoCliente)//oterwise is taken like proveedor
        {

            
            HelperService.getParameters();

            Fixture fixture = new Fixture();







            fixture.Register(() => notastipo);

            if (notForThisServices.Contains(ServicesEnum.claseDocumento))
                fixture.Register(() => ClaseDocumento.A);
            
            
            if (!notForThisServices.Contains(ServicesEnum.TipoIngresoService))
                fixture.Register(() => new TipoIngresoService().GetByID(new Guid("D1EB91CC-8D77-4457-982F-106B3A77C6AA")));

            if (!notForThisServices.Contains(ServicesEnum.TributoService))
                fixture.Register(() => new TributoService().GetByID(new Guid("DC4E499D-F085-4510-9AC1-000000000000")));

            if (!notForThisServices.Contains(ServicesEnum.condicionIvaService))
                fixture.Register(() => new CondicionIVAService().GetByID(new Guid("AAAAAAAA-0000-0000-0000-000000000000")));

            if (!notForThisServices.Contains(ServicesEnum.TipoRetiroService))
                fixture.Register(() => new TipoRetiroService().GetByID(new Guid("D1EB91CC-8D77-4457-0000-106B3A77C6AE")));

            if (!notForThisServices.Contains(ServicesEnum.LocalService))
                fixture.Register(() => new LocalService().GetByID(new Guid("9C5596C1-A290-4C41-9E91-9EE105F647FB")));

            if (!notForThisServices.Contains(ServicesEnum.PersonalService))
                fixture.Register(() => new PersonalService().GetByID(new Guid("5F6A39C1-948F-4FFB-8E8F-5860AEECB8C6")));

            if (!notForThisServices.Contains(ServicesEnum.BancoService))
                fixture.Register(() => new BancoService().GetByID(new Guid("B9D9D2D0-7C66-4AF7-8CCF-87F61A028AE7")));

            if (!notForThisServices.Contains(ServicesEnum.CuentaService))
                fixture.Register(() => new CuentaService().GetByID(new Guid("9D185266-8066-4DEE-BA3C-E2BBDCCC4493")));

            if (!notForThisServices.Contains(ServicesEnum.ChequeraService))
                fixture.Register(() => new ChequeraService().GetByID(new Guid("CE49A063-5C66-47E2-9514-D3E316879988")));

            if (!notForThisServices.Contains(ServicesEnum.ChequeService))
                fixture.Register(() => new ChequeService().GetByID(new Guid("553C9621-57D1-47DE-88C7-5FDC48837B04")));
            //fixture.Register<chequeData>(() => new chequeData());



            if (!notForThisServices.Contains(ServicesEnum.ProveedorService))
                fixture.Register(() => new ProveedorService().GetByID(new Guid("87BD5765-3156-44A9-B847-4013EDC5CDDE")));

            if (!notForThisServices.Contains(ServicesEnum.ClienteService))
                fixture.Register(() => new ClienteService().GetByID(new Guid("DB8B68C0-0CA3-4ED6-9F22-CA0824A2FCA7")));

            if (!notForThisServices.Contains(ServicesEnum.TemporadaService))
                fixture.Register(() => new TemporadaService().GetByID(new Guid("4FACF958-973E-4797-A08A-C00CB07BD225")));

            if (!notForThisServices.Contains(ServicesEnum.LineaService))
                fixture.Register(() => new LineaService().GetByID(new Guid("4C338B5D-CF54-4B1B-BFDE-9AE0DD93918E")));

            if (!notForThisServices.Contains(ServicesEnum.FormaPagoService))
                fixture.Register(() => new FormaPagoService().GetByID(new Guid("4C338B5D-CF54-4B1B-BFDE-9AE0DD93918E")));
            
           

            if (personaDataAsClient)
            {
                fixture.Register<PersonaData>(() => new ClienteService().GetByID(new Guid("DB8B68C0-0CA3-4ED6-9F22-CA0824A2FCA7")));
            }
            else
            {
                fixture.Register<PersonaData>(() => new ProveedorService().GetByID(new Guid("87BD5765-3156-44A9-B847-4013EDC5CDDE")));
            }



            if (!notForThisServices.Contains(ServicesEnum.ProductoService))
                fixture.Register(() => new ProductoService().GetByID(new Guid("3EC55B8E-3259-4589-A577-D9B691F82A65")));

            if (!notForThisServices.Contains(ServicesEnum.ColorService))
                fixture.Register(() => new ColorService().GetByID(new Guid("7342C790-58C3-4A66-9799-D30E2841CCDB")));


           
            
            // formato Codigo : aaaa proveedor + bbb articulo + ccc color + dd talle

            var p = new ProductoService().GetByID(new Guid("3EC55B8E-3259-4589-A577-D9B691F82A65"));
            
            //color Codigo : 010
            //


            CantidadCodigoDescriptionData a = new CantidadCodigoDescriptionData(p.CodigoInterno + "010" + "22", 2);

            if (notForThisServices.Contains(ServicesEnum.Codigo))
                fixture.Register<string>(() => p.CodigoInterno + "010" + "22");

            fixture.Register<CantidadCodigoDescriptionData>(() => a);


            if (!metrosMode)
            {
                fixture.Customizations.Add(
                       new RandomNumericSequenceGenerator(0, 99, 200));

                fixture.Customize<StockData>(ob => ob
    .Without(ss => ss.Metros).Without(x => x.Talle61));
                

            }
            HelperService.haymts = metrosMode;
                

            fixture.Register(() => new DateTime(2017, 3, 20, 20, 20, 20, 30));//porque sino se generan fechas con 10 decimales de diferencia.....weirrdddd

            fixture.RepeatCount = 10;

            return fixture;
        }



        public static List<string> GetDifferences(object test1, object test2, bool getChildrensDiff = true)
        {
            List<string> differences = new List<string>();

            if (test1 == null && test2 == null)
                return null;
            

            if (test1 == null || test2 == null)
            {
                differences.Add("null object");
                return differences;
            }

            PropertyInfo[] props = null;
           
            try
            {
                 props = test1.GetType().GetProperties();
            }
            catch (Exception)
            {
                
                throw;
            }

            if (props==null || props.Count()==0)
            {//los objects son values.
                if (test1.GetType() != typeof(EstadoCheque)) { //.....
                
                    if (!test1.Equals(test2))
                    differences.Add(test1.GetType().Name);
                }
        

            }
            foreach (PropertyInfo property in props)
            {
              

                object value1 = property.GetValue(test1, null);
                object value2 = property.GetValue(test2, null);


                if(value1==null && value2==null)
                    continue;
                try
                {
                    if (value1.GetType().Assembly.FullName.StartsWith("DTO") )//haters gonna hate
                    {
                        
                        
                        if ( getChildrensDiff)
                        {
                            differences.AddRange(GetDifferences(value1, value2));    
                        }
                        
                        
                    }

                    else
                    {
                        if (IsGenericList(value1))
                        {
                            if (!AreYourMomMyMomAndYourDadMyDad(value1, value2))
                                differences.Add(property.Name);
                        }
                        else
                        {
                            if (!value1.Equals(value2))
                            {
                                if (value1 is DateTime)
                                {
                                    DateTime dateTime1 = value1 is DateTime ? (DateTime) value1 : new DateTime();
                                    DateTime dateTime2 = value2 is DateTime ? (DateTime)value2 : new DateTime();

                                    if (Math.Abs((dateTime1 - dateTime2).Ticks) > 20000)
                                    {
                                        differences.Add(property.Name);       
                                    }
                                }
                                else
                                {
                                    differences.Add(property.Name);    
                                }
                                
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    differences.Add(property.Name);

                }

            }
            return differences;
        }

        public static bool AreYourMomMyMomAndYourDadMyDad(dynamic value1, dynamic value2)
        {

            //Type a = typeof (value1);
            //List<X> childrend1 = (List<>)value1;
            //List<X> childrend2 = (List<a>)value2;

            var childrend1 = value1;
            var childrend2 = value2;
            if (childrend1.Count != childrend2.Count)
                return false;

            bool find = false;

            foreach (dynamic  c1 in childrend1)
            {
                find = false;
                foreach (dynamic c2 in childrend2)
                {
                    if (GetDifferences(c1, c2).Count == 0)
                    {
                        find = true;
                        break;
                    }
                }
                if (!find) return false;
            }
            return true;

        }


        public static bool IsGenericList(object o)
        {
            var oType = o.GetType();
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }

    }
}
