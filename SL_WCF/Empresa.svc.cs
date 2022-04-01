using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Empresa" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Empresa.svc or Empresa.svc.cs at the Solution Explorer and start debugging.
    public class Empresa : IEmpresa
    {

        public SL_WCF.Result Add(ML.Empresa empresa){
            ML.Result result = BL.Empresa.Add(empresa);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects                
            };
        }

        public SL_WCF.Result Update(ML.Empresa empresa)
       {
            ML.Result result = BL.Empresa.UpdateById(empresa);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects                
            };
        }

        public SL_WCF.Result Delete(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.DeleteById(IdEmpresa);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects                
            };
        }

        public SL_WCF.Result GetAll()
        {
            ML.Result result = BL.Empresa.GetAllEF();
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects                
            };
        }

        public SL_WCF.Result GetById(int IdEmpresa)
        {
            ML.Result result = BL.Empresa.GetByID(IdEmpresa);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }
    }
}
