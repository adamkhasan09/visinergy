using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visinergy
{
    public partial class Form1 : Form
    {

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        string alphabet;
        char[] letters;
        public Form1()
        {
            InitializeComponent();
            int f_letter = (int)'a';
            int l_letter = (int)'z';
            string numbers = "0123456789";
            int diap_count = l_letter - f_letter;
            letters = new char[diap_count + 1 + numbers.Length];
            for (int i = 0; i < diap_count + 1; i++)
            {
                int num = f_letter + i;
                letters[i] = (char)num;
            }
            int j = 0;
            for (int i = diap_count + 1; i < diap_count + 1 + numbers.Length; i++)
            {
                letters[i] = numbers[j];
                j++;
            }
            alphabet = new string(letters);
         
        }

       
       
        static int[] indexSearch(string key, string alphabet)
        {
            int[] indxKey = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (key[i] == alphabet[j])
                    {
                        indxKey[i] = j;
                    }
                }
            }
            return indxKey;
        }
        static string[] key_words_create(char[] letters, string key, int[] indxKey)
        {
            string[] encryption_alphabet_from_key = new string[key.Length];
            char[] letters_save = new char[letters.Length];
            for (int i = 0; i < encryption_alphabet_from_key.Length; i++)
            {

                for (int j = 0; j < letters.Length; j++)
                {
                    int index = Math.Abs(j + indxKey[i]);
                    if (index > letters.Length - 1)
                    {
                        index = Math.Abs(j + indxKey[i] - letters.Length);
                    }
                    letters_save[j] = letters[index];
                }
                encryption_alphabet_from_key[i] = new string(letters_save);
                Console.WriteLine(encryption_alphabet_from_key[i]);
            }
            return encryption_alphabet_from_key;
        }
        static string getEncyWord(string message, string[] encryption_alphabet_from_key, string alphabet)
        {
            char[] word_char = new char[message.Length];
            int enAlCount = encryption_alphabet_from_key.Count();
            int cycleCount = 0;
            int[] idxmsg = indexSearch(message, alphabet);
            string enWord;
            for (int i = 0; i < idxmsg.Length; i++)
            {
                word_char[i] = encryption_alphabet_from_key[cycleCount][idxmsg[i]];
                cycleCount++;
                if (cycleCount == encryption_alphabet_from_key.Count() - 1)
                {
                    cycleCount = 0;
                }
            }
            enWord = new string(word_char);
            return enWord;
        }
        static string getTrancWord(string encyWord, string[] encryption_alphabet_from_key, string alphabet)
        {
            int[] keyIdx = new int[encyWord.Length];
            int cycleCount = 0;
            for (int i = 0; i < encyWord.Length; i++)
            {

                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (encryption_alphabet_from_key[cycleCount][j] == encyWord[i])
                    {
                        keyIdx[i] = j;
                    }

                }
                cycleCount++;
                if (cycleCount == encryption_alphabet_from_key.Count() - 1)
                {
                    cycleCount = 0;
                }

            }
            char[] symbols = new char[keyIdx.Length];
            for (int i = 0; i < keyIdx.Length; i++)
            {
                symbols[i] = alphabet[keyIdx[i]];

            }
            string transWord = new string(symbols);
            return transWord;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string message = textBox1.Text;
            string key = textBox2.Text;
            int[] indxKey = indexSearch(key, alphabet);
            string[] encryption_alphabet_from_key = key_words_create(letters, key, indxKey);
            string encyWord = getEncyWord(message, encryption_alphabet_from_key, alphabet);
            textBox3.Text = encyWord;
            listBox1.Items.AddRange(encryption_alphabet_from_key);
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            string key = textBox5.Text;
            int[] indxKey = indexSearch(key, alphabet);
            string[] encryption_alphabet_from_key = key_words_create(letters, key, indxKey);
            string encyWord = textBox4.Text;
            string trands_word = getTrancWord(encyWord, encryption_alphabet_from_key, alphabet);
            textBox6.Text = trands_word;
            listBox1.Items.AddRange(encryption_alphabet_from_key);
        }
    }
}
