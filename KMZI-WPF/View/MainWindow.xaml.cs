namespace KMZI_WPF
{
    using System;
    using System.IO;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string KeyErrorMessage = "The key must be with unique symbols!";

        private string message;
        private string key;
        private string cryptogram;

        public MainWindow()
        {
            InitializeComponent();
            this.message = string.Empty;
            this.key = string.Empty;
            this.cryptogram = string.Empty;
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            this.message = this.tbMessage.Text;
            this.key = this.tbMessageKey.Text;
            this.cryptogram = Logic.Encrypt.CryptMessage(message, key);

            if (this.cryptogram == "Error")
            {
                MessageBox.Show(KeyErrorMessage);
            }
            else
            {
                this.lbEncryptedMessage.Items.Add(cryptogram);
            }

            SaveTextFile(message, key, cryptogram, @"\KMZI-Cryptogram.txt");
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            this.cryptogram = this.tbCryptogram.Text;
            this.key = this.tbCryptogramKey.Text;
            this.message = Logic.Decrypt.DeCryptMessage(cryptogram, key);

            if (this.message == "Error")
            {
                MessageBox.Show(KeyErrorMessage);
            }
            else
            {
                this.lbDecryptedMessage.Items.Add(message);
            }

            SaveTextFile(cryptogram, key, message, @"\KMZI-Message.txt");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.tbMessage.Text = "";
            this.tbMessageKey.Text = "";
            this.tbCryptogram.Text = "";
            this.tbCryptogramKey.Text = "";
            this.lbEncryptedMessage.Items.Clear();
            this.lbDecryptedMessage.Items.Clear();
        }

        private void SaveTextFile(string input, string key, string output, string path)
        {
            // Save textfile
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                using (StreamWriter outfile = new StreamWriter(mydocpath + path))
                {
                    outfile.WriteLine("Input data: " + input);
                    outfile.WriteLine("Key:" + key);
                    outfile.WriteLine("Output data: " + output);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }
    }
}
