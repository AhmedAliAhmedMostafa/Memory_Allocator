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
        Brush brush_adress = new SolidBrush(Color.Red);
        Brush brush_data = new SolidBrush(Color.Blue);
        //float outer_width = 2;
        int memory_width = 40;
        float memory_height = 215;
        float step = 215 / (float)Form1.memory_max_size;
        int num_adress_digits = (Form1.memory_max_size.ToString()).Length;
        int digit_width = 3;
        int digit_height = 4;
        float starting_x;
        float starting_y;
        float w, h;
        Graphics g_global;
        Point genesis_point = new Point(260, 0);
        List<int> old_start = new List<int>();
        List<int> old_size = new List<int>();
        SortedDictionary<int, int> new_process = new SortedDictionary<int, int>();


        // poss hena poss hena 

        List<int> seg_count_per_process = new List<int>();

        //

        public struct segment
        {
            public int process_id, start, size;
            public string name;

            public segment(int pid, int start, int size, string name)
            {
                this.process_id = pid;
                this.start = start;
                this.size = size;
                this.name = name;

            }
        }


        public struct process
        {
            public List<segment> Process_seg;
            public int num_seg;
            public process(int num = 0)
            {
                Process_seg = new List<segment>();
                num_seg = num;
            }
        }


        List<process> all_new = new List<process>();
        SortedDictionary<int, KeyValuePair<char, int>> all = new SortedDictionary<int, KeyValuePair<char, int>>();
        List<segment> all_seg = new List<segment>();



        // hereeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee


        int allocated_process_id = 0;
        public void clear_sort()
        {
            Form1.sortedHoles_in_address.Clear();
            for (int j = 0; j < Form1.holes_num; j++)
            {
                Form1.sortedHoles_in_address.Add(Form1.holes_start[j], new Form1.hole(Form1.holes_start[j], Form1.holes_size[j], j));

            }
        }
        public void Allocate_first_fit()
        {

            // as first execution of Redraw() cycles through sortedHoles_in_address to allocate ,we couldn't modify sortedHoles_in_address here 
            // Form1.sortedHoles_in_address_exhausted = Form1.sortedHoles_in_address; 

        int all_Process_seg_allocated_flag = 0;
        process youngest = new process(Form1.segment_size.Count());
        all_new.Add(youngest);

        clear_sort();


        
            for (int i = 0; i < Form1.segment_size.Count(); i++)
            {

                bool is_allocated = false;

                foreach (KeyValuePair<int, Form1.hole> p in Form1.sortedHoles_in_address)
                {

                    if (Form1.segment_size[i] <= p.Value.size)
                    {

                        segment s = new segment(allocated_process_id, p.Key, Form1.segment_size[i], Form1.segment_name[i]);


                        all_seg.Add(s);
                        youngest.Process_seg.Add(s);

                        Form1.holes_size[p.Value.id] -= Form1.segment_size[i];


                        if (Form1.holes_size[p.Value.id] == 0)
                        {
                            Form1.holes_size.RemoveAt(p.Value.id);
                            Form1.holes_start.RemoveAt(p.Value.id);
                            Form1.holes_num--;

                        }
                        else
                        {

                            Form1.holes_start[p.Value.id] += s.size;
                        }



                        //reconstructing the sorted holes again as their start&size changed

                        clear_sort();
                        is_allocated = true;
                        all_Process_seg_allocated_flag++;
                        break;

                    }



                }

                if (!is_allocated)
                {
                    MessageBox.Show("The process doesn't fit!, you should try to deallocate first");
                    all_new.RemoveAt((all_new.Count)-1);
                    return;
                }



            }
            if (all_Process_seg_allocated_flag == Form1.segment_size.Count())
            {
                allocated_process_id++;
            }


        }


        public void Allocate_best_fit()
        {




            int all_Process_seg_allocated_flag = 0;
            for (int i = 0; i < Form1.segment_size.Count(); i++)
            {

                bool is_allocated = false;





                for (int j = 0; j < Form1.holes_start.Count(); j++)
                {

                    if (Form1.segment_size[i] <= Form1.sortedHoles_inSize[j].Size)
                    {

                        segment s = new segment(allocated_process_id, Form1.sortedHoles_inSize[j].Hole.start, Form1.segment_size[i], Form1.segment_name[i]);


                        all_seg.Add(s);


                        Form1.holes_start[Form1.sortedHoles_inSize[j].Hole.id] += Form1.segment_size[i];

                        // if (Form1.holes_start[Form1.sortedHoles_inSize[j].Hole.id] >= Form1.holes_size[Form1.sortedHoles_inSize[j].Hole.id])
                        //{
                        //   Form1.holes_start.RemoveAt(Form1.sortedHoles_inSize[j].Hole.id);

                        //}


                        Form1.holes_size[Form1.sortedHoles_inSize[j].Hole.id] -= Form1.segment_size[i];


                        if (Form1.holes_size[Form1.sortedHoles_inSize[j].Hole.id] == 0)
                        {

                            Form1.holes_start.RemoveAt(Form1.sortedHoles_inSize[j].Hole.id);
                            Form1.holes_size.RemoveAt(Form1.sortedHoles_inSize[j].Hole.id);

                        }



                        //reconstructing the sorted holes again as their start&size changed

                        Form1.sortedHoles_inSize.Clear();

                        var sorted = Form1.holes_size
                                        .Select((x, l) => new KeyValuePair<int, int>(x, l))
                                        .OrderBy(x => x.Key)
                                        .ToList();

                        List<int> Sorted_holes_size = sorted.Select(x => x.Key).ToList();
                        List<int> orignal_id = sorted.Select(x => x.Value).ToList();

                        for (int k = 0; k < Form1.holes_start.Count(); k++)
                        {


                            Form1.sortedHoles_inSize.Add(new Form1.ForBestFit(Sorted_holes_size[k], new Form1.hole(Form1.holes_start[orignal_id[k]], Sorted_holes_size[k], orignal_id[k])));

                        }


                        is_allocated = true;
                        all_Process_seg_allocated_flag++;
                        break;

                    }



                }

                if (!is_allocated)
                {
                    MessageBox.Show("The process doesn't fit!, you should try to deallocate first");
                    return;
                }



            }
            if (all_Process_seg_allocated_flag == Form1.segment_size.Count())
            {
                allocated_process_id++;
            }
        }





        public void Deallocate_old(int pid)
        {

            if (pid >= old_size.Count)
            {
                MessageBox.Show("There Is No Old Process In yMemor With this ID");
            }
            else
            {
                Form1.holes_start.Add(old_start[pid]);
                Form1.holes_size.Add(old_size[pid]);
                old_start.RemoveAt(pid);
                old_size.RemoveAt(pid);
                Form1.holes_num++;
                combact();
            }
        }
        public void Deallocate_new(int pid)
        {


            int de_allocated_start;
            int de_allocated_size;


            foreach (KeyValuePair<int, Form1.NewProcessPair> p in Form1.allocated_processes_with_id)
            {


                if (pid == p.Key)
                {


                    de_allocated_start = p.Value.P_start;
                    de_allocated_size = p.Value.p_size;

                    // fetch the original hole that contains it  

                    foreach (KeyValuePair<int, int> pi in Form1.allocted_holeId_processId_mapping)
                    {

                        if (pi.Value == pid)
                        {
                            Form1.holes_start[pi.Key] -= de_allocated_size;
                            Form1.holes_size[pi.Key] += de_allocated_size;

                        }



                    }


                    break;
                }


            }

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
      
        public Point Draw_Area(Graphics g, char type, Point genesis_point,int start,float size,string data)
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
            Point p1 = new Point();
            int head_start, foot_end;
            float height_foot,height_head;
            if (height < min_height)
            {
                height = min_height;
            }
            if (genesis_point.Y + height > memory_height)
            {
                height_foot = memory_height-genesis_point.Y;
                height_head = height - height_foot;
                if (height_head < min_height)
                    height_head = min_height;
                foot_end=(int)(height_foot/step)+start;
                head_start = foot_end;
                height=height_foot;
                if (height_foot < 15)
                {
                    g.DrawString(start.ToString(), font_adress, brush_adress, adress_x, genesis_point.Y );
                    genesis_point.X -= memory_width + num_adress_digits * digit_width + 10;
                    genesis_point.Y = 10;
                    return Draw_Area(g, type, genesis_point, start, size, data);
                }
                g.DrawString(foot_end.ToString(), font_adress, brush_adress, adress_x,genesis_point.Y+height );
                switch (type)
                {
                    case 'h':
                        g.DrawRectangle(Pen_hole, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;
                    case 'o':
                        g.DrawRectangle(pen_old, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;
                    case 'n':
                        g.DrawRectangle(pen_new, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;

                }
                height = height_head;
                genesis_point.X -= memory_width + num_adress_digits * digit_width + 10;
                
                genesis_point.Y = 10;
                adress_x =genesis_point.X - num_adress_digits * digit_width;
                adress_y = adress_y = genesis_point.Y - digit_height / 2;
                data_x=genesis_point.X+memory_width/2-(len/2*digit_width);
                data_y = genesis_point.Y + height / 2;
                
                switch (type)
                {
                    case 'h':
                        g.DrawRectangle(Pen_hole, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(head_start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;
                    case 'o':
                        g.DrawRectangle(pen_old, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(head_start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;
                    case 'n':
                        g.DrawRectangle(pen_new, genesis_point.X, genesis_point.Y, width, height);
                        g.DrawString(head_start.ToString(), font_adress, brush_adress, adress_x, adress_y);
                        g.DrawString(data, font_data, brush_data, data_x, data_y);
                        break;

                }
                genesis_point.Y +=(int) height;
                return genesis_point;
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
                return genesis_point;
            }
            
        }


        public void sort()
        {


            all.Clear();
             foreach(KeyValuePair<int, Form1.hole> p in Form1.sortedHoles_in_address) {


                all.Add(p.Key, new KeyValuePair<char, int>('h', p.Value.id));
            }

             for(int i=0;i<old_start.Count();i++)
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

            int test=1;

            g.PageUnit = GraphicsUnit.Millimeter;
            genesis_point = new Point(310, 10);
            string data="";
            int size = 0;
            ////////////////Holes/////////////////////////////////

           KeyValuePair <int, KeyValuePair<char, int> >last=new KeyValuePair<int,KeyValuePair<char,int>>();
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
                last = p;
            }
            
            g.DrawString((last.Key+size).ToString(), font_adress, brush_adress, genesis_point.X - num_adress_digits * digit_width, genesis_point.Y);
            
        }
        public void sort_holes()
        { 
            int holder1,holder2;
            
            for(int i=0;i<Form1.holes_num;i++)
            {
                for (int j = i+1; j < Form1.holes_num; j++)
                {
                    if (Form1.holes_start[j] < Form1.holes_start[i])
                    {
                        holder1 = Form1.holes_start[j];
                        holder2=Form1.holes_size[j];
                        Form1.holes_start[j] = Form1.holes_start[i];
                        Form1.holes_size[j] = Form1.holes_size[i];
                        Form1.holes_start[i] = holder1;
                        Form1.holes_size[i] = holder2;
                       
                    }
                }
            }
        }


        public void combact()
        {
            sort_holes();

            for (int i = 0; i < Form1.holes_num; i++)
            {
                for (int j = 0; j < Form1.holes_num; j++)
                {
                    if (Form1.holes_start[j] == Form1.holes_start[i] + Form1.holes_size[i] )
                    {
                        Form1.holes_size[i] += Form1.holes_size[j];
                        Form1.holes_start.RemoveAt(j);
                        Form1.holes_size.RemoveAt(j);
                        Form1.holes_num--;
                        j--;
                    }
                    
                }
            }
            clear_sort();
        }
        public void get_old()
        {
           
            int itr=0;
            old_start.Clear();
            old_size.Clear();
            foreach(KeyValuePair<int,Form1.hole>p in Form1.sortedHoles_in_address)
            {
                if (itr < p.Key)
                {
                    old_start.Add(itr);
                    old_size.Add(p.Key - itr);
                    itr = p.Key + p.Value.size;

                }
                else
                {
                    itr = p.Key + p.Value.size;
                }
            }
            if (itr <= Form1.memory_max_size - 1)
            {
                old_start.Add(itr);
                old_size.Add((Form1.memory_max_size) - itr);
            }
        }


        
        private void Gant_chart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g_global = g;
            g.PageUnit = GraphicsUnit.Millimeter;
            starting_x =130;
            starting_y = 0;
            Redraw(g);
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void Deallocate_new(int pid)
        {

            int loopCondition = all_seg.Count();


            if (Form1.holes_num <= 0)
            {



                for (int i = 0, j = 0; j < loopCondition; i++, j++)
                {


                    if (pid == all_seg[i].process_id)
                    {
                        Form1.holes_size.Add(all_seg[i].size);
                        Form1.holes_start.Add(all_seg[i].start);
                        Form1.holes_num++;

                        all_seg.RemoveAt(i);
                        i--;
                    }
                }
            }

            else
            {


                for (int i = 0, j = 0; j < loopCondition; i++, j++)
                {



                    if (all_seg[i].HoleThatContainsIt <= Form1.holes_num - 1)
                    {


                        Form1.holes_size[all_seg[i].HoleThatContainsIt] += all_seg[i].size;


                        Form1.holes_start[all_seg[i].HoleThatContainsIt] -= all_seg[i].size;

                        all_seg.RemoveAt(i);
                        i--;

                    }
                    else
                    {


                        Form1.holes_size.Add(all_seg[i].size);
                        Form1.holes_start.Add(all_seg[i].start);
                        Form1.holes_num++;

                        all_seg.RemoveAt(i);
                        i--;


                    }
                }



            }


            combact();

            return;
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {



            seg_count_per_process.Clear(); 
            seg_table.Controls.Clear();

            seg_table.RowCount = 1;
            seg_table.ColumnCount = 1;
            // MessageBox.Show(seg_table.ColumnCount.ToString());
            
            int num_seg;  
            try
            {
                 num_seg = int.Parse(seg_txt.Text);
            }
            catch (Exception t)
            {
                MessageBox.Show("you didn't insert num_seg");
                return;

            }
            seg_count_per_process.Add(num_seg); 

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
                    l1.Text = "SegSHIT" + i.ToString();
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

        private void allo_btn_Click(object sender, EventArgs e)
        {


            Form1.segment_name.Clear();
            Form1.segment_size.Clear();


            try
            {
                int q = int.Parse(seg_txt.Text);



                Form1.x.Clear();

                //don't  segment_name.Clear();
                for (int i = 1; i <= q; i++)
                {


                    // x.Add(int.Parse(tableLayoutPanel1.Controls[i].Text));
                    Form1.segment_name.Add(seg_table.Controls[i].Text);
                }

                //segment_name.Add(x);
                Form1.x.Clear();
                for (int i = 1; i <= q; i++)
                {

                    //x.Add(int.Parse(tableLayoutPanel1.Controls[U + i].Text));
                    Form1.segment_size.Add(int.Parse(seg_table.Controls[q + i].Text));
                    //don't segment_size.Add(int.Parse(tableLayoutPanel1.Controls[U + i].Text));
                }


                //testing 


                //for (int i = 0; i < Form1.segment_size.Count(); i++)
                //{
                //    MessageBox.Show(Form1.segment_size[i].ToString() + " " + Form1.segment_name[i].ToString());
                //}

                // reDRAWING 
                //  get_old();

                if (comboBox1.Text == "")
                {

                    MessageBox.Show("you didn't insert the allocation method");
                return;


                }
                else if(comboBox1.Text == "First Fit")
                {

                    Allocate_first_fit();
                    combact();
                    Invalidate();
                    
                }
                else if (comboBox1.Text == "Best Fit")
                {

                    // FOR BEST FIT
                    Form1.sortedHoles_inSize.Clear();
                    var sorted = Form1.holes_size
                        .Select((x, i) => new KeyValuePair<int, int>(x, i))
                        .OrderBy(x => x.Key)
                        .ToList();

                    List<int> Sorted_holes_size = sorted.Select(x => x.Key).ToList();
                    List<int> orignal_id = sorted.Select(x => x.Value).ToList();

                    for (int j = 0; j < Form1.holes_start.Count(); j++)
                    {

                        Form1. sortedHoles_inSize.Add(new Form1.ForBestFit(Sorted_holes_size[j], new Form1.hole(Form1.holes_start[orignal_id[j]], Sorted_holes_size[j], orignal_id[j])));

                    }

                    //


                    Allocate_best_fit();
                    combact();
                    Invalidate();
                }



                Form1.gant.Show();

            }
            catch (Exception t)
            {
                MessageBox.Show("you didn't insert all the required segments data");
                return;

            }

            //testing
            //foreach (KeyValuePair<int, int> p in sortedHoles_in_address)
            //{
            //    MessageBox.Show(p.Key.ToString() + " " + p.Value.ToString());

            //}




            //tableLayoutPanel1.ColumnCount = 0;
            //tableLayoutPanel1.RowCount = 0;
            //tableLayoutPanel1.Controls.Clear();
            //tableLayoutPanel1.Visible = false;
            //button3.Visible = false;
            ////gant.Invalidate();













        }


        private void Gant_chart_Load(object sender, EventArgs e)
        {
            seg_box.Visible = false;
            seg_table.ColumnStyles.Clear();
            seg_table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 30F));
            seg_table.RowStyles.Clear();
            seg_table.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30f));
            get_old();
            combact();
        
            
        }

        private void Deallocate_btn_Click(object sender, EventArgs e)
        {
            if (Type_combo.Text == "Old Process")
            {
                Deallocate_old(int.Parse(Pid_txt.Text));
                Invalidate();
            }
            else if (Type_combo.Text == "New Process")
            {
                Deallocate_new(int.Parse(Pid_txt.Text));
                Invalidate();
            }
        }






    }
}
