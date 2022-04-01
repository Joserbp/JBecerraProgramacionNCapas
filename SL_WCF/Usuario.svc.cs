using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Usuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Usuario.svc or Usuario.svc.cs at the Solution Explorer and start debugging.
    public class Usuario : IUsuario
    {
        public SL_WCF.Result Add(ML.Usuario usuario)
        {
            ML.Result resultUsuario = BL.Usuario.AddEF(usuario);
            return new SL_WCF.Result
            {
                Correct = resultUsuario.Correct,
                ErrorMessage = resultUsuario.ErrorMessage,
                Ex = resultUsuario.Ex,
                Object = resultUsuario.Object,
                Objects = resultUsuario.Objects,
            };
        }
        public SL_WCF.Result Update(ML.Usuario usuario)
        {
            ML.Result resultUsuario = BL.Usuario.UpdateByIdEF(usuario);
            return new SL_WCF.Result
            {
                Correct = resultUsuario.Correct,
                ErrorMessage = resultUsuario.ErrorMessage,
                Ex = resultUsuario.Ex,
                Object = resultUsuario.Object,
                Objects = resultUsuario.Objects,
            };
        }
        public SL_WCF.Result Delete(int IdUsuario)
        {
            ML.Result resultUsuario = BL.Usuario.DeleteByIdEF(IdUsuario);
            return new SL_WCF.Result
            {
                Correct = resultUsuario.Correct,
                ErrorMessage = resultUsuario.ErrorMessage,
                Ex = resultUsuario.Ex,
                Object = resultUsuario.Object,
                Objects = resultUsuario.Objects,
            };
        }
        public SL_WCF.Result GetAll(ML.Usuario usuario)
        {
            ML.Result resultUsuario = BL.Usuario.GetAllEF(usuario);
            return new Result
            {
                Correct = resultUsuario.Correct,
                ErrorMessage = resultUsuario.ErrorMessage,
                Ex = resultUsuario.Ex,
                Object = resultUsuario.Object,
                Objects = resultUsuario.Objects,
            };

        }
        public SL_WCF.Result GetById(int IdUsuario)
        {
            ML.Result resultUsuario = BL.Usuario.GetByIdEF(IdUsuario);
            SL_WCF.Result result = new SL_WCF.Result()
            {
                Correct = resultUsuario.Correct,
                ErrorMessage = resultUsuario.ErrorMessage,
                Ex = resultUsuario.Ex,
                Object = resultUsuario.Object,
                Objects = resultUsuario.Objects,
            };
            return result;
        }
    }
}
