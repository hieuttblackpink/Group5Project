namespace Group5Project
{
    class SVDTO
    {
        private int maSV;
        private string hoSV;
        private string tenSV;
        private int nam;
        private string ngaySinh;
        private string gioiTinh;
        private string maKhoa;

        public SVDTO()
        {
            
        }

        public SVDTO(int maSV, string hoSV, string tenSV, int nam, string ngaySinh, string gioiTinh, string maKhoa)
        {
            this.MaSV = maSV;
            this.HoSV = hoSV;
            this.TenSV = tenSV;
            this.Nam = nam;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.MaKhoa = maKhoa;
        }

        public int MaSV { get => maSV; set => maSV = value; }
        public string HoSV { get => hoSV; set => hoSV = value; }
        public string TenSV { get => tenSV; set => tenSV = value; }
        public int Nam { get => nam; set => nam = value; }
        public string NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string MaKhoa { get => maKhoa; set => maKhoa = value; }
    }
}
