using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Zen.Barcode;
namespace HomeWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        int EditOrSave;//O for Save Data And 1 For Update Data
        int id;//Select Id
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-J8D4LKT;Initial Catalog=Students;User ID=mohsin;Password=mohsin");
        private void Form1_Load(object sender, EventArgs e)
        {
            Showdata();
            ShowNoOfRecordInLbl();
            //DataGridViewComboBoxColumn EditOrDelete = new DataGridViewComboBoxColumn();
            //EditOrDelete.HeaderText = "Edit Or Delete";
            //EditOrDelete.Name = "Cmb";
            //EditOrDelete.MaxDropDownItems = 2;
            //EditOrDelete.Items.Add("Edit");
            //EditOrDelete.Items.Add("Delete");
            //dataGridView1.Columns.Add(EditOrDelete);
        }
        public void Showdata()
        {
            DataTable dt = GetData(UserIdDv, FirstNameDv, LastnameDv, FathernameDv, EmailDv, PhoneDv, AgeDv, GenderDv);
            dataGridView1.DataSource = dt;
        }
        private DataTable GetData(DataGridViewColumn UserId,DataGridViewColumn firstname,DataGridViewColumn lastname,DataGridViewColumn fathername,DataGridViewColumn email,DataGridViewColumn phone,DataGridViewColumn age,DataGridViewColumn gender)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("GetData", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);
                Con.Open();
                UserId.DataPropertyName = dt.Columns[0].ToString();
                firstname.DataPropertyName = dt.Columns["firstname"].ToString();
                lastname.DataPropertyName = dt.Columns["lastname"].ToString();
                fathername.DataPropertyName = dt.Columns["fathername"].ToString();
                email.DataPropertyName = dt.Columns["email"].ToString();
                phone.DataPropertyName = dt.Columns["phone"].ToString();
                age.DataPropertyName = dt.Columns["age"].ToString();
                gender.DataPropertyName = dt.Columns["gender"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            finally
            {
                Con.Close();
            }
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VaidateControl();
            if(fnLbl.Visible || LnLbl.Visible || FnaLbl.Visible|| eLbl.Visible || pLbl.Visible || aLbl.Visible || GLbl.Visible)
            {
                MessageBox.Show("All Fields Are Mandetory ...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (EditOrSave == 0)
                {
                    EditOrSave = 0;
                    try
                    {
                        SqlCommand cmd = new SqlCommand("InsertData", Con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fname", Firstnametxt.Text);
                        cmd.Parameters.AddWithValue("@lname", lastnametxt.Text);
                        cmd.Parameters.AddWithValue("@faName", fathernametxt.Text);
                        cmd.Parameters.AddWithValue("@email", emailtxt.Text);
                        cmd.Parameters.AddWithValue("@phone", phonetxt.Text);
                        cmd.Parameters.AddWithValue("@age", agetxt.Text);
                        cmd.Parameters.AddWithValue("@gender", genderCb.SelectedItem);
                        Con.Open();
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        MessageBox.Show("Data Inserted Successfully...!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Showdata();
                        ClearControl();
                        ShowNoOfRecordInLbl();
                        
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + ex.StackTrace);
                    }
                    finally
                    {
                        Con.Close();
                    }
                }
                else if (EditOrSave == 1)
                {
                    EditOrSave = 1;
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UpdateData", Con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fname", Firstnametxt.Text);
                        cmd.Parameters.AddWithValue("@lname", lastnametxt.Text);
                        cmd.Parameters.AddWithValue("@faName", fathernametxt.Text);
                        cmd.Parameters.AddWithValue("@email", emailtxt.Text);
                        cmd.Parameters.AddWithValue("@phone", phonetxt.Text);
                        cmd.Parameters.AddWithValue("@age", agetxt.Text);
                        cmd.Parameters.AddWithValue("@gender", genderCb.SelectedItem);
                        cmd.Parameters.AddWithValue("@id", id);
                        Con.Open();
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        MessageBox.Show("Data Updated Successfully...!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Showdata();
                        ClearControl();
                        button1.Text = "Insert";
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + ex.StackTrace);
                    }
                    finally
                    {
                        Con.Close();
                    }
                }
            }
        }
        //This Method is for Clear Control Method
        private void ClearControl()
        {
            Firstnametxt.Text = "";
            lastnametxt.Text = "";
            fathernametxt.Text = "";
            emailtxt.Text = "";
            phonetxt.Text = "";
            agetxt.Text = "";
            genderCb.SelectedIndex = -1;
        }
        public void VaidateControl()
        {
            if (Firstnametxt.Text == "") { fnLbl.Visible = true; } else { fnLbl.Visible = false; }
            if (lastnametxt.Text == "") { LnLbl.Visible = true; } else { LnLbl.Visible = false; }
            if (fathernametxt.Text == "") { FnaLbl.Visible = true; } else { FnaLbl.Visible = false; }
            if (emailtxt.Text == "") { eLbl.Visible = true; } else { eLbl.Visible = false; }
            if (phonetxt.Text == "") { pLbl.Visible = true; } else { pLbl.Visible = false; }
            if (agetxt.Text == "") { aLbl.Visible = true; } else { aLbl.Visible = false; }
            if (genderCb.SelectedIndex == -1) { GLbl.Visible = true; } else { GLbl.Visible = false; }
        }
        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //Display In Control Selected row Form Gridview

        //    //Disable Text Control
        //    //if (dataGridView1.Columns.Contains(Editbtn.Name = "Edit"))
        //    //{
        //    //    MessageBox.Show("Edit button Click");
        //    //}
        //    DisableControl();
        //}
        private void DisableOrEnableControl(bool OnOff)
        {
            Firstnametxt.Enabled = OnOff;
            lastnametxt.Enabled = OnOff;
            fathernametxt.Enabled = OnOff;
            emailtxt.Enabled = OnOff;
            phonetxt.Enabled = OnOff;
            agetxt.Enabled = OnOff;
            genderCb.Enabled = OnOff;
        }
        public void ShowNoOfRecordInLbl()
        {
            SqlCommand cmd = Con.CreateCommand();
            cmd.CommandText= "Select Count(Sid)from StudentInfo";
            cmd.CommandType = CommandType.Text;
            Con.Open();
            object temp = cmd.ExecuteScalar();
            NoOfRecordLbl.Text = temp.ToString();
            Con.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int ColumnIndex = dataGridView1.CurrentCell.ColumnIndex;
            //label1.Text= ColumnIndex.ToString();
            if (e.ColumnIndex == 0)
            {
                EditOrSave = 1;
                int rowindex = e.RowIndex;
                id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells["UserIdDv"].Value.ToString());
                Firstnametxt.Text = dataGridView1.Rows[rowindex].Cells["FirstNameDv"].Value.ToString();
                lastnametxt.Text = dataGridView1.Rows[rowindex].Cells["LastnameDv"].Value.ToString();
                fathernametxt.Text = dataGridView1.Rows[rowindex].Cells["FathernameDv"].Value.ToString();
                emailtxt.Text = dataGridView1.Rows[rowindex].Cells["EmailDv"].Value.ToString();
                phonetxt.Text = dataGridView1.Rows[rowindex].Cells["PhoneDv"].Value.ToString();
                agetxt.Text = dataGridView1.Rows[rowindex].Cells["AgeDv"].Value.ToString();
                genderCb.SelectedItem = dataGridView1.Rows[rowindex].Cells["GenderDv"].Value.ToString();
                button1.Text = "Update";
            }
            else if (e.ColumnIndex == 1)
            {
                int rowindex = e.RowIndex;
                id = Convert.ToInt32(dataGridView1.Rows[rowindex].Cells["UserIdDv"].Value.ToString());
                DialogResult result = MessageBox.Show("Are You Sure...! You Want To Delete Record...", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("DeleteData", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    Showdata();
                    ClearControl();
                    button1.Text = "Insert";
                    ShowNoOfRecordInLbl();
                }
            }
        }

        private void ClearChekBox_CheckedChanged(object sender, EventArgs e)
        {
            EditOrSave = 0;
            ClearControl();
            ClearChekBox.Checked = false;
            button1.Text = "Insert";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // + "\nPhone : " + phonetxt.Text + "\nAge :" + agetxt.Text + "\nGender :" + genderCb.Text
            BarcodeDraw drawbar = BarcodeDrawFactory.CodeQr;
            var qrText = "First Name :" +Firstnametxt.Text+"\nLast Name :" + lastnametxt.Text+"\nFather Name :" + fathernametxt.Text;
            pictureBox1.Image = drawbar.Draw(qrText, 50);
            pictureBox1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportForm re = new ReportForm();
            re.Show();
        }
    }
}
