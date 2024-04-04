﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSVdatabase
{
    public partial class frmListStudent : Form
    {
        public frmListStudent()
        {
            InitializeComponent();
        }

        private void frmListStudent_Load(object sender, EventArgs e)
        {
            LoadListStudent();
        }

        private string Search = "";
        
            
        


        private void LoadListStudent()
        {
            List<CustomParameter> lstpara = new List<CustomParameter>();
            lstpara.Add(new CustomParameter()
            {
                key = "@keyword",
                value = Search
            });
            dgvstudent.DataSource = new Database().SelectData("SelectAllStudent", lstpara);

            dgvstudent.Columns["Student_ID"].HeaderText = "Student ID";
            dgvstudent.Columns["fullname"].HeaderText = "Full Name";
            dgvstudent.Columns["Date_of_birth"].HeaderText = "Date Of Birth";
            dgvstudent.Columns["Sex"].HeaderText = "Sex";
            dgvstudent.Columns["Home_town"].HeaderText = "Home Town";
            dgvstudent.Columns["Address"].HeaderText = "Address";
            dgvstudent.Columns["Phone_number"].HeaderText = "Phone Number";
            dgvstudent.Columns["Email"].HeaderText = "Email";

        }

        private void dgvstudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            new frmStudent(null).ShowDialog();
            LoadListStudent();
        }

        private void dgvstudent_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                var msv = dgvstudent.Rows[e.RowIndex].Cells["Student_ID"].Value.ToString();
                new frmStudent(msv).ShowDialog();
                LoadListStudent();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search = txtKeyword.Text;
            LoadListStudent();
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvListStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string studentId = dgvstudent.Rows[e.RowIndex].Cells["Student_ID"].Value.ToString();
                string fullName = dgvstudent.Rows[e.RowIndex].Cells["fullName"].Value.ToString();

                delStudent.Tag = new Tuple<string, string>(studentId, fullName);
            }
        }

        private void delStudent_Click(object sender, EventArgs e)
        {
            Tuple<string, string> tagData = delStudent.Tag as Tuple<string, string>;
            if (tagData != null)
            {
                string studentId = tagData.Item1 as string;
                string fullName = tagData.Item2 as string;
                DialogResult result = MessageBox.Show("Do you want delete student '" + fullName + "'?", "Delete", MessageBoxButtons.OKCancel);
                string sql = "deleteStudent";
                if (result == DialogResult.OK)
                {
                    List<CustomParameter> listPara = new List<CustomParameter>();

                    listPara.Add(new CustomParameter
                    {
                        key = "@studentId",
                        value = studentId
                    });

                    int resultDel = new Database().ExeCuteDelete(sql, listPara);
                    if (resultDel > 0)
                    {
                        MessageBox.Show("Delete successfully!");
                        LoadListStudent();
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    LoadListStudent();
                }

            }

        }
    }
}
