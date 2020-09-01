using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Vinegere
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[][] VinegereTable;
        private bool TextToEncryptTextBoxIndicator, SecretEncryptionKeyTextBoxIndicator, TextToDecryptTextBoxIndicator, SecretDecryptKeyTextBoxIndicator;
        public MainWindow()
        {
            InitializeComponent();
            this.VinegereTable = new char[26][];
            TextToEncryptTextBoxIndicator = false;
            SecretEncryptionKeyTextBoxIndicator = false;
            TextToDecryptTextBoxIndicator = false;
            SecretDecryptKeyTextBoxIndicator = false;
            char z = 'A';
            for(int i=0;i<26;i++)
            {
                VinegereTable[i] = new char[26];
                z = (char)('A' + i);
                for(int j=0;j<26;j++)
                {
                    VinegereTable[i][j] = z;
                    z++;
                    if (z == '[') z = 'A';
                }
            }
        }

        private void encryptButton_Click(object sender, RoutedEventArgs e)
        {
            Encode(textToEncryptTextBox.Text, secretEncryptionKeyTextBox.Text);
        }

        private void textToEncodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtonIfBothTextBoxesContainLetters((TextBox)sender, ref TextToEncryptTextBoxIndicator, ref SecretEncryptionKeyTextBoxIndicator, encryptButton);
        }
        private void secretEncryptionKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtonIfBothTextBoxesContainLetters((TextBox)sender, ref SecretEncryptionKeyTextBoxIndicator, ref TextToEncryptTextBoxIndicator, encryptButton);
        }
        private void loadPlainTextButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFromFile(textToEncryptTextBox, "ToEncrypt.txt");
        }

        private void savePlainTextButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile(textToEncryptTextBox, "ToEncrypt.txt");
        }

        private void loadEncryptionKeyButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFromFile(secretEncryptionKeyTextBox, "SecretKey.txt");
        }
        private void saveEncryptionKeyButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile(secretEncryptionKeyTextBox, "SecretKey.txt");
        }

        private void saveEncryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile(encryptedTextTextBox, "ToDecrypt.txt");
        }

        private void Encode(String plainText, String secretKey)
        {
            plainText = plainText.ToUpper();
            secretKey = secretKey.ToUpper();
            StringBuilder stringBuilder = new StringBuilder();
            plainText = Regex.Replace(plainText, @"[^A-Z \n]+", "");
            int plainTextLength = plainText.Length;
            secretKey = Regex.Replace(secretKey, @"[^A-Z]+", "");
            while (stringBuilder.Length < plainTextLength)
            {
                stringBuilder.Append(secretKey);
            }
            for (int i = 0; i < plainTextLength; i++)
            {
                if (plainText[i] == ' ') stringBuilder.Insert(i, ' ');
                if(plainText[i]== '\n') stringBuilder.Insert(i, '\n');
            }
            stringBuilder.Remove(plainTextLength, stringBuilder.Length - plainTextLength);
            secretKey = stringBuilder.ToString();
            stringBuilder.Clear();
            for (int i = 0; i < plainTextLength; i++)
            {
                if (plainText[i] == ' ') stringBuilder.Append(' ');
                if (plainText[i] == '\n') stringBuilder.Append('\n');
                else if(plainText[i] != ' ' && plainText[i] != '\n')
                {
                    stringBuilder.Append(VinegereTable[plainText[i] - 65][secretKey[i] - 65]);
                }
            }
            encryptedTextTextBox.Text = stringBuilder.ToString();
        }
        private void decryptButton_Click(object sender, RoutedEventArgs e)
        {
            Decode(textToDecryptTextBox.Text, secretDecryptionKeyTextBox.Text);
        }
        private void textToDecryptTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtonIfBothTextBoxesContainLetters((TextBox)sender, ref TextToDecryptTextBoxIndicator, ref SecretDecryptKeyTextBoxIndicator, decryptButton);
        }

        private void secretDecryptionKeyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButtonIfBothTextBoxesContainLetters((TextBox)sender, ref SecretDecryptKeyTextBoxIndicator, ref TextToDecryptTextBoxIndicator, decryptButton);
        }


        private void loadEncryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFromFile(textToDecryptTextBox, "ToDecrypt.txt");
        }

        private void loadDecryptionKeyButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFromFile(secretDecryptionKeyTextBox, "SecretKey.txt");
        }

        private void saveDecryptionKeyButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile(secretDecryptionKeyTextBox, "SecretKey.txt");
        }

        private void saveDecryptedTextButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile(decryptedTextTextBox, "Decrypted.txt");
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Program has ability to encrypt and decrypt text using Vinegere cipher. All letters outside of ASCII scope will be removed and reslut will be displayed in all CAPS.\n"
    + "The Vigener cipher is based on matching each plaintext letter with the corresponding letter in a two-dimensional array filled with alphabet with Caesar shift corresponding to the row/column number. The matching letter from the 'intersection' of the plaintext letter and the key letter is the encrypted one.\n"
    + "The program allows you to load and save text from files in the program folder. NOTE - the encrypted text from the Encryption tab is saved to the same file as the plain text from the Decryption tab.\n"
    + "Passwords from both tabs are also saved to the same files. This solution was chosen to facilitate the testing of the program operation");
        }

        private void Decode(String encodedString, String secretKey)
        {
            encodedString = encodedString.ToUpper();
            secretKey = secretKey.ToUpper();
            StringBuilder stringBuilder = new StringBuilder();
            encodedString = Regex.Replace(encodedString, @"[^A-Z \n]+", "");
            int encodedStringLength = encodedString.Length;
            secretKey = Regex.Replace(secretKey, @"[^A-Z]+", "");
            while (stringBuilder.Length < encodedStringLength)
            {
                stringBuilder.Append(secretKey);
            }
            for (int i = 0; i < encodedStringLength; i++)
            {
                if (encodedString[i] == ' ') stringBuilder.Insert(i, ' ');
                if (encodedString[i] == '\n') stringBuilder.Insert(i, '\n');
            }
            secretKey = stringBuilder.ToString();
            stringBuilder.Clear();
            int secretKeyIndex = 0, encodedStringIndex = 0;
            for (int j = 0; j < encodedStringLength; j++)
            {
                if (secretKey[j] == ' ') stringBuilder.Append(' ');
                else if (secretKey[j] == '\n') stringBuilder.Append('\n');
                else
                {
                    secretKeyIndex = Array.IndexOf(VinegereTable[0], secretKey[j]);
                    for (int i = 0; i < 26; i++)
                    {
                        if (VinegereTable[i][secretKeyIndex] == encodedString[j]) encodedStringIndex = i;
                    }
                    stringBuilder.Append(VinegereTable[encodedStringIndex][0]);
                }
            }
            decryptedTextTextBox.Text = stringBuilder.ToString();
        }
        private void LoadFromFile(TextBox textBox, String filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            StringBuilder stringBuilder = new StringBuilder();
            String line = streamReader.ReadLine();
            while (line != null)
            {
                stringBuilder.Append(line);
                stringBuilder.Append('\n');
                line = streamReader.ReadLine();
            }
            streamReader.Close();
            textBox.Text = stringBuilder.ToString();
        }
        private void SaveToFile(TextBox textBox, String filePath)
        {
            StreamWriter streamWriter = new StreamWriter(filePath);
            if (textBox.LineCount > 0)
            {
                for (int i = 0; i < textBox.LineCount; i++)
                {
                    streamWriter.Write(textBox.GetLineText(i));
                }
            }
            streamWriter.Close();
        }
        private void EnableButtonIfBothTextBoxesContainLetters(TextBox textBoxToCheck, ref bool textBoxToCheckIndicator, ref bool secondTextBoxIndicator, Button button)
        {
            textBoxToCheckIndicator = Regex.Match(textBoxToCheck.Text, @"[A-Za-z]").Success;
            if (textBoxToCheckIndicator && secondTextBoxIndicator) button.IsEnabled = true;
            else button.IsEnabled = false;
        }
    }
}
