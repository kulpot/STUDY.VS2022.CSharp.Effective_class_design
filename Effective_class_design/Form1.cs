using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//ref link:https://www.youtube.com/watch?v=r-J6soFc-kw&list=PLAIBPfq19p2EJ6JY0f5DyQfybwBGVglcR&index=72
//Some intermediate tips on effective class design

namespace Effective_class_design
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Progress progress = new Progress();
            bool b = progress.IsComplete;
        }
    }
}
