﻿using MP_Garcia_GeneJoseph_BMIS.Models;
using MP_Garcia_GeneJoseph_BMIS.Presenters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_Garcia_GeneJoseph_BMIS.Views.ResidentView
{
    class AddFamilyView : Form, IResident
    {
        public AddFamilyView()
        {
            this.InitComponents();
        }

        private List<Resident> residents = new List<Resident>();
        public List<Resident> Residents { get { return residents; } set { residents = value; } }

        private Resident resident = new Resident();
        public Resident Resident { get { return resident; } set { resident = value; } }

        private int parentOneId;
        private int parentTwoId;

        public void PopulateFirstDataList()
        {
            this.dataListPrnt1.DataSource = this.residents;
            this.dataListPrnt1.Columns["ResidentId"].Visible = false;
            this.dataListPrnt1.Columns["Sex"].Visible = false;
            this.dataListPrnt1.Columns["Birthdate"].Visible = false;
            this.dataListPrnt1.Columns["Address"].Visible = false;
            this.dataListPrnt1.Columns["Status"].Visible = false;
        }

        public void PopulateSecondDataList(int toExclude)
        {

            this.dataListPrnt2.DataSource = this.residents.Where(m=>m.ResidentId != toExclude).ToList();
            this.dataListPrnt2.Columns["ResidentId"].Visible = false;
            this.dataListPrnt2.Columns["Sex"].Visible = false;
            this.dataListPrnt2.Columns["Birthdate"].Visible = false;
            this.dataListPrnt2.Columns["Address"].Visible = false;
            this.dataListPrnt2.Columns["Status"].Visible = false;
        }

        // listeners
        private void DataListOnSelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataListPrnt1.SelectedRows)
            {
                string strId = row.Cells[0].Value.ToString();
                int id = int.Parse(strId);
                this.PopulateSecondDataList(id);

            }
        }
        private void CreateOnClick(object sender, EventArgs e)
        {
            int familyMembers = 0;
            bool valid = true;

            if (this.dataListPrnt1.SelectedRows.Count == 1)
                foreach (DataGridViewRow row in this.dataListPrnt1.SelectedRows)
                {
                    string strId = row.Cells[0].Value.ToString();
                    parentOneId = int.Parse(strId);
                }
            else
                valid = false;

            if (valid)
                if (this.dataListPrnt2.SelectedRows.Count == 1)
                    foreach (DataGridViewRow row in this.dataListPrnt2.SelectedRows)
                    {
                        string strId = row.Cells[0].Value.ToString();
                        parentTwoId = int.Parse(strId);
                    }

            familyMembers = int.Parse(this.numFamilyMember.Value.ToString());

            if (valid)
            {
                new ResidentPresenter().PostSaveFamily(parentOneId, parentTwoId, familyMembers);
            }
            else
            {
                MessageBox.Show("First parent is required, and the second parent is optional for single-parents.", "Add Family", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void InitComponents()
        {
            // component initialization
            DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle21 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle22 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle23 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle24 = new DataGridViewCellStyle();
            this.dsnLlbl = new Label();
            this.dsnBox = new PictureBox();
            this.lblResidents = new Label();
            this.dataListPrnt1 = new DataGridView();
            this.dataListPrnt2 = new DataGridView();
            this.btnCreate = new Button();
            this.dsnLine = new PictureBox();
            this.lblFamilyMembers = new Label();
            this.numFamilyMember = new NumericUpDown();
            this.lblParent1 = new Label();
            this.lblParent2 = new Label();

            // Label : Resident
            this.lblResidents.AutoSize = true;
            this.lblResidents.Font = new Font("Trebuchet MS", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblResidents.ForeColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            this.lblResidents.Location = new Point(39, 80);
            this.lblResidents.Name = "lblResidents";
            this.lblResidents.Size = new Size(90, 24);
            this.lblResidents.Text = "Residents";

            // Label : First Parent
            this.lblParent1.AutoSize = true;
            this.lblParent1.Font = new Font("Trebuchet MS", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblParent1.ForeColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            this.lblParent1.Location = new Point(40, 136);
            this.lblParent1.Name = "lblParent1";
            this.lblParent1.Size = new Size(83, 20);
            this.lblParent1.Text = "Parent One";

            // DataGridView: First set of residents
            this.dataListPrnt1.AllowUserToAddRows = false;
            this.dataListPrnt1.AllowUserToDeleteRows = false;
            this.dataListPrnt1.AllowUserToResizeColumns = false;
            this.dataListPrnt1.AllowUserToResizeRows = false;
            dataGridViewCellStyle17.BackColor = Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle17.Font = new Font("Trebuchet MS", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = Color.Black;
            this.dataListPrnt1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dataListPrnt1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataListPrnt1.BackgroundColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.dataListPrnt1.BorderStyle = BorderStyle.None;
            this.dataListPrnt1.CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dataListPrnt1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.dataListPrnt1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            dataGridViewCellStyle18.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle18.SelectionBackColor = Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle18.SelectionForeColor = SystemColors.Desktop;
            dataGridViewCellStyle18.WrapMode = DataGridViewTriState.True;
            this.dataListPrnt1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataListPrnt1.ColumnHeadersHeight = 40;
            this.dataListPrnt1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = Color.White;
            dataGridViewCellStyle19.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = Color.Black;
            dataGridViewCellStyle19.SelectionBackColor = Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle19.SelectionForeColor = Color.Black;
            dataGridViewCellStyle19.WrapMode = DataGridViewTriState.False;
            this.dataListPrnt1.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataListPrnt1.GridColor = Color.Black;
            this.dataListPrnt1.Location = new Point(37, 162);
            this.dataListPrnt1.MultiSelect = false;
            this.dataListPrnt1.Name = "dataListPrnt1";
            this.dataListPrnt1.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(164)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle20.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            dataGridViewCellStyle20.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = DataGridViewTriState.True;
            this.dataListPrnt1.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataListPrnt1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataListPrnt1.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataListPrnt1.RowTemplate.Height = 40;
            this.dataListPrnt1.RowTemplate.ReadOnly = true;
            this.dataListPrnt1.RowTemplate.Resizable = DataGridViewTriState.False;
            this.dataListPrnt1.Size = new Size(376, 423);
            this.dataListPrnt1.TabIndex = 1;
            this.dataListPrnt1.SelectionChanged += new EventHandler(this.DataListOnSelectionChanged);

            // Label : Second Parent
            this.lblParent2.AutoSize = true;
            this.lblParent2.Font = new Font("Trebuchet MS", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblParent2.ForeColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            this.lblParent2.Location = new Point(466, 136);
            this.lblParent2.Name = "lblParent2";
            this.lblParent2.Size = new Size(173, 20);
            this.lblParent2.Text = "Second Parent (optional)";

            // DataGridView : Second set of residents
            this.dataListPrnt2.AllowUserToAddRows = false;
            this.dataListPrnt2.AllowUserToDeleteRows = false;
            this.dataListPrnt2.AllowUserToResizeColumns = false;
            this.dataListPrnt2.AllowUserToResizeRows = false;
            dataGridViewCellStyle21.BackColor = Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle21.Font = new Font("Trebuchet MS", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = Color.Black;
            this.dataListPrnt2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dataListPrnt2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataListPrnt2.BackgroundColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.dataListPrnt2.BorderStyle = BorderStyle.None;
            this.dataListPrnt2.CellBorderStyle = DataGridViewCellBorderStyle.RaisedHorizontal;
            this.dataListPrnt2.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.dataListPrnt2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            dataGridViewCellStyle22.Font = new Font("Calibri", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle22.SelectionBackColor = Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(236)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle22.SelectionForeColor = SystemColors.Desktop;
            dataGridViewCellStyle22.WrapMode = DataGridViewTriState.True;
            this.dataListPrnt2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dataListPrnt2.ColumnHeadersHeight = 40;
            this.dataListPrnt2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle23.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = Color.White;
            dataGridViewCellStyle23.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = Color.Black;
            dataGridViewCellStyle23.SelectionBackColor = Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle23.SelectionForeColor = Color.Black;
            dataGridViewCellStyle23.WrapMode = DataGridViewTriState.False;
            this.dataListPrnt2.DefaultCellStyle = dataGridViewCellStyle23;
            this.dataListPrnt2.GridColor = Color.Black;
            this.dataListPrnt2.Location = new Point(466, 162);
            this.dataListPrnt2.MultiSelect = false;
            this.dataListPrnt2.Name = "dataListPrnt2";
            this.dataListPrnt2.ReadOnly = true;
            dataGridViewCellStyle24.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.BackColor = Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(164)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle24.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            dataGridViewCellStyle24.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = DataGridViewTriState.True;
            this.dataListPrnt2.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dataListPrnt2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataListPrnt2.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataListPrnt2.RowTemplate.Height = 40;
            this.dataListPrnt2.RowTemplate.ReadOnly = true;
            this.dataListPrnt2.RowTemplate.Resizable = DataGridViewTriState.False;
            this.dataListPrnt2.Size = new Size(376, 423);
            this.dataListPrnt2.TabIndex = 2;


            // Label : Family Members
            this.lblFamilyMembers.AutoSize = true;
            this.lblFamilyMembers.Font = new Font("Trebuchet MS", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblFamilyMembers.ForeColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(39)))), ((int)(((byte)(78)))));
            this.lblFamilyMembers.Location = new Point(49, 626);
            this.lblFamilyMembers.Name = "lblFirstName";
            this.lblFamilyMembers.Size = new Size(124, 22);
            this.lblFamilyMembers.Text = "Family Members";

            // Number : Family Members 
            this.numFamilyMember.BackColor = Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.numFamilyMember.BorderStyle = BorderStyle.None;
            this.numFamilyMember.Font = new Font("Calibri", 12.75F, FontStyle.Bold);
            this.numFamilyMember.Location = new Point(183, 627);
            this.numFamilyMember.Name = "numFamilyMember";
            this.numFamilyMember.Size = new Size(156, 24);
            this.numFamilyMember.TabIndex = 3;
            this.numFamilyMember.Value = 0;

            // Button
            this.btnCreate.BackColor = Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(117)))), ((int)(((byte)(216)))));
            this.btnCreate.FlatStyle = FlatStyle.Flat;
            this.btnCreate.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.btnCreate.Location = new Point(745, 618);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new Size(97, 37);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new EventHandler(this.CreateOnClick);

            // Design Components
            this.dsnLlbl.AutoSize = true;
            this.dsnLlbl.BackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.dsnLlbl.Font = new Font("Trebuchet MS", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.dsnLlbl.ForeColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.dsnLlbl.Location = new Point(9, 12);
            this.dsnLlbl.Name = "dsnLlbl";
            this.dsnLlbl.Size = new Size(372, 24);
            this.dsnLlbl.Text = "Barangay Management Information System";
            this.dsnBox.BackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.dsnBox.Location = new Point(-3, -1);
            this.dsnBox.Name = "dsnBox";
            this.dsnBox.Size = new Size(888, 51);
            this.dsnLine.BackColor = Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.dsnLine.Location = new Point(183, 652);
            this.dsnLine.Name = "dsnLine";
            this.dsnLine.Size = new Size(156, 3);

            // Actual Form
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new Size(884, 713);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.Name = "AddFamilyView";
            this.Text = "AddFamilyView";

            // Load Control into Form
            this.Controls.Add(this.lblParent2);
            this.Controls.Add(this.lblParent1);
            this.Controls.Add(this.numFamilyMember);
            this.Controls.Add(this.dsnLine);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dataListPrnt2);
            this.Controls.Add(this.dataListPrnt1);
            this.Controls.Add(this.lblResidents);
            this.Controls.Add(this.lblFamilyMembers);
            this.Controls.Add(this.dsnLlbl);
            this.Controls.Add(this.dsnBox);
        }

        private Label dsnLlbl;
        private PictureBox dsnBox;
        private Label lblResidents;
        private DataGridView dataListPrnt1;
        private DataGridView dataListPrnt2;
        private Button btnCreate;
        private PictureBox dsnLine;
        private Label lblFamilyMembers;
        private NumericUpDown numFamilyMember;
        private Label lblParent1;
        private Label lblParent2;

    }
}