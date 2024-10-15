using AutoMapper;
using BLL.InterFace;
using CRUD.Helper;
using CRUD.Models;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics.Metrics;

namespace CRUD.Controllers
{
    public class CounteryController : Controller
    {
        private readonly IGenaricRepository<Countery<int>, int> _genaricRepository;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        public CounteryController(
            IGenaricRepository<Countery<int>,int> genaricRepository,
            IMapper mapper,
            IToastNotification toastNotification
            )
        {
            _genaricRepository = genaricRepository;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var result = await  _genaricRepository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<CounteryViewModel>>(result);
            return View(mapped);
        }
        public async Task<IActionResult> Create()
        {
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CounteryViewModel model)
        {
           

            if (ModelState.IsValid)
            {
                

                var mapped = _mapper.Map<Countery<int>>(model);
                var count = await _genaricRepository.AddAsync(mapped);
                if (count > 0)
                {
                    _toastNotification.AddSuccessToastMessage("Countery Created Successfully");
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Inalid Created Countery");
            return View(model);
           

        }
        public async Task<IActionResult> Details(int? id)
        {

            if (id is null) return BadRequest();

            var result = await _genaricRepository.GetAsync(id.Value);
            if (result is null) return NotFound();

            var mapped = _mapper.Map<CounteryViewModel>(result);

            return View(mapped);
        }
        public async Task<IActionResult> Edit(int? id)
        {
         
            if (id is null) return BadRequest();

            var result = await _genaricRepository.GetAsync(id.Value);
            if (result is null) return NotFound();

            var mapped = _mapper.Map<CounteryViewModel>(result);

            return View(mapped);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CounteryViewModel model)
        {
           
            var mapped = _mapper.Map<Countery<int>>(model);

            var result = _genaricRepository.Update(mapped);

            if (result > 0)
            {
                _toastNotification.AddSuccessToastMessage("Countery Edited Successfully");
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
