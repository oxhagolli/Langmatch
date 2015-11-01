using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Langmatch_for_Windows_Desktop
{
    public partial class Game : Form
    {
        string language, mode, topic;
        bool[] roll = { false, false, false, false, false,
                        false, false, false, false, false };
        PictureBox[] boxes;

        Dictionary<string, string> albanian;
        Dictionary<string, string> italian;
        Dictionary<string, string> french;
        Dictionary<string, string> japanese;

        Dictionary<Bitmap, string> mapped;
        int n;

        public Game(string language, string mode, string topic)
        {
            InitializeComponent();
            this.language = language.ToLower();
            this.mode = mode;
            this.topic = topic;
        }

        private void Game_Load(object sender, EventArgs e)
        {
            PictureBox[] temp = { pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, 
                                  pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11 };
            boxes = temp;
            label2.Text = topic + " Masteries:";
            for (int i = 0; i < 10; i++)
            {
                roll[i] = false;
                boxes[i].Image = Properties.Resources._empty;
            }
            pictureBox12.Image = Properties.Resources._empty;
            mapped = new Dictionary<Bitmap, string>();
            populate();
            puzzle();
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void loss()
        {
            for (int i = 0; i < 10; i++)
            {
                roll[i] = false;
                boxes[i].Image = Properties.Resources._empty;
            }
            pictureBox12.Image = Properties.Resources._cross;
        }

        private void win()
        {
            for (int i = 0; i < 10; i++)
            {
                if(roll[i] == false)
                {
                    roll[i] = true;
                    boxes[i].Image = Properties.Resources._check;
                    break;
                }
            }
            pictureBox12.Image = Properties.Resources._check;
            if(roll[9])
            {
                MessageBox.Show("Congratulations! You completed this level and can consider it mastered. Choose another topic.");
                this.Close();
            }
        }

        private void populate()
        {
            albanian = new Dictionary<string, string>();
            albanian.Add("soccer", "futboll");
            albanian.Add("basketball", "basketboll");
            albanian.Add("swimming", "not");
            albanian.Add("golf", "golf");
            albanian.Add("tennis", "tenis");

            french = new Dictionary<string, string>();
            french.Add("soccer", "le football");
            french.Add("basketball", "le basket");
            french.Add("swimming", "la natation");
            french.Add("golf", "le golf");
            french.Add("tennis", "le tennis");

            italian = new Dictionary<string, string>();
            italian.Add("soccer", "il calcio");
            italian.Add("basketball", "la pallacanestro");
            italian.Add("swimming", "il nuoto");
            italian.Add("golf", "il golf");
            italian.Add("tennis", "il tennis");

            japanese = new Dictionary<string, string>();
            japanese.Add("soccer", "サッカー");
            japanese.Add("basketball", "バスケットボール");
            japanese.Add("swimming", "水泳");
            japanese.Add("golf", "ゴルフ");
            japanese.Add("tennis", "テニス");

            ResourceSet rs = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in rs)
            {
                if (!entry.Key.ToString().Contains(topic + "_"))
                    continue;
                mapped.Add((Bitmap)entry.Value, translate(entry.Key.ToString().ToLower().Split('_')[1]));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Whoops. Please put in some text!");
                return;
            }
            string text = textBox1.Text.ToLower();
            string answer;
            mapped.TryGetValue(mapped.Keys.ElementAt<Bitmap>(n), out answer);
            answer = answer.ToLower();
            if(text.Split(' ').Count<string>() < 2)
            {
                if(answer.Split(' ').Count<string>() < 2)
                {
                    if (answer == text)
                        win();
                    else loss();
                }
                else
                {
                    if (text == answer.Split(' ')[1])
                        win();
                    else loss();
                }
            }
            else
            {
                if (answer.Split(' ').Count<string>() < 2)
                {
                    loss();
                }
                else
                {
                    if (text == answer)
                        win();
                    else loss();
                }
            }
            puzzle();
            textBox1.Text = "";
        }

        private string translate(string input)
        {
            string str;
            if (language.Contains("albanian"))
                albanian.TryGetValue(input, out str);
            else if (language.Contains("italian"))
                italian.TryGetValue(input, out str);
            else if (language.Contains("italian"))
                french.TryGetValue(input, out str);
            else if (language.Contains("italian"))
                japanese.TryGetValue(input, out str);
            else
                str = "NULL";
            return str;
        }

        private void puzzle()
        {
            Random i = new Random();
            n = i.Next(mapped.Keys.Count);
            pictureBox1.Image = mapped.Keys.ElementAt<Bitmap>(n);
        }
    }
}
