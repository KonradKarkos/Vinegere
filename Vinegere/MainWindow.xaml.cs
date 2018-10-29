using System;
using System.Collections.Generic;
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

namespace Vinegere
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] tablica;
        bool pierwszy, drugi, trzeci, czwarty;
        public MainWindow()
        {
            InitializeComponent();
            this.tablica = new int[26, 26];
            pierwszy = false;
            drugi = false;
            trzeci = false;
            czwarty = false;
            int z = 0;
            for(int i=0;i<26;i++)
            {
                z = i;
                for(int j=0;j<26;j++)
                {
                    tablica[i, j] = z;
                    z++;
                    if (z == 26) z = 0;
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            szyfruj(textBox.Text, textBox1.Text);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text.Length > 0 && sprawdz(textBox)) pierwszy = true;
            else pierwszy = false;
            if (drugi && pierwszy) button.IsEnabled = true;
            else button.IsEnabled = false;
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox2.Text.Length > 0 && sprawdz(textBox2)) trzeci = true;
            else trzeci = false;
            if (trzeci && czwarty) button1.IsEnabled = true;
            else button1.IsEnabled = false;
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox3.Text.Length > 0 && sprawdz(textBox3)) czwarty = true;
            else czwarty = false;
            if (trzeci && czwarty) button1.IsEnabled = true;
            else button1.IsEnabled = false;
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            odszyfruj(textBox2.Text, textBox3.Text);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            wczytaj(textBox, "DoZaszyfrowania.txt");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            zapisz(textBox, "DoZaszyfrowania.txt");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            wczytaj(textBox1, "Haslo.txt");
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Length > 0 && sprawdz(textBox1)) drugi = true;
            else drugi = false;
            if(pierwszy && drugi) button.IsEnabled = true;
            else button.IsEnabled = false;
        }
        private void szyfruj(String teks, String hasl)
        {
            String tekst = teks;
            String haslo = hasl;
            tekst = tekst.ToUpper();
            haslo = haslo.ToUpper();
            StringBuilder s = new StringBuilder();
            foreach (char c in tekst)
            {
                if ((c >= 'A' && c <= 'Z') || c == ' ' || c== (char)(10)) s.Append(c);
            }
            tekst = s.ToString();
            int dlugosctekstu = tekst.Length;
            s.Clear();
            foreach (char c in haslo)
            {
                if (c >= 'A' && c <= 'Z') s.Append(c);
            }
            haslo = s.ToString();
            s.Clear();
            while (s.Length < dlugosctekstu)
            {
                s.Append(haslo);
            }
            for (int i = 0; i < dlugosctekstu; i++)
            {
                if (tekst[i] == ' ') s.Insert(i, ' ');
                if(tekst[i]== (char)(10)) s.Insert(i, (char)(10));
            }
            s.Remove(dlugosctekstu, s.Length - dlugosctekstu);
            haslo = s.ToString();
            s.Clear();
            char a;
            int indeks1, indeks2;
            for (int i = 0; i < dlugosctekstu; i++)
            {
                if (tekst[i] == ' ') s.Append(' ');
                if (tekst[i] == (char)(10)) s.Append((char)(10));
                else if(tekst[i] != ' ' && tekst[i] != (char)(10))
                {
                    indeks1 = (int)tekst[i];
                    indeks2 = (int)haslo[i];
                    a = (char)(tablica[indeks1 - 65, indeks2 - 65] + 65);
                    s.Append(a);
                }
            }
            textBox4.Text = s.ToString();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            zapisz(textBox1, "Haslo.txt");
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            zapisz(textBox4, "DoOdszyfrowania.txt");
        }

        private void button8_Click_1(object sender, RoutedEventArgs e)
        {
            zapisz(textBox2, "DoOdszyfrowania.txt");
        }

        private void button7_Click_1(object sender, RoutedEventArgs e)
        {
            wczytaj(textBox2, "DoOdszyfrowania.txt");
        }

        private void button9_Click_1(object sender, RoutedEventArgs e)
        {
            wczytaj(textBox3, "Haslo.txt");
        }

        private void button10_Click_1(object sender, RoutedEventArgs e)
        {
            zapisz(textBox3, "Haslo.txt");
        }

        private void button11_Click_1(object sender, RoutedEventArgs e)
        {
            zapisz(textBox3_Copy, "Odszyfrowany.txt");
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            okienko();
        }

        private void odszyfruj(String teks, String hasl)
        {
            String tekst = teks;
            String haslo = hasl;
            tekst = tekst.ToUpper();
            haslo = haslo.ToUpper();
            StringBuilder s = new StringBuilder();
            foreach (char c in tekst)
            {
                if ((c >= 'A' && c <= 'Z') || c == ' ' || c==(char)(10)) s.Append(c);
            }
            tekst = s.ToString();
            int dlugosctekstu = tekst.Length;
            s.Clear();
            foreach (char c in haslo)
            {
                if (c >= 'A' && c <= 'Z') s.Append(c);
            }
            haslo = s.ToString();
            s.Clear();
            while (s.Length < dlugosctekstu)
            {
                s.Append(haslo);
            }
            for (int i = 0; i < dlugosctekstu; i++)
            {
                if (tekst[i] == ' ') s.Insert(i, ' ');
                if (tekst[i] == (char)(10)) s.Insert(i, (char)(10));
            }
            s.Remove(dlugosctekstu, s.Length - dlugosctekstu);
            haslo = s.ToString();
            s.Clear();
            int indeks1 = 0, indeks2 = 0;
            char a;
            //pętla tworząca odszyfrowany tekst
            for (int j = 0; j < haslo.Length; j++)
            {
                if (haslo[j] == ' ') s.Append(' ');
                if (haslo[j] == (char)(10)) s.Append((char)(10));
                //"szyfrowanie wsteczne"
                else if (haslo[j] != ' ' && haslo[j] != (char)(10))
                {
                    for (int i = 0; i < 26; i++)
                    {
                        //znajdowanie odpowiedniej litery w tablicy
                        if (tablica[0, i] == ((int)haslo[j] - 65))
                        {
                            indeks1 = i;
                            break;
                        }
                    }
                    //znajdowanie indeksu litery z tekstu zaszyfrowanego
                    for (int i = 0; i < 26; i++)
                    {
                        if (tablica[i, indeks1] == ((int)tekst[j] - 65)) indeks2 = i;
                    }
                    a = (char)(tablica[indeks2, 0] + 65);
                    s.Append(a);
                }
            }
            textBox3_Copy.Text = s.ToString();
        }
        private void wczytaj(TextBox tekst, String plik)
        {
            StreamReader sr = new StreamReader(plik);
            StringBuilder s = new StringBuilder();
            String linia = sr.ReadLine();
            char a = (char)(10);
            while (linia != null)
            {
                s.Append(linia);
                s.Append(a);
                linia = sr.ReadLine();
            }
            sr.Close();
            tekst.Text = s.ToString();
        }
        private void zapisz(TextBox tekst, String plik)
        {
            StreamWriter sr = new StreamWriter(plik);
            StringBuilder s = new StringBuilder();
            if (tekst.LineCount > 0)
            {
                for (int i = 0; i < tekst.LineCount; i++)
                {
                    sr.Write(tekst.GetLineText(i));
                }
            }
            sr.Close();
        }
        private void okienko()
        {
            MessageBox.Show("Program polega na szyfrowaniu podanego tekstu za pomocą szyfru Vigener'a. Wszystkie specjalne znaki zostaną usunięte, a tekst wyjściowy będzie podany w capitalikach."+(char)(10)
                +"Szyfr Vigener'a polega na dopasowaniu po kolei każdej litery tekstu jawnego z odpowiadjącą jej literą hasła w tablicy dwuwymiarowej wypełnionej alfabetem z przesunięciem Cezara odpowiadającym numerowi wiersza/kolumny. Dopasowana w ten sposób litera z 'przecięcia' litery tekstu jawnego i litery hasła jest już zaszyfrowana."+(char)(10)
                +"Program pozwala na wczytywanie i zapisywanie tekstu z plików znajdujących się w folderze Debug. UWAGA - tekst zaszyfrowany z zakładki Szyfrowanie jest zapisywany do tego samego pliku co tekst jawny z zakładki Deszyfrowanie."+(char)(10)
                +"Hasła z obu zakładek także są zapisywanie do tych samych plików. Takie rozwiązanie zostało wybrane aby ułatwić testowanie działania programu"+(char)(10)+(char)(10)
                +"Do przekształcania łańcuchów znaków została użyta klasa StringBuilder w celu zminimalizowanie odwołań do danego miejsca w pamięci, których byłoby dużo z uwagi na ilość pętli. Tablica z alfabetami odpowiadającymi kolejnym przesunięciom to tablica liczb oznaczająych kolejną literę alfabetu (mogła to być również tablica char'ów).",
                "Opis");
        }
        private bool sprawdz(TextBox tekst)
        {
            bool prawda = false;
            String s = tekst.Text;
            for(int i=0;i<s.Length;i++)
            {
                if (s[i] != ' ' && s[i] != (char)(10)) prawda = true;
            }
            return prawda;
        }
    }
}
