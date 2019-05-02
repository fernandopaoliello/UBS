using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UBS.Teste.IServices;

namespace UBS.Teste.UIForms
{
    public partial class Form1 : Form
    {
        ProcessosClientesClient clientCallbackService = new ProcessosClientesClient();
        IProcessosClientes clientContract;

        public Form1()
        {
            InitializeComponent();
            IniciarConexaoProcesso();
        }

        private void IniciarConexaoProcesso()
        {
            clientCallbackService.MyTextBlock = this.txtCliente;
            InstanceContext callback = new InstanceContext(clientCallbackService);
            DuplexChannelFactory<IProcessosClientes> channelFactory = new DuplexChannelFactory<IProcessosClientes>(callback, "UBS.Teste.IServices.IProcessosClientes");
            clientCallbackService.ChannelFactory = channelFactory;

            WSDualHttpBinding binding = (WSDualHttpBinding)clientCallbackService.ChannelFactory.Endpoint.Binding;

            string clientcallbackaddress = binding.ClientBaseAddress.AbsoluteUri;
            clientcallbackaddress += Guid.NewGuid().ToString();
            binding.ClientBaseAddress = new Uri(clientcallbackaddress);

            clientContract = channelFactory.CreateChannel();
            clientContract.Subscribe();
        }

        public class ProcessosClientesClient : IProcessosClientesContract
        {
            public DuplexChannelFactory<IProcessosClientes> ChannelFactory { get; set; }
            public System.Windows.Forms.TextBox MyTextBlock { get; set; }
            public System.Windows.Forms.TextBox MyTextFeminino { get; set; }
            public System.Windows.Forms.TextBox MyTextMasculino { get; set; }
            public object Lock = new object();

            public void ClienteProcessado(string nome, string email, string empresa, string sexo)
            {
                lock (Lock)
                {
                    MyTextBlock.Text += string.Concat(string.Format("Nome: {0}\t - E-mail: {1}\t\t Empresa: {2}\t\t Sexo: {3}\t\t", nome, email, empresa, sexo), System.Environment.NewLine);
                    MyTextBlock.SelectionStart = MyTextBlock.Text.Length;
                    MyTextBlock.ScrollToCaret();

                    if (sexo == "Female")
                    {
                        MyTextFeminino.Text += string.Concat(string.Format("Nome: {0}\t - E-mail: {1}\t\t Empresa: {2}\t\t Sexo: {3}\t\t", nome, email, empresa, sexo), System.Environment.NewLine);
                        MyTextFeminino.SelectionStart = MyTextFeminino.Text.Length;
                        MyTextFeminino.ScrollToCaret();
                    }
                    else
                    {
                        MyTextMasculino.Text += string.Concat(string.Format("Nome: {0}\t - E-mail: {1}\t\t Empresa: {2}\t\t Sexo: {3}\t\t", nome, email, empresa, sexo), System.Environment.NewLine);
                        MyTextMasculino.SelectionStart = MyTextMasculino.Text.Length;
                        MyTextMasculino.ScrollToCaret();
                    }
                }
            }
        }
    }
}
