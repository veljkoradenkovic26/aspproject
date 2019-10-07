using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Comment;
using Application.Commands.News;
using Application.DataTransfer;
using Application.DataTransfer.Create;
using Application.Exceptions;
using Application.Interfaces;
using Application.SearchObjects;
using EfDataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    public class CommentController : Controller
    {
        private IGetNewsCommand _getNewsCommand;
        private IGetCommentsCommand _getCommentsCommand;
        private IGetCommentCommand _getCommentCommand;
        private IAddCommentCommand _addComment;
        private IDeleteCommentCommand _deleteCommentCommand;
        private IEditCommentCommand _editCommentCommand;
        private IEmailSender _emailSender;
        private NewsContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CommentController(IGetNewsCommand getNewsCommand, IGetCommentsCommand getCommentsCommand, IGetCommentCommand getCommentCommand, IAddCommentCommand addComment, IDeleteCommentCommand deleteCommentCommand, IEditCommentCommand editCommentCommand, IEmailSender emailSender, NewsContext context, IHostingEnvironment hostingEnvironment)
        {
            _getNewsCommand = getNewsCommand;
            _getCommentsCommand = getCommentsCommand;
            _getCommentCommand = getCommentCommand;
            _addComment = addComment;
            _deleteCommentCommand = deleteCommentCommand;
            _editCommentCommand = editCommentCommand;
            _emailSender = emailSender;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: Comment
        public ActionResult Index(CommentSearch search)
        {
            var dto = _getCommentsCommand.Execute(search);
            return View(dto);
        }

        // GET: Comment/Details/5
        //[LoggedIn]
        public ActionResult Details(int id)
        {
            var dto = _getCommentCommand.Execute(id);
            return View(dto);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            ViewBag.News = _context.News.Select(n => new NewsCommentDto {
                Heading = n.Heading,
                NewsId = n.Id
            });
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["greska"] = "Doslo je do greske pri unosu";
                RedirectToAction("create");
            }
            try
            {
                _addComment.Execute(dto);
                _emailSender.Subject = "Uspesno ste postavili komentar";
                _emailSender.ToEmail = dto.Email;
                _emailSender.Body = "Hvala na komentaru";
                _emailSender.Send();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                TempData["error"] = e.Message;
                ViewBag.News = _context.News.Select(n => new NewsCommentDto
                {
                    Heading = n.Heading,
                    NewsId = n.Id
                });
                return View();
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var dto = _getCommentCommand.Execute(id);
                ViewBag.News = _context.News.Select(n => new NewsCommentDto
                {
                    Heading = n.Heading,
                    NewsId = n.Id
                });
                return View(dto);
            }
            catch(EntityNotFoundException ex)
            {
                TempData["error"] = ex.Message;

            }
            catch (Exception)
            {
                TempData["greska"] = "Doslo je do greske.";
            }
            return View();

        }

        // POST: Comment/Edit/5
        //[LoggedIn("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateCommentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                TempData["greska"] = "Doslo je do greske pri unosu";
                RedirectToAction("Edit");
            }
            try
            {
                updateDto.CommentId = id;
                _editCommentCommand.Execute(updateDto);

                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        // GET: Comment/Delete/5
        //[LoggedIn("Admin")]
        public ActionResult Delete(int id)
        {
            var dto = _getCommentCommand.Execute(id);
            return View(dto);
        }

        // POST: Comment/Delete/5
       // [LoggedIn("Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _deleteCommentCommand.Execute(id);
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