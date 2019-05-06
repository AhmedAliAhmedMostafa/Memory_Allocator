using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign
{
    public partial class Gant_chart : Form
    {
      
        
        //Pen outer = new Pen(Color.Black,1);
        Pen Pen_hole = new Pen(Color.Green);
        Pen pen_old = new Pen(Color.Gray);
        Pen pen_new = new Pen(Color.Red);
        Font font_data = new Font(FontFamily.GenericMonospace, 10);
        Font font_adress = new Font(FontFamily.GenericSerif, 10);
        Brush brush_adress=new SolidBrush(Color.Red);
        Brush brush_data = new SolidBrush(Color.Blue);
        //float outer_width = 2;
        int memory_width =40;
        float memory_height=220;
        float step = 220/ (float)Form1.memory_max_size;
        int num_adress_digits=(Form1.memory_max_size.ToString()).Length;
        int digit_width=2;
        int digit_height = 4;
        float starting_x;
        float starting_y;
        float w, h;
        Point genesis_point;
        List<int> old_start=new List<int>();
        List<int> old_size=new List<int>();
        List<int> new_start=new List<int>();
        List<int> new_size=new List<int>();
        SortedDictionary<int, KeyValuePair<char, int>> all = new SortedDictionary<int, KeyValuePair<char, int>>();
        public Gant_chart()
        {
            InitializeComponent();


        }
        public string format(string d)
        {
            int l = num_adress_digits-d.Length;
            string n="";
            for (int i = 0; i < l; i++)
            { n += "0"; }
            return n + d;
               

        }
        public Point Draw_Area(Graphics g, char type, Point genesis_point,int start,int size,string data)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            float width=memory_width;
            float height=size*step;
            float y = genesis_point.Y;
            float x = genesis_point.X;
            genesis_point.Y+=height;
            float adress_x = starting_x - num_adress_digits * digit_width;
            float adress_y ;
            int len=data.Length;
            float data_x=starting_x+memory_width/2-(len/2*digit_width);
            float data_y=starting_y+y+height/2;
            start = int.Parse( format(start.ToString()));

            
            switch (type)
            {
                case'h':
                    g.DrawRectangle(Pen_hole, x, y, width,height);
                    g.DrawString(start.ToString(),font_adress,brush_adress,adress_x,adress_y);
                    g.DrawString(data,font_data,brush_data,data_x,data_y);
                    break;
                case'o':
                    g.DrawRectangle(pen_old, x, y, width,height);
                    g.DrawString(start.ToString(),font_adress,brush_adress,adress_x,adress_y);
                    g.DrawString(data,font_data,brush_data,data_x,data_y);
                    break;
                case'n':
                    g.DrawRectangle(pen_new, x, y, width,height);
                    g.DrawString(start.ToString(),font_adress,brush_adress,adress_x,adress_y);
                    g.DrawString(data,font_data,brush_data,data_x,data_y);
                    break;
                    
            }
        }
        public void sort()
        { 
            int total=Form1.holes_num+old_start.Count+new_start.Count;

            for(int i=0;i<Form1.holes_num;i++)
            {
                all.Add(Form1.holes_start[i],new KeyValuePair<char,int>('h',i));
            }
             for(int i=0;i<old_start.Count;i++)
            {
                all.Add(old_start[i], new KeyValuePair<char, int>('o', i));
            }
            for(int i=0;i<new_start.Count;i++)
            {
                all.Add(new_start[i],new KeyValuePair<char,int>('n',i));
            }

        }
        public void Redraw(Graphics g)
        {
            g.Clear(Color.White);
            g.PageUnit = GraphicsUnit.Millimeter;
            g.DrawRectangle(outer, starting_x, starting_y, memory_width, memory_height);
            ////////////////Holes/////////////////////////////////
          
       
                for (int i = 0; i < Form1.holes_num; i++)
                {
                    Draw_Area(g, 'h', Form1.holes_start[i], Form1.holes_size[i], "Hole" + i.ToString());
                }
            ////////////////Old///////////////////////////////////
                for (int j = 0; j < old_start.Count; j++)
                {
                    Draw_Area(g, 'o', old_start[j],old_size[j],"Old"+j.ToString());
                }
                ////////////////new///////////////////////////////////
                for (int k = 0; k< new_start.Count; k++)
                {
                    Draw_Area(g, 'n',new_start[k], new_size[k], "x" + k.ToString());
                }
            
        }
        public void get_old()
        {
           
            int itr=0;
            foreach(KeyValuePair<int,int>p in Form1.sortedHoles)
            {
                if (itr < p.Key)
                {
                    old_start.Add(itr);
                    old_size.Add(p.Key - itr);
                    itr = p.Key + p.Value;
                }
                else
                {
                    itr = p.Key + p.Value;
                }
            }
            if (itr != Form1.memory_max_size - 1)
            {
                old_start.Add(itr);
                old_size.Add((Form1.memory_max_size) - itr);
            }
        }


        private void Gant_chart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            starting_x =130;
            starting_y = 0;
            get_old();
            Redraw(g);
        }






    }
}
