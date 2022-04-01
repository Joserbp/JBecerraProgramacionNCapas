using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmpresa" in both code and config file together.
    [ServiceContract]
    public interface IEmpresa
    {
        [OperationContract]
        SL_WCF.Result Add(ML.Empresa empresa);
        [OperationContract]
        SL_WCF.Result Update(ML.Empresa empresa);
        [OperationContract]
        SL_WCF.Result Delete(int IdEmpresa);
        [OperationContract]
        SL_WCF.Result GetAll();
        [OperationContract]
        SL_WCF.Result GetById(int IdEmpresa);
    }
}
