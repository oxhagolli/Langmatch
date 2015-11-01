using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Langmatch_for_Windows_Desktop
{
    public partial class Game : Form
    {
        string language, mode, topic;
        bool[] roll = {false, false, false, false, false,
                        false, false, false, false, false};

        public Game(string language, string mode, string topic)
        {
            this.language = language;
            this.mode = mode;
            this.topic = topic;
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void loss()
        {
            for (int i = 0; i < 10; i++)
                roll[i] = false;
        }

        private void win()
        {
            for (int i = 0; i < 10; i++)
            {
                if(roll[i] == false)
                {
                    roll[i] = true;
                    break;
                }
            }
        }


    }
}
