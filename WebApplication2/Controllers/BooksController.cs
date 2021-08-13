using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    //Not protected from public users ?
    public class BooksController : Controller
    {

        public readonly ApplicationDbContext _db;
        [BindProperty]
        public Book Book { set; get; }

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.Get("email") == null)
            {
                return RedirectToAction("Login", "Users", new {area = ""});
            }
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if (HttpContext.Session.Get("email") == null)
            {
               return RedirectToAction("Login", "Users", new { area = "" });
            }
            Book = new Book();
            if (id == null)
            {
                // insert
                return View(Book);
            }
            // update
            Book = _db.Books.Find(id);
            if(Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }

        [HttpPost]
        public IActionResult Upsert()
        {

            if(ModelState.IsValid)
            {
                if(Book.Id == 0) // insert
                {
                    _db.Books.Add(Book);
                }
                else
                {
                    _db.Books.Update(Book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Book);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            //Oreturn Ok()
            return Json(new { success=true, message="successfully deleted" });
        }
        #endregion
    }
}
