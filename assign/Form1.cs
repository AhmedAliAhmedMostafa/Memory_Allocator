using System;
using System.Collections;
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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AllocationMethod.Visible = false;
            comboBox1.Visible = false; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize, 30F));
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30f));
            tableLayoutPanel1.Visible = false;
            add_holes.Visible = false;
            button5.Visible = false;
            button3.Visible = false;
            AddProcessPanel.Visible = false;
            //panel2.Visible = false;
            //panel3.Visible = false;

        }


        //global variables 
        Label l1;

        /// <back_processing>

        public static List<int> holes_start = new List<int>(); //done 
        public static List<int> holes_size = new List<int>();  // done
        public static List<int> segment_number = new List<int>(); //done 
        public static List<string> segment_name = new List<string>(); //done 
        public static List<int> segment_size = new List<int>();  //done 
        public static int holes_num; //done 
        public static int memory_max_size; //done 


       public static  SortedDictionary<int, int> sortedHoles =
           new SortedDictionary<int, int>();


        // public static List<List<int>> segment_name = new List<List<int>>();

        //public static List<List<int>> segment_size = new List<List<int>>();



        public static int processes_num = 0;
       // public static List<List<int>> x = new List<List<int>>();
        public static List<int> x = new List<int>();

        /// </back_processing>


        public static List<int> bursts_entries = new List<int>();
        public static List<int> arrival_entries = new List<int>();
        public static List<int> arrival_copy = new List<int>();
        public static List<int> pirority_entries = new List<int>();
        public static List<int> fcfs = new List<int>();
        public static List<int> sjfnpBURST = new List<int>();
        public static List<int> sjfnpINDEX = new List<int>();
        public static List<int> bursts_copy = new List<int>();
        public static int U;
        public static string choice;
        public static string sub_choice;
        public static bool open = false;
        //int u = Int32.Parse(textBox1.Text);
        // for SJF non peemptive algo ::
        public static List<int> available = new List<int>();
        public static List<int> sorted_arrival_entries = new List<int>();
        public static List<int> availableIndex = new List<int>();
        public static int Q;


        //int u = Int32.Parse(textBox1.Text);
        //add instance of gant chart




        //generates processes label column 
        private void holesTemplateGenerator(int u)
        {

            l1 = new Label();
            l1.Text = "Holes";
            //l1.AutoSize = false;
            //l1.Margin = new Padding(6, 6, 6, 6);
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.Controls.Add(l1, 0, 0);

            //flowLayoutPanel1.Controls.Add(l1);
            for (int i = 1; i <= u; i++)
            {
                l1 = new Label();
                l1.Name = i.ToString(); ;
                l1.Text = "H" + i.ToString();
                l1.AutoSize = true;
                l1.Margin = new Padding(6, 6, 6, 6);
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.Controls.Add(l1, 0, i);





            }

        }
        //**************/// 
        private void processTemplateGenerator(int u)
        {
            
            l1 = new Label();
            l1.Name = processes_num.ToString(); ;
            l1.Text = "P" + processes_num.ToString();
            //l1.AutoSize = false;
            //l1.Margin = new Padding(6, 6, 6, 6);
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.Controls.Add(l1, 0, 0);

            //flowLayoutPanel1.Controls.Add(l1);
            for (int i = 1; i <= u; i++)
            {
                l1 = new Label();
                l1.Name = i.ToString(); ;
                l1.Text = "Seg" + i.ToString();
                l1.AutoSize = true;
                l1.Margin = new Padding(6, 6, 6, 6);
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.Controls.Add(l1, 0, i);





            }
        }

        //generates arrival_time text input column 
        TextBox t2;
        private void textboxgenerateHsize(int u)
        {


            Label l1 = new Label();
            l1.Text = "Size";
            tableLayoutPanel1.ColumnCount++;
            tableLayoutPanel1.Controls.Add(l1, 2, 0);
            for (int i = 1; i <= u; i++)
            {
                t2 = new TextBox();

                t2.AutoSize = true;

                //Arrival_Storing_Handlers(); 
                //this.t2.Leave += new System.EventHandler(this.t2_Leave);


                tableLayoutPanel1.Controls.Add(t2, 2, i);
                tableLayoutPanel1.Controls.SetChildIndex(t2, u + i);

            }

        }

        private void textboxgenerateProcesssize(int u)
        {

            Label l1 = new Label();
            l1.Text = "Size";
            tableLayoutPanel1.ColumnCount++;
            tableLayoutPanel1.Controls.Add(l1, 2, 0);
            for (int i = 1; i <= u; i++)
            {
                t2 = new TextBox();

                t2.AutoSize = true;

                //Arrival_Storing_Handlers(); 
                //this.t2.Leave += new System.EventHandler(this.t2_Leave);


                tableLayoutPanel1.Controls.Add(t2, 2, i);
                tableLayoutPanel1.Controls.SetChildIndex(t2, u + i);

            }

        }

        private void textboxgenerateProcesname(int u)
        {

            Label l1 = new Label();
            l1.Text = "Name";
            tableLayoutPanel1.ColumnCount++;
            tableLayoutPanel1.Controls.Add(l1, 1, 0);


            for (int i = 1; i <= u; i++)
            {
                t1 = new TextBox();
                t1.AutoSize = true;
                tableLayoutPanel1.Controls.Add(t1, 1, i);
                tableLayoutPanel1.Controls.SetChildIndex(t1, i);

                //Burst_Storing_Handlers(); 
                //**** this.t1.Leave += new System.EventHandler(this.t1_Leave);


            }

        }
        
        //**************/// 
        //generates burst_time text input column 
        TextBox t1;


        //private void Burst_Storing_Handlers() {

        //    this.t1.Leave += new System.EventHandler(this.t1_Leave);


        //}
        //private void Arrival_Storing_Handlers()
        //{

        //    this.t2.Leave += new System.EventHandler(this.t2_Leave);

        //}



        private void textboxgenerateHstart(int u)
        {
            Label l1 = new Label();
            l1.Text = "Start";
            tableLayoutPanel1.ColumnCount++;
            tableLayoutPanel1.Controls.Add(l1, 1, 0);


            for (int i = 1; i <= u; i++)
            {
                t1 = new TextBox();
                t1.AutoSize = true;
                tableLayoutPanel1.Controls.Add(t1, 1, i);
                tableLayoutPanel1.Controls.SetChildIndex(t1, i);

                //Burst_Storing_Handlers(); 
                //**** this.t1.Leave += new System.EventHandler(this.t1_Leave);


            }

        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }





        // panel1 columns activator according to #processes entered by user 
        private void button1_Click(object sender, EventArgs e)
        {



            //ErrorProvider errorProvider1 = new ErrorProvider();
            //ErrorProvider errorProvider2 = new ErrorProvider();
            //if (flagForConfirmProceedButton>1) errorProvider1.SetError(butto, "You left something empty");
            if (textBox1.Text == "") errorProvider1.SetError(textBox1, "You didn't insert #Holes");
            if (textBox3.Text == "") errorProvider1.SetError(textBox3, "You didn't insert MaxSize");



            else
            {
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(textBox3, "");

                //errorProvider1.Clear();

                //errorProvider2.Clear();

                try
                {
                    int u = Int32.Parse(textBox1.Text); //#holes 
                    int x = Int32.Parse(textBox3.Text); //MaxSize 

                    holes_num = u;
                    memory_max_size = x; 




                    tableLayoutPanel1.ColumnCount = 1;
                    tableLayoutPanel1.RowCount = 1;
                    tableLayoutPanel1.Controls.Clear();
                    holesTemplateGenerator(u);
                    textboxgenerateHstart(u);
                    textboxgenerateHsize(u);

                }
                catch (Exception y)
                {
                    MessageBox.Show("only integer values are allowed");
                    return;

                }

                AddProcessPanel.Visible = false;

                tableLayoutPanel1.Visible = true;
                //tableLayoutPanel1.Visible = false;
                add_holes.Visible = true;
                button5.Visible = true;


                
            }
        }






        // Generate
        //the action taken after catching bursts&arrival times 


        private void button5_Click(object sender, EventArgs e)
        {


            

            holes_start.Clear();
            holes_size.Clear();






            int u = Int32.Parse(textBox1.Text);

            

            //  sub_choice = cb.Text;
            for (int i = 1; i <= u; i++)
            {

                try
                {
                    holes_start.Add(int.Parse(tableLayoutPanel1.Controls[i].Text));
                } 

                catch(Exception t)
                {
                    MessageBox.Show("you didn't insert all the required holes data");
                    return;

                }
            }
            for (int i = 1; i <= u; i++)
            {
                try
                {
                    holes_size.Add(int.Parse(tableLayoutPanel1.Controls[u + i].Text));

                }
                catch (Exception t)
                {
                    MessageBox.Show("you didn't insert all the required holes data");
                    return;

                }
            }



            //testing 

            //for (int i = 0; i < holes_start.Count(); i++)
            //{
            //    MessageBox.Show(holes_start[i].ToString()+" "+ holes_size[i].ToString());
            //}



            StartPanel.Visible = false;
            add_holes.Visible = false;
            AddProcessPanel.Visible = true;
            tableLayoutPanel1.ColumnCount = 0;
            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.Visible = false;
            ////gant.Invalidate();
            //Gant_chart gant = new Gant_chart();

            //gant.Show();


            button5.Visible = false; 







        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        int flagForConfirmProceedButton = 0;
        //stores bursts_entries into list : bursts_entries
        private void t1_Leave(object sender, EventArgs e)
        {

            ErrorProvider errorprovider1 = new ErrorProvider();
            TextBox l1 = sender as TextBox;
            //MessageBox.Show(l1.Text); 

            if (l1.Text == "")
            {


                errorprovider1.SetError(l1, "You didn't insert a value");
                flagForConfirmProceedButton++;

            }
            else
            {

                errorprovider1.SetError(l1, "");
                bursts_entries.Add(Int32.Parse(l1.Text));


            }

        }

        //stores arrival_entries into list : arrival_entries
        private void t2_Leave(object sender, EventArgs e)
        {
            ErrorProvider errorprovider1 = new ErrorProvider();
            TextBox l1 = sender as TextBox;
            //MessageBox.Show(l1.Text); 


            if (l1.Text == "") errorprovider1.SetError(l1, "You didn't insert a value");
            else
            {

                errorprovider1.SetError(l1, "");
                arrival_entries.Add(Int32.Parse(l1.Text));
                arrival_copy.Add(Int32.Parse(l1.Text));



            }

        }
        //stores arrival_entries into list : pirority_entries
        private void t3_Leave(object sender, EventArgs e)
        {
            ErrorProvider errorprovider1 = new ErrorProvider();
            TextBox l1 = sender as TextBox;
            //MessageBox.Show(l1.Text); 


            if (l1.Text == "") errorprovider1.SetError(l1, "You didn't insert a value");
            else
            {

                errorprovider1.SetError(l1, "");
                pirority_entries.Add(Int32.Parse(l1.Text));



            }

        }





        ComboBox cb = new ComboBox(); //used inside this method and another one, hence it's placed in here
       

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void add_process_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel1.RowCount++;
            int u = int.Parse(textBox1.Text);
            U++;
            Label l1 = new Label();

            l1.Text = "H"+ U;
            tableLayoutPanel1.Controls.Add(l1, 0, tableLayoutPanel1.RowCount - 1);
            TextBox t1 = new TextBox();
            tableLayoutPanel1.Controls.Add(t1, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.Controls.SetChildIndex(t1, U);
            TextBox t2 = new TextBox();
            tableLayoutPanel1.Controls.Add(t2, 2, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.Controls.SetChildIndex(t2, 2 * U);
            
            tableLayoutPanel1.Visible = true;



        }

        //private void textBox1_Validating(object sender, CancelEventArgs e)
        //{
        //    string data = textBox1.Text;
        //    int n;
        //    if (!int.TryParse(data, out n))
        //    {
        //        errorProvider1.SetError(textBox1, "only integer numbes are allowed");
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        errorProvider1.SetError(textBox1, "");
        //        e.Cancel = false;
        //    }
        //}
        //private void textBox3_Validating(object sender, CancelEventArgs e)
        //{
        //    string data = textBox3.Text;
        //    int n;
        //    if (!int.TryParse(data, out n))
        //    {
        //        errorProvider1.SetError(textBox3, "only integer numbes are allowed");
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        errorProvider1.SetError(textBox3, "");
        //        e.Cancel = false;
        //    }
        //}




        private void button2_Click(object sender, EventArgs e)//add process
        {
            if (textBox2.Text == "") errorProvider1.SetError(textBox2, "You didn't insert the segments num");

            else
            {
                errorProvider1.SetError(textBox2, "");

                try
                {
                    int u = Int32.Parse(textBox2.Text);
                    //ahe
                    U = Int32.Parse(textBox2.Text);

                    if (u > 4)
                    {

                        MessageBox.Show("there could be 4 segments per process at most ::code,stack,data,others");

                        
                        button3.Visible = false;
                        tableLayoutPanel1.Visible = false;
                        AllocationMethod.Visible = false;
                        comboBox1.Visible = false;
                        return;

                    }
                    if (u == 0)
                    {
                        MessageBox.Show("process has to have segments");
                        return;
                    }


                    segment_number.Add(int.Parse(textBox2.Text));



                    //testing
                    //for (int i = 0; i < segment_number.Count(); i++)
                    //{
                    //    MessageBox.Show(segment_number[i].ToString());
                    //}

                    tableLayoutPanel1.ColumnCount = 1;
                    tableLayoutPanel1.RowCount = 1;
                    tableLayoutPanel1.Controls.Clear();
                    processTemplateGenerator(u);
                    textboxgenerateProcesname(u);
                    textboxgenerateProcesssize(u);
                }

                catch (Exception y)
                {
                    MessageBox.Show("only integer values are allowed");
                    return;

                }

                button3.Visible = true;
                tableLayoutPanel1.Visible = true;
                AllocationMethod.Visible = true;
                comboBox1.Visible = true; 

 }

            processes_num++;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            x.Clear();
            //don't  segment_name.Clear();
            for (int i = 1; i <= U; i++)
            {


                // x.Add(int.Parse(tableLayoutPanel1.Controls[i].Text));
                segment_name.Add(tableLayoutPanel1.Controls[i].Text);
            }

            //segment_name.Add(x);
            x.Clear();
            for (int i = 1; i <= U; i++)
            {

                //x.Add(int.Parse(tableLayoutPanel1.Controls[U + i].Text));
                segment_size.Add(int.Parse(tableLayoutPanel1.Controls[U + i].Text));
                //don't segment_size.Add(int.Parse(tableLayoutPanel1.Controls[U + i].Text));
            }


            //testing 
            //for (int i = 0; i < segment_size.Count(); i++)
            //{
            //    MessageBox.Show(segment_size[i].ToString()+" "+segment_name[i].ToString());
            //}

            for (int i = 0; i < holes_num; i++) { 
                sortedHoles.Add(holes_start[i] , holes_size[i]);

        }


            //testing
            //foreach (KeyValuePair<int, int> p in sortedHoles)
            //{
            //    MessageBox.Show(p.Key.ToString() + " " + p.Value.ToString());

            //}




            tableLayoutPanel1.ColumnCount = 0;
            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.Visible = false;
            button3.Visible = false;
            ////gant.Invalidate();
            Gant_chart gant = new Gant_chart();

            gant.Show();








        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
      

 


