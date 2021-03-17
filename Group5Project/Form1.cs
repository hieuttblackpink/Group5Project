using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group5Project
{
    public partial class Form1 : Form
    {
        public static DataSet dsSV = new DataSet("SVDB");
        private List<SVDTO> listSV = new List<SVDTO>();
        //public static SqlConnection con = new SqlConnection(@"uid=sa;pwd=blackpink9999;
        //                                                      Initial Catalog=QLSVien;Data Source=SE141080\SQLEXPRESS");

        public Form1()
        {
            InitializeComponent();

            loadData();
        }

        private void loadData ()
        {
            string str = @"uid=sa;pwd=blackpink9999;Initial Catalog=QLSVien;Data Source=SE141080\SQLEXPRESS";

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM SVIEN", str);

            dataAdapter.Fill(dsSV, "SVIEN");

            for (int i = 0; i < dsSV.Tables["SVIEN"].Rows.Count; i++)
            {
                listMSSV.Items.Add(dsSV.Tables["SVIEN"].Rows[i]["MASV"]);

                int maSV = Int32.Parse(dsSV.Tables["SVIEN"].Rows[i]["MASV"].ToString());
                string hoSV = dsSV.Tables["SVIEN"].Rows[i]["HOSV"].ToString();
                string tenSV = dsSV.Tables["SVIEN"].Rows[i]["TEN"].ToString();
                int nam = Int32.Parse(dsSV.Tables["SVIEN"].Rows[i]["NAM"].ToString());
                string ngaySinh = dsSV.Tables["SVIEN"].Rows[i]["NGAYSINH"].ToString();
                string gioiTinh = dsSV.Tables["SVIEN"].Rows[i]["GIOITINH"].ToString();
                string maKhoa = dsSV.Tables["SVIEN"].Rows[i]["MAKH"].ToString();

                SVDTO svDTO = new SVDTO(maSV, hoSV, tenSV, nam, ngaySinh, gioiTinh, maKhoa);
                listSV.Add(svDTO);
            }

            listMSSV.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            string findMSV = listMSSV.Text;

            bool foundSV = false;

            for (int i = 0; i < listSV.Count; i++)
            {
                if (findMSV.Equals(listSV[i].MaSV.ToString()))
                {
                    txtHo.Text = listSV[i].HoSV;
                    txtTen.Text = listSV[i].TenSV;
                    txtNgaysinh.Text = listSV[i].NgaySinh;
                    txtGioitinh.Text = listSV[i].GioiTinh;
                    txtMakhoa.Text = listSV[i].MaKhoa;
                    foundSV = true;
                    break;
                }
                else
                {
                    foundSV = false;
                }
            }

            if (foundSV == false)
            {
                txtHo.Text = "";
                txtTen.Text = "";
                txtNgaysinh.Text = "";
                txtGioitinh.Text = "";
                txtMakhoa.Text = "";
                txtDiemtb.Text = "";
                if (dsSV.Tables.Contains("MHOC"))
                {
                    dsSV.Tables.Remove("MHOC");
                }
                this.dataGridView1.DataSource = dsSV.Tables["MHOC"];
                this.dataGridView1.Refresh();
                this.dataGridView1.Parent.Refresh();
                MessageBox.Show("NOT FOUND!");
                return;
            }

            txtHo.ReadOnly = true;
            txtTen.ReadOnly = true;
            txtNgaysinh.ReadOnly = true;
            txtGioitinh.ReadOnly = true;
            txtMakhoa.ReadOnly = true;
            txtDiemtb.ReadOnly = true;

            calAverageMark(findMSV);
        }

        private void calAverageMark (string findMSV)
        {
            string str = @"uid=sa;pwd=blackpink9999;Initial Catalog=QLSVien;Data Source=SE141080\SQLEXPRESS";

            string sql = string.Format("SELECT t.MAMH AS 'MÃ MH', m.TENMH AS 'TÊN MN', t.DIEM AS 'ĐIỂM' "
                                     + "FROM MHOC AS m, (SELECT h.MAMH, h.MAHP, k.MASV, k.DIEM "
                                                      + "FROM HPHAN AS h, (SELECT MASV, MAHP, DIEM "
                                                                        + "FROM KQUA "
                                                                        + "WHERE MASV = {0}) AS k "
                                                                        + "WHERE h.MAHP = k.MAHP) AS t "
                                     + "WHERE m.MAMH = t.MAMH", findMSV);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, str);

            if (dsSV.Tables.Contains("MHOC"))
            {
                dsSV.Tables.Remove("MHOC");
            }

            dataAdapter.Fill(dsSV, "MHOC");
            dataAdapter.Update(dsSV.Tables["MHOC"]);

            int totalRecord = dsSV.Tables["MHOC"].Rows.Count;

            float totalMark = 0;

            for (int j = 0; j < totalRecord; j++)
            {
                totalMark += float.Parse(dsSV.Tables["MHOC"].Rows[j]["ĐIỂM"].ToString());
            }

            float averageMark = totalMark / totalRecord;

            this.dataGridView1.DataSource = dsSV.Tables["MHOC"];
            this.dataGridView1.Refresh();
            this.dataGridView1.Parent.Refresh();

            if (averageMark.ToString().Equals("NaN"))
            {
                txtDiemtb.Text = "";
            }
            else
            {
                txtDiemtb.Text = averageMark.ToString();
            }
        }
    }
}
