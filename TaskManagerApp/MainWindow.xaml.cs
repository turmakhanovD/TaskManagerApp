using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManagerApp.TaskManagerApp.DataAccess;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using TaskManagerApp.DataAccess;

namespace TaskManagerApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskManagerApp.DataAccess.Task newTask = new TaskManagerApp.DataAccess.Task();
        static MailAddress fromAddress = new MailAddress("dinislam.turmakhan@gmail.com", "do not reply");     
        const string fromPassword = "123Islam";

        SmtpClient smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };



        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendingEmailSelected(object sender, RoutedEventArgs e)
        {
            actionDescription.Visibility = Visibility.Visible;
            actionDescription.Text = "Получатель:";
            actionTextBox.Visibility = Visibility.Visible;
            directoryChoose.Visibility = Visibility.Collapsed;
            directoryChoose1.Visibility = Visibility.Collapsed;
            actionTextBoxReplace.Visibility = Visibility.Visible;
            actionDescriptionReplace.Text = "Message:";
            actionDescriptionReplace.Visibility = Visibility.Visible;
            newTask.TypeTask = TaskType.Email;
            //newTask.EmailAddress = actionTextBox.Text;
        }

        private void DownloadSelected(object sender, RoutedEventArgs e)
        {
            actionDescription.Visibility = Visibility.Visible;
            actionDescription.Text = "Ссылка:";
            actionTextBox.Visibility = Visibility.Visible;
            directoryChoose.Visibility = Visibility.Collapsed;
            directoryChoose1.Visibility = Visibility.Collapsed;
            actionTextBoxReplace.Visibility = Visibility.Collapsed;
            actionDescriptionReplace.Visibility = Visibility.Collapsed;
            newTask.TypeTask = TaskType.Download;
          //  newTask.DownloadDirectory = actionTextBox.Text;
        }

        private void ReplaceSelected(object sender, RoutedEventArgs e)
        {
            actionDescription.Visibility = Visibility.Visible;
            actionDescription.Text = "Путь к файлу:";
            actionTextBox.Visibility = Visibility.Visible;
            actionTextBoxReplace.Visibility = Visibility.Visible;
            actionDescriptionReplace.Visibility = Visibility.Visible;
            actionDescriptionReplace.Text = "Переместить в";
            directoryChoose.Visibility = Visibility.Visible;
            directoryChoose1.Visibility = Visibility.Visible;
            newTask.TypeTask = TaskType.ReplaceCatalog;
        }

        private void directoryChooseClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            newTask.From = fileDialog.FileName;
            actionTextBox.Text = fileDialog.FileName;
            newTask.From = fileDialog.FileName;
        }

        private void directoryChoose1Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();
            newTask.To = fileDialog.SelectedPath;
            actionTextBoxReplace.Text = fileDialog.SelectedPath;
            FileInfo file = new FileInfo(newTask.From);
            string name = file.Name; 
            string ext = file.Extension; 
            newTask.To = fileDialog.SelectedPath + "\\" + name + "." + ext;
           
        }

        private void CreateNewTask(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) || !datePicker.SelectedDate.HasValue || typeComboBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Fill all blanks!", "Error!");
                return;
            }
            else
            {

                newTask.TaskName = textBox.Text;
                newTask.Date = datePicker.SelectedDate;
                if (newTask.TypeTask == TaskType.Email)
                {
                    newTask.EmailAddress = actionTextBox.Text;
                    newTask.Message = actionTextBoxReplace.Text;
                    //Thread thread = new Thread(SendMessage);
                    //thread.Start();

                }
                else if (newTask.TypeTask == TaskType.Download)
                {
                    newTask.DownloadDirectory = actionTextBox.Text;
                    //Timer timer = new System.Threading.Timer(Download,null,)

                }

                var thread = ThreadPool.QueueUserWorkItem(SaveToDB);
               


            }
        }

        private void MoveFile()
        {
            File.Move(newTask.From, newTask.To);
        }

        private void SendMessage()
        {
            using (var message = new MailMessage(fromAddress.ToString(), newTask.EmailAddress)
            {
                Subject = newTask.TaskName,
                Body = newTask.Message
            })
            {
                smtp.Send(message);
            }
        }

        private void SaveToDB(object obj)
        {
            using (var context = new TaskContext())
            {
                context.Tasks.Add(newTask);
                context.SaveChanges();
            }
        }

        private void Download()
        {
            Uri address = new Uri(newTask.DownloadDirectory);
            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(address, @"C:\Users\ТурмаханД\Downloads\cs16.exe");
        }

        
    }
}
