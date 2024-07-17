using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace webTMDT.Controllers
{
    public class AddSanPhamController : Controller
    {
        //tạo _db thuộc tính chứa database
        private ApplicationDbContext _db;
        public AddSanPhamController(ApplicationDbContext db)//xét ký hiệu để gọi cho dễ
        {
            _db = db;
        }
        public IActionResult HienSanPham()//trang hiển thị sản phẩm trong database
        {
            var sanpham = _db.sanpham.Include(sp => sp.danhmuc).ToList();
            ViewBag.sanpham = sanpham;
            return View();
        }
        //httpget là trang của người dùng để gủi dữ liệu đến sever
        [HttpGet] 
        public IActionResult ThemSanpham()
        {
            ViewBag.DanhMucList = _db.danhmuc.ToList(); // dưa dữ liệu database thành dạnh bảng và cho nó vào ViewBag.DanhMucList
            return View();
        }
        //httpPost đây là chỗ sever sử lý dữ liệu người dùng
        [HttpPost] 
        public IActionResult ThemSanpham(SanPham sanpham) //lấy dữ liệu mà người dùng nhập trong models SanPham với tên gọi ra là sanpham
        {
            sanpham.danhmuc = _db.danhmuc.FirstOrDefault(dm => dm.Id == sanpham.DanhmucId);//lấy dữ liệu từ  DanhmucId để gọi bảng DanhMuc và lưu dữ liệu cùng id đó
            if (!ModelState.IsValid)
            {
                _db.Add(sanpham);//thêm mới vào database
                _db.SaveChanges();//sao lưu thay đổi
                return RedirectToAction(nameof(HienSanPham));//chuyển hướng về trang HienSanPham
            }
            return View();
        }
        [HttpGet]
        public IActionResult SuaSanpham(int id) //lấy id dữ liệu để hiển thị
        {
            ViewBag.DanhMucList = _db.danhmuc.ToList();
            if (id == 0)
            {
                return NotFound();
            }
            var sanpham = _db.sanpham.Find(id);
            return View(sanpham);
        }
        [HttpPost]
        public IActionResult SuaSanpham(SanPham sanpham)
        {
            sanpham.danhmuc = _db.danhmuc.FirstOrDefault(dm => dm.Id == sanpham.DanhmucId);
            if (!ModelState.IsValid)
            {
                _db.Update(sanpham);//cập Nhận thay đổi về database
                _db.SaveChanges();//lưu thay đổi
                return RedirectToAction(nameof(HienSanPham));
            }
            return View();
        }
        [HttpGet]
        public IActionResult XoaSanpham(int id)
        {
            ViewBag.DanhMucList = _db.danhmuc.ToList();
            if (id == 0)
            {
                return NotFound();
            }
            var sanpham = _db.sanpham.Find(id);
            return View(sanpham);
        }
        [HttpPost]
        public IActionResult XacNHanXoa(SanPham sanpham)
        {
            var sanphamToDelete = _db.sanpham.Find(sanpham.IdSanPham);
            if(sanphamToDelete == null)
            {
                return NotFound();
            }
            _db.sanpham.Remove(sanphamToDelete);//xoá dữ liệu từ database
            _db.SaveChanges();
            return RedirectToAction(nameof(HienSanPham));
        }
    }
}
