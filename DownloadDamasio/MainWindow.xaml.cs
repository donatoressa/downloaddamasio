using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DownloadDamasio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //string [] videos = File.ReadAllLines(string.Concat(Directory.GetCurrentDirectory(),"\\VideosDamasio.txt"));

        }

        string nomeArquivoEntrada = string.Empty;
        string nomeArquivoSaida = string.Empty;
        ProcessStartInfo ProcessInfo;
        Process Process;

        private void btnRun_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            nomeArquivoEntrada = txtArquivoEntrada.Text;
            nomeArquivoSaida = txtArquivoSaida.Text.Trim();

            if (ValidarPreenchimentoCampos(nomeArquivoEntrada, nomeArquivoSaida))
            {
                Processando.Visibility = Visibility.Visible;
                string diretorio = Directory.GetCurrentDirectory();
                File.WriteAllText(string.Concat(diretorio,"\\DownloadDamasio.bat"), MontarArquivoBat(nomeArquivoEntrada, nomeArquivoSaida));

                String command = string.Concat(diretorio, "\\DownloadDamasio.bat");
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
                Process = Process.Start(ProcessInfo);
                Process.WaitForExit();
                if (Process.ExitCode.Equals(0))
                {
                    msgSucesso.Text = "Download realizado com sucesso!";
                    msgErro.Text = string.Empty;

                    msgSucesso.Visibility = Visibility.Visible;
                    msgErro.Visibility = Visibility.Collapsed;
                }
                else
                {
                    msgErro.Text = Process.ExitCode.ToString();
                    msgSucesso.Text = string.Empty;

                    msgSucesso.Visibility = Visibility.Collapsed;
                    msgErro.Visibility = Visibility.Visible;
                }
                Process.Close();
                Processando.Visibility = Visibility.Collapsed;
            }
        }

        private void btnRun_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            nomeArquivoEntrada = txtArquivoEntrada.Text;
            nomeArquivoSaida = txtArquivoSaida.Text.Trim();

            if (ValidarPreenchimentoCampos(nomeArquivoEntrada, nomeArquivoSaida))
            {
                Processando.Visibility = Visibility.Visible;
                string diretorio = Directory.GetCurrentDirectory();
                File.WriteAllText(string.Concat(diretorio,"\\DownloadDamasio.bat"), MontarArquivoBat(nomeArquivoEntrada, nomeArquivoSaida));
                
                String command = string.Concat(diretorio,"\\DownloadDamasio.bat");
                ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
                Process = Process.Start(ProcessInfo);
                Process.WaitForExit();
                if (Process.ExitCode.Equals(0))
                {
                    msgSucesso.Text = "Download realizado com sucesso!";
                    msgErro.Text = string.Empty;

                    msgSucesso.Visibility = Visibility.Visible;
                    msgErro.Visibility = Visibility.Collapsed;
                }
                else
                {
                    msgErro.Text = Process.ExitCode.ToString();
                    msgSucesso.Text = string.Empty;

                    msgSucesso.Visibility = Visibility.Collapsed;
                    msgErro.Visibility = Visibility.Visible;
                }
                Process.Close();
                Processando.Visibility = Visibility.Collapsed;
            }
        }

        private string MontarArquivoBat(string nomeArquivoEntrada, string nomeArquivoSaida)
        {
            StringBuilder s = new StringBuilder();
            s.Append("ffmpeg.exe -i ");
            s.Append(nomeArquivoEntrada.Trim());
            s.Append(" ");
            s.Append(nomeArquivoSaida.Trim().Replace(" ",string.Empty));
            s.Append(".avi");

            return s.ToString();
        }

        private void btnLimpar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtArquivoEntrada.Text = string.Empty;
            txtArquivoSaida.Text = string.Empty;
            msgErro.Text = string.Empty;
            msgSucesso.Text = string.Empty;
        }

        private void btnLimpar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            txtArquivoEntrada.Text = string.Empty;
            txtArquivoSaida.Text = string.Empty;
            msgErro.Text = string.Empty;
            msgSucesso.Text = string.Empty;
        }

        private bool ValidarPreenchimentoCampos(string entrada, string saida)
        {
            if(string.IsNullOrEmpty(entrada) && string.IsNullOrEmpty(saida))
            {
                msgErro.Text = "Arquivo Entrada e Arquivo Saída não preenchidos.";
                msgSucesso.Text = string.Empty;

                msgSucesso.Visibility = Visibility.Collapsed;
                msgErro.Visibility = Visibility.Visible;

                return false;
            }
            else if (string.IsNullOrEmpty(entrada) && !string.IsNullOrEmpty(saida))
            {
                msgErro.Text = "Arquivo Entrada não preenchido.";
                msgSucesso.Text = string.Empty;

                msgSucesso.Visibility = Visibility.Collapsed;
                msgErro.Visibility = Visibility.Visible;

                return false;
            }
            else if(!string.IsNullOrEmpty(entrada) && string.IsNullOrEmpty(saida))
            {
                msgErro.Text = "Arquivo Saída não preenchido.";
                msgSucesso.Text = string.Empty;

                msgSucesso.Visibility = Visibility.Collapsed;
                msgErro.Visibility = Visibility.Visible;

                return false;
            }
            else
            {
                msgErro.Text = string.Empty;
                msgSucesso.Text = string.Empty;
                return true;
            }
        }
    }
}
