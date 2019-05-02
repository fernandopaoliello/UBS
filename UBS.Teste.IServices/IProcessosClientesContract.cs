using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UBS.Teste.IServices
{
    public interface IProcessosClientesContract
    {
        [OperationContract(IsOneWay = true)]
        void ClienteProcessado(string nome, string email, string empresa, string sexo);
    }
}
