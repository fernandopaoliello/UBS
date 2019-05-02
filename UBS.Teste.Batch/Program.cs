using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UBS.Teste.IServices;
using UBS.Teste.Dto;

namespace UBS.Teste.Batch
{
    class Program
    {
        static List<String> arquivos = new List<string>();
        static IProcessosClientes clientContract;

        static void Main(string[] args)
        {
            ProcessarArquivo();
        }
        public static void ProcessarArquivo()
        {
            String caminhoArquivos = ConfigurationManager.AppSettings["LocalArquivo"];
            arquivos = System.IO.Directory.EnumerateFiles(caminhoArquivos, "*.json", System.IO.SearchOption.TopDirectoryOnly).ToList();

            ProcessosClientesClient clientCallbackService = new ProcessosClientesClient();
            //IProcessosClientes clientContract;

            InstanceContext callback = new InstanceContext(clientCallbackService);
            DuplexChannelFactory<IProcessosClientes> channelFactory = new DuplexChannelFactory<IProcessosClientes>(callback, "UBS.Teste.IServices.IProcessosClientes");
            clientCallbackService.ChannelFactory = channelFactory;

            clientContract = channelFactory.CreateChannel();

            for (int i = 0; i < arquivos.Count; i++)
            {
                Thread thread = new Thread(() => LerArquivo(arquivos[i]));
                thread.Start();              
            }

            Console.WriteLine();
            Console.WriteLine("ENTER para sair.");
            Console.ReadLine();
        }
        public static void LerArquivo(object nomeArquivo)
        {
            using (StreamReader r = new StreamReader(nomeArquivo.ToString()))
            {
                string json = r.ReadToEnd();
                List<ClienteDto> items = JsonConvert.DeserializeObject<List<ClienteDto>>(json);

                foreach (ClienteDto cliente in items)
                {
                    clientContract.EnviarClienteProcessado(new ClienteDto() { first_name = cliente.first_name, gender = cliente.gender, email = cliente.email, Company = cliente.Company });
                }
            }
        }

        public class ProcessosClientesClient : IProcessosClientesContract
        {
            public DuplexChannelFactory<IProcessosClientes> ChannelFactory { get; set; }

            public void ClienteProcessado(string nome, string email, string empresa, string sexo)
            {
                //throw new NotImplementedException();
            }
        }
    }
}
