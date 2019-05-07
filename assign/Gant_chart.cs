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
        Graphics g_global;
        Point genesis_point=new Point(260,0);
        List<int> old_start=new List<int>();
        List<int> old_size=new List<int>();
        SortedDictionary<int,int> new_process=new SortedDictionary<int,int>();
            public struct segment
        {
            public int process_id,start,size;
            public string name;
        }
        public struct process
        {
            public List<segment> Process_seg;
            public int num_seg;
           public process(int num=0)
            {
                Process_seg=new List<segment>();
                num_seg = num;
            }
        }
        List<process>all_new =new List<process>();
        SortedDictionary<int, KeyValuePair<char, int>> all = new SortedDictionary<int, KeyValuePair<char, int>>();
        List<segment> all_seg = new List<segment>();
        public void Allocate_best_fit()
        {

        }
        public void Allocate_first_fit()
        {

        }
        public void Deallocate_old()
        {
        }
        public void Deallocate_new()
        {

        }

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
            g_global = g;
            float width=memory_width;
            float height=size*step;
            float adress_x = genesis_point.X - num_adress_digits * digit_width;
            float adress_y=genesis_point.Y-digit_height/2 ;
            int len=data.Length;
            float data_x=genesis_point.X+memory_width/2-(len/2*digit_width);
            float data_y=genesis_point.Y+height/2;
            start = int.Parse( format(start.ToString()));
            float min_height = 10;
            Point p1=new Point();
            float size1;
            int size2,start1,end;
            if (height < min_height)
            {
                height = min_height;
            }
            if (genesis_point.Y + height > memory_height)
            {
                size1=(memory_height-genesis_point.Y)/step;
                start1 = start + (int)size1;
                end=start1-1;
                size2=size-(int)size1;
                p1 = Draw_Area(g, type, genesis_point, start,(int)size1,data);
                g.DrawString(end.ToString(), font_adress, brush_adress, adress_x, memory_height - digit_height / 2);
                genesis_point.X-=memory_width+num_adress_digits*digit_width+10;
                genesis_point.Y = 10;
                genesis_point= Draw_Area(g, type, genesis_point, start1, size2, data);
            }
            else
            {
                switch (type)
                {
                    case 'h':
                        g.DrawRectangle(Pen_hole, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;
                    case 'o':
                        g.DrawRectangle(pen_old,genesis_point.X , genesis_point.Y, width, height);
                        g.DrawString(start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;
                    case 'n':
                        g.DrawRectangle(pen_new, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;

                }
                
                genesis_point.Y += (int)height;
            }
            return genesis_point;
        }
        public void sort()
        { 
         

            for(int i=0;i<Form1.holes_num;i++)
            {
                all.Add(Form1.holes_start[i],new KeyValuePair<char,int>('h',i));
            }
             for(int i=0;i<old_start.Count;i++)
            {
                all.Add(old_start[i], new KeyValuePair<char, int>('o', i));
            }
            for(int i=0;i<all_seg.Count;i++)
            {
                all.Add(all_seg[i].start,new KeyValuePair<char,int>('n',i));
            }

        }
        public void Redraw(Graphics g)
        {
            g.Clear(Color.White);
            g.PageUnit = GraphicsUnit.Millimeter;
            genesis_point = new Point(310, 10);
            string data="";
            int size = 0;
            ////////////////Holes/////////////////////////////////

            sort();
            foreach (KeyValuePair<int, KeyValuePair<char, int>> p in all)
            {
                switch(p.Value.Key)
                {
                    case'h':
                        data="Hole_"+p.Value.Value.ToString();
                        size=Form1.holes_size[p.Value.Value];
                        break;
                    case'o':
                        data="Old_"+p.Value.Value.ToString();
                        size=old_size[p.Value.Value];
                        break;
                    case'n':
                        data = all_seg[p.Value.Value].process_id.ToString() +":"+ all_seg[p.Value.Value].name;
                        size=all_seg[p.Value.Value].size;
                        break;
                }
                genesis_point = Draw_Area(g, p.Value.Key, genesis_point, p.Key, size, data);
            }
            g.DrawString((Form1.memory_max_size - 1).ToString(), font_adress, brush_adress, genesis_point.X - num_adress_digits * digit_width, genesis_point.Y);
            
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            seg_table.Controls.Clear();
            int num_seg = int.Parse(seg_txt.Text);

                Label l1 = new Label();
                l1.Text = "Seg_num";
                //l1.AutoSize = false;
                //l1.Margin = new Padding(6, 6, 6, 6);
                seg_table.Controls.Add(l1, 0, 0);

                //flowLayoutPanel1.Controls.Add(l1);
                for (int i = 1; i <= num_seg; i++)
                {
                    l1 = new Label();
                    l1.Name = i.ToString(); ;
                    l1.Text = "Seg" + i.ToString();
                    l1.AutoSize = true;
                    l1.Margin = new Padding(6, 6, 6, 6);
                    seg_table.RowCount++;
                    seg_table.Controls.Add(l1, 0, i);


                }
            Label l2 = new Label();
            l2.Text = "Seg_Name";
            seg_table.ColumnCount++;
            seg_table.Controls.Add(l2, 1, 0);
            for (int i = 1; i <= num_seg; i++)
            {
                TextBox t2 = new TextBox();

                t2.AutoSize = true;

                //Arrival_Storing_Handlers(); 
                //this.t2.Leave += new System.EventHandler(this.t2_Leave);


                seg_table.Controls.Add(t2, 1, i);
                seg_table.Controls.SetChildIndex(t2, i);

            }
            Label l3 = new Label();
            l3.Text = "Seg_Size";
            seg_table.ColumnCount++;
            seg_table.Controls.Add(l3, 2, 0);
            for (int i = 1; i <= num_seg; i++)
            {
                TextBox t3= new TextBox();

                t3.AutoSize = true;

                //Arrival_Storing_Handlers(); 
                //this.t2.Leave += new System.EventHandler(this.t2_Leave);


                seg_table.Controls.Add(t3, 2, i);
                seg_table.Controls.SetChildIndex(t3, num_seg + i);

            }
            seg_box.Visible = true;
            //Redraw(g_global);
            
        }

        private void Gant_chart_Load(object sender, EventArgs e)
        {
            seg_box.Visible = false;
            seg_table.ColumnStyles.Clear();
            seg_table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 30F));
            seg_table.RowStyles.Clear();
            seg_table.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30f));
        
            
        }






    }
}
