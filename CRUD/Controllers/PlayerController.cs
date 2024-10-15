using BLL.InterFace;
using CRUD.Models;
using DAL.Model;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CRUD.Helper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;
using NToastNotify;
using DAL.Data.Context;

namespace CRUD.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IGenaricRepository<Player<int>, int> _genaricRepository;
        private readonly IGenaricRepository<Countery<int>, int> _counterRepository;
        private readonly IToastNotification _toastNotification;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayerController(
            IGenaricRepository<Player<int>, int> genaricRepository,
            IGenaricRepository<Countery<int>, int> counterRepository,
            IMapper mapper,
            IToastNotification toastNotification,
            ApplicationDbContext context
            )
        {
            _genaricRepository = genaricRepository;
            _counterRepository = counterRepository;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _genaricRepository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<PlayersViewModel>>(result);
            return View(mapped);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var countery = await _counterRepository.GetAllAsync();
            ViewData["countery"] = countery;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayersViewModel model)
        {
            model.Poster = DocumentSetting.UploadFile(model.PosterImage, "Image");

            if (ModelState.IsValid)
            {
                //if (model.PosterImage is not null)
                //{
                //    model.Poster = DocumentSetting.UploadFile(model.PosterImage, "Image");

                //}

                var mapped = _mapper.Map<Player<int>>(model);
                var count = await _genaricRepository.AddAsync(mapped);
                if (count > 0)
                {
                    _toastNotification.AddSuccessToastMessage("Player Created Successfully");
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Inalid Created Player");
            return View(model);
            #region MyRegion
            //if (ModelState.IsValid)
            //{
            //    var files = Request.Form.Files;
            //    if (!files.Any())
            //    {
            //        model.Countery = await _counterRepository.GetAllAsync();
            //        ModelState.AddModelError("Poster", "Please Enter Poster");
            //        return View(model);
            //    }
            //    var poster = files.FirstOrDefault();
            //    var allowedExtenstion = new List<string> { ".png", ".jpg" };
            //    if (!allowedExtenstion.Contains(Path.GetExtension(poster.FileName).ToLower()))
            //    {
            //        model.Countery = await _counterRepository.GetAllAsync();
            //        ModelState.AddModelError("Poster", "Please Enter Poster(.png or .jpg)");
            //        return View(model);
            //    }
            //    if (poster.Length > 1048576)
            //    {
            //        model.Countery = await _counterRepository.GetAllAsync();
            //        ModelState.AddModelError("Poster", "Please Enter Poster less 1MB");
            //        return View(model);
            //    }
            //    using var datastream = new MemoryStream();
            //    await poster.CopyToAsync(datastream);

            //    var play = new Player<int>
            //    {
            //        CounteryId = model.CounteryId,
            //        Description = model.Description,
            //        Name = model.Name,
            //        YearBirth = model.YearBirth,
            //        Rate = model.Rate,
            //        Poster = datastream.ToArray()
            //    };

            //   await _genaricRepository.AddAsync(play);
            //    return RedirectToAction("Index");
            //}

            //model.Countery = await _counterRepository.GetAllAsync();
            //return View(model); 
            #endregion

        }
        public async Task<IActionResult> Details(int? id,string vieName = "Details")
        {
            var countery = await _counterRepository.GetAllAsync();
            ViewData["countery"] = countery;

            if (id is null) return BadRequest();

            var result = await _genaricRepository.GetAsync(id.Value);
            if (result is null) return NotFound();

            var mapped = _mapper.Map<PlayersViewModel>(result);

            return View(vieName,mapped);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var countery = await _counterRepository.GetAllAsync();
            ViewData["countery"] = countery;
            if (id is null) return BadRequest();

            var result = await _genaricRepository.GetAsync(id.Value);
            if (result is null) return NotFound();

            var mapped = _mapper.Map<PlayersViewModel>(result);

            return View(mapped);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlayersViewModel model)
        {
            if (model.Poster is not null)
            {
                DocumentSetting.DeleteFile(model.Poster, "Image");
            }
            if (model.PosterImage is not null)
            {
                model.Poster = DocumentSetting.UploadFile(model.PosterImage, "Image");

            }
            var mapped = _mapper.Map<Player<int>>(model);

            var result =  _genaricRepository.Update(mapped);

            if (result > 0)
            {
                _toastNotification.AddSuccessToastMessage("Player Edited Successfully");
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var result = await _genaricRepository.GetAsync(id.Value);
            if (result is null) return NotFound();

              _genaricRepository.Delete(result);

            return Ok();
        }

    }
}
