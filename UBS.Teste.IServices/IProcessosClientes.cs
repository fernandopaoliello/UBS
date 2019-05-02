using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UBS.Teste.Dto;

namespace UBS.Teste.IServices
{
    [ServiceContract(SessionMode = SessionMode.Required,
          CallbackContract = typeof(IProcessosClientesContract))]
    public interface IProcessosClientes
    {
        [OperationContract(IsOneWay = false, IsInitiating = true)]
        void Subscribe();
        [OperationContract(IsOneWay = false, IsTerminating = true)]
        void Unsubscribe();
        [OperationContract(IsOneWay = true)]
        void EnviarClienteProcessado(ClienteDto cliente);
    }
}
