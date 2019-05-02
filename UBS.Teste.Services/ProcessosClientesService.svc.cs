using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UBS.Teste.Dto;
using UBS.Teste.IServices;

namespace UBS.Teste.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProcessosClientesService : IProcessosClientes
    {
        public static event ClienteProcessadoEventHandler ClienteProcessadoChangeEvent;
        public delegate void ClienteProcessadoEventHandler(object sender, ClienteProcessadoEventArgs e);

        IProcessosClientesContract callback = null;

        ClienteProcessadoEventHandler clienteProcessadoChangeHandler = null;

        public void Subscribe()
        {
            callback = OperationContext.Current.GetCallbackChannel<IProcessosClientesContract>();
            clienteProcessadoChangeHandler = new ClienteProcessadoEventHandler(clienteChangeHandler);
            ClienteProcessadoChangeEvent += clienteProcessadoChangeHandler;
        }

        public void Unsubscribe()
        {
            ClienteProcessadoChangeEvent -= clienteProcessadoChangeHandler;
        }

        public void EnviarClienteProcessado(ClienteDto cliente)
        {
            ClienteProcessadoEventArgs e = new ClienteProcessadoEventArgs();
            e.first_name = cliente.first_name;
            e.email = cliente.email;
            e.Company = cliente.Company;
            e.gender = cliente.gender;
            if (ClienteProcessadoChangeEvent != null)
                ClienteProcessadoChangeEvent(this, e);
        }

        public void clienteChangeHandler(object sender, ClienteProcessadoEventArgs e)
        {
            callback.ClienteProcessado(e.first_name, e.email, e.Company, e.gender);
        }
    }
}
