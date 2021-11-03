using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using project_05;

namespace GuessTheWord
{
    public delegate void CheckLetter(string letter);
    
    public partial class Form6 : Form
    {
        int  MAX_NUMBER_OF_CHANCE = 5;
        event CheckLetter ChkLtr;

        string input;
        string missedLetters = "";
        
        string wordToFind= "";
        
        string wordToDisplay = "";
        
        char[] wordToFindArray;
        int[] wordToFindLettersPosition;
        bool IsLetterFound = false;

        Random rndm = new Random();
        
        List<string> wordList = new List<string>();
        
        List<int> wordsIndexAlreadyPlayed = new List<int>();

        int missedLetterCount = 0;


        public Form6()
        {
            InitializeComponent();
            this.ChkLtr += new CheckLetter(Form6_ChkLtr);          
            CreateWordList();
            RestartTheGame();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MAX_NUMBER_OF_CHANCE = 10;
            label_MissedLtrCnt.Text = 10.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MAX_NUMBER_OF_CHANCE = 5;
            label_MissedLtrCnt.Text = 5.ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            MAX_NUMBER_OF_CHANCE = 3;
            label_MissedLtrCnt.Text = 3.ToString();
        }

        private void CreateWordList()
        {
            wordList.Add("Banana");
            wordList.Add("Apple");
            wordList.Add("Grapes");
            wordList.Add("Peach");
            wordList.Add("Orange");
            wordList.Add("Mango");
            wordList.Add("Kiwi");
            wordList.Add("Plum");
            wordList.Add("Watermelon");
            wordList.Add("Pineapple");
            wordList.Add("Cherry");
            wordList.Add("Melon");
            wordList.Add("Grapefruit");
            wordList.Add("Pear");
            wordList.Add("Raspberry");
            wordList.Add("Strawberry");
            wordList.Add("Papaya");
            wordList.Add("Coconut");
        }

        private string GetNewWordFromPool()
        {
            bool IsNewWord = false;
            string temp = "HANGMAN";
            
            try
            {
                while (IsNewWord == false)
                {
                    int index = rndm.Next();

                    index = index % wordList.Count;

                    if (!wordsIndexAlreadyPlayed.Exists(e => e == index))
                    {
                        temp = wordList[index];
                        wordsIndexAlreadyPlayed.Add(index);
                        IsNewWord = true;
                        break;
                    }
                    else
                    {
                        IsNewWord = false;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return temp.ToUpper();
        }

        private void RestartTheGame()
        {
            try
            {
                wordToFind = GetNewWordFromPool();
                wordToFind = wordToFind.ToUpper();
                wordToFindArray = wordToFind.ToCharArray();

                wordToFindLettersPosition = new int[wordToFind.Length];

                input = "";
                wordToDisplay = "";
                for (int i = 0; i < wordToFind.Length; i++)
                {
                    wordToDisplay += "-";
                }

                missedLetters = "";
                missedLetterCount = 0;

                label_Word.Text = wordToDisplay.ToUpper();
                label_MissedLetters.Text = missedLetters;
                label_MissedLtrCnt.Text = MAX_NUMBER_OF_CHANCE.ToString();
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void Form6_ChkLtr(string currentInputletter)
        {
            try
            {
                if (missedLetterCount < MAX_NUMBER_OF_CHANCE)
                {

                    IsLetterFound = false;
                    for (int i = 0; i < wordToFindArray.Length; i++)
                    {
                        if (currentInputletter[0] == wordToFindArray[i])
                        {
                            wordToFindLettersPosition[i] = 1;
                            IsLetterFound = true;
                        }
                    }

                    if (IsLetterFound == false)
                    {
                        missedLetters += currentInputletter + ", ";
                        missedLetterCount++;
                        label_MissedLtrCnt.Text = (MAX_NUMBER_OF_CHANCE - missedLetterCount).ToString();
                    }

                    wordToDisplay = "";
                    for (int i = 0; i < wordToFindArray.Length; i++)
                    {
                        if (wordToFindLettersPosition[i] == 1)
                        {
                            wordToDisplay += wordToFindArray[i].ToString();
                        }
                        else
                        {
                            wordToDisplay += "-";
                        }
                    }

                    label_Word.Text = wordToDisplay.ToUpper();
                    label_MissedLetters.Text = missedLetters;

                    if (wordToFindLettersPosition.All(e => e == 1))
                    {
                        string login_id = Login.login_id;
                        int login_count = Login.login_count + 1;
                        int result_count = Login.result_count + 1;

                        User user = DataManager.Users.Single(x => x.Id == login_id);
                        user.Count = login_count;

                        User result = DataManager.Results.Single(x => x.Id == login_id);
                        result.Count = result_count;

                        DataManager.Save();

                        MessageBox.Show("승리하셨습니다.\n\n" + login_id + "는 " + login_count + "번 승리", "RESULT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RestartTheGame();
                    }
                }
                else
                {
                    string login_id = Login.login_id;
                    User user = DataManager.Users.Single(x => x.Id == login_id);
                    string id = user.Id;
                    int count = user.Count;
                    MessageBox.Show("패배하셨습니다.\n" + "\n정답은: " + wordToFind + "\n\n" + id + "는 " + count + "번 승리", "RESULT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestartTheGame();
                }
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        #region Getting Alphabets
        
        private void button_A_Click(object sender, EventArgs e)
        {
            input = "A"; 
            
            ChkLtr(input);
        }

        private void button_B_Click(object sender, EventArgs e)
        {
            input = "B";
            
            ChkLtr(input);
        }

        private void button_C_Click(object sender, EventArgs e)
        {
            input = "C";
            
            ChkLtr(input);
        }

        private void button_D_Click(object sender, EventArgs e)
        {
            input = "D";
            
            ChkLtr(input);
        }

        private void button_E_Click(object sender, EventArgs e)
        {
            input = "E";
            
            ChkLtr(input);
        }

        private void button_F_Click(object sender, EventArgs e)
        {
            input = "F";
            
            ChkLtr(input);
        }

        private void button_G_Click(object sender, EventArgs e)
        {
            input = "G";
            
            ChkLtr(input);
        }

        private void button_H_Click(object sender, EventArgs e)
        {
            input = "H";
            
            ChkLtr(input);
        }

        private void button_I_Click(object sender, EventArgs e)
        {
            input = "I";
            
            ChkLtr(input);
        }

        private void button_J_Click(object sender, EventArgs e)
        {
            input = "J";
            
            ChkLtr(input);
        }

        private void button_K_Click(object sender, EventArgs e)
        {
            input = "K";
            
            ChkLtr(input);
        }

        private void button_L_Click(object sender, EventArgs e)
        {
            input = "L";
            
            ChkLtr(input);
        }

        private void button_M_Click(object sender, EventArgs e)
        {
            input = "M";
            
            ChkLtr(input);
        }

        private void button_N_Click(object sender, EventArgs e)
        {
            input = "N";
            
            ChkLtr(input);
        }

        private void button_O_Click(object sender, EventArgs e)
        {
            input = "O";
            
            ChkLtr(input);
        }

        private void button_P_Click(object sender, EventArgs e)
        {
            input = "P";
            
            ChkLtr(input);
        }

        private void button_Q_Click(object sender, EventArgs e)
        {
            input = "Q";
            
            ChkLtr(input);
        }

        private void button_R_Click(object sender, EventArgs e)
        {
            input = "R";
            
            ChkLtr(input);
        }

        private void button_S_Click(object sender, EventArgs e)
        {
            input = "S";
            
            ChkLtr(input);
        }

        private void button_T_Click(object sender, EventArgs e)
        {
            input = "T";
            
            ChkLtr(input);
        }

        private void button_U_Click(object sender, EventArgs e)
        {
            input = "U";
            
            ChkLtr(input);
        }

        private void button_V_Click(object sender, EventArgs e)
        {
            input = "V";
            
            ChkLtr(input);
        }

        private void button_W_Click(object sender, EventArgs e)
        {
            input = "W";
            
            ChkLtr(input);
        }

        private void button_X_Click(object sender, EventArgs e)
        {
            input = "X";
            
            ChkLtr(input);
        }

        private void button_Y_Click(object sender, EventArgs e)
        {
            input = "Y";
            
            ChkLtr(input);
        }

        private void button_Z_Click(object sender, EventArgs e)
        {
            input = "Z";
            
            ChkLtr(input);
        }
#endregion

        private void button_LoadNewWord_Click(object sender, EventArgs e)
        {
            RestartTheGame();
        }
        private void Form6_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            new Form1().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            new Form4().ShowDialog();
        }

        private void label_Word_Click(object sender, EventArgs e)
        {
        }
    }
}
