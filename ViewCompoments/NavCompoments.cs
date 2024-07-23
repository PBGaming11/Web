using Microsoft.AspNetCore.Mvc;
using Web.Data;
namespace Web.ViewCompoments

{
    public class NavCompoments : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public NavCompoments(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var danhmuc = _db.danhmuc.ToList();
            return View(danhmuc);
        }
    }
}
