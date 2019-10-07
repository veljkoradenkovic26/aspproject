using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Category;
using Application.Commands.News;
using Application.DataTransfer;
using Application.DataTransfer.Create;
using Application.Exceptions;
using Application.Responses;
using Application.SearchObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class NewsController : Controller
    {
        private IGetNewsCommand _getNewsCommand;
        private IGetNewCommand _getNewCommand;
        private IAddNewsCommand _addNewsCommand;
        private IEditNewsCommand _editNewsCommand;
        private IDeleteNewsCommand _deleteNewsCommand;
        private IGetCategoriesCommand _getCategoriesCommand;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserDto _user;

        public NewsController(IGetNewsCommand getNewsCommand, IGetNewCommand getNewCommand, IAddNewsCommand addNewsCommand, IEditNewsCommand editNewsCommand, IDeleteNewsCommand deleteNewsCommand, IGetCategoriesCommand getCategoriesCommand, IHostingEnvironment hostingEnvironment, UserDto user)
        {
            _getNewsCommand = getNewsCommand;
            _getNewCommand = getNewCommand;
            _addNewsCommand = addNewsCommand;
            _editNewsCommand = editNewsCommand;
            _deleteNewsCommand = deleteNewsCommand;
            _getCategoriesCommand = getCategoriesCommand;
            _hostingEnvironment = hostingEnvironment;
            _user = user;
        }

        // GET: News
        public ActionResult Index(NewsSearch search)
        {
            var dto = _getNewsCommand.Execute(search);
            var user = HttpContext.Session.GetObjectFromJson<UserDto>();
            return View(dto);
            
        }

        // GET: News/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var dto = _getNewCommand.Execute(id);
                return View(dto);
            }
            catch (EntityNotFoundException ex)
            {
                TempData["error"] = ex.Message;
            }
            catch (Exception e)
            {
                TempData["greska"] = "Doslo je do greske.";
            }
            return View();
        }
        
        //[LoggedIn("Admin")]
        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.Categories = _getCategoriesCommand.Execute(null);
            return View();
        }

        // POST: News/Create
        //[LoggedIn("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewsDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["greska"] = "Doslo je do greske pri unosu";
                RedirectToAction("create");
            }
            try
            {
                _addNewsCommand.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Whoops! Something went wrong!";
                ViewBag.Categories = _getCategoriesCommand.Execute(null);
                return View();
            }
        }

        // GET: News/Edit/5
        [LoggedIn("Admin")]
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getNewCommand.Execute(id);
                ViewBag.Categories = _getCategoriesCommand.Execute(null);
                return View(dto);
            }
            catch (EntityNotFoundException ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }

        }

        // POST: News/Edit/5
        [LoggedIn("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateNewsDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["greska"] = "Doslo je do greske pri unosu";
                RedirectToAction("Edit");
            }
            try
            {
                dto.NewsId = id;
                var pathSave = "";
                if (dto.Image != null) {
                    var newFileName = Guid.NewGuid().ToString() + "_" + dto.Image.FileName;
                    var webPath = _hostingEnvironment.WebRootPath;
                    var uploads = Path.Combine("", webPath + @"\uploads\" + newFileName);
                    pathSave = @"/uploads/" + newFileName;
                    dto.Image.CopyTo(new FileStream(uploads, FileMode.Create));
                }
               
                dto.Path = pathSave;
                _editNewsCommand.Execute(dto);

                return RedirectToAction(nameof(Index));
            }
            catch(EntityNotFoundException ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        // GET: News/Delete/5
        [LoggedIn("Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                var dto = _getNewCommand.Execute(id);
                return View(dto);
            }
            catch (EntityNotFoundException ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        // POST: News/Delete/5
        [LoggedIn("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, NewsDto newsDto)
        {
            try
            {
                _deleteNewsCommand.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch(EntityNotFoundException ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }
    }
}