using Frontend.Models;
using Frontend.Models.ViewModels;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Frontend.Controllers
{
    public class BPKBController : Controller
    {
        private readonly BPKBService _bpkbService;

        public BPKBController(BPKBService bpkbService)
        {
            _bpkbService = bpkbService;
        }

        public IActionResult Login()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username != null)
            {
                return RedirectToAction("Index", "BPKB");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MsUser model)
        {
            if (ModelState.IsValid)
            {
                var isValidUser = await _bpkbService.ValidateUserAsync(model);

                if (isValidUser)
                {
                    HttpContext.Session.SetString("Username", model.Username);

                    return RedirectToAction("Index", "BPKB");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username == null)
            {
                return RedirectToAction("Login", "BPKB");
            }

            var trBpkbs = await _bpkbService.GetAllTrBpkbsAsync();
            var locations = await _bpkbService.GetAllLocationsAsync();

            var trBpkbViewModels = trBpkbs.Select(trBpkb => new BPKBViewModel
            {
                AgreementNumber = trBpkb.AgreementNumber,
                BpkbNo = trBpkb.BpkbNo,
                BranchId = trBpkb.BranchId,
                BpkbDate = trBpkb.BpkbDate,
                FakturNo = trBpkb.FakturNo,
                FakturDate = trBpkb.FakturDate,
                LocationName = locations.FirstOrDefault(l => l.LocationId == trBpkb.LocationId)?.LocationName,
                PoliceNo = trBpkb.PoliceNo,
                BpkbDateIn = trBpkb.BpkbDateIn,
                CreatedBy = trBpkb.CreatedBy,
                CreatedOn = trBpkb.CreatedOn,
                LastUpdatedBy = trBpkb.LastUpdatedBy,
                LastUpdatedOn = trBpkb.LastUpdatedOn
            });

            return View(trBpkbViewModels);
        }


        public async Task<IActionResult> Create()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username == null)
            {
                return RedirectToAction("Login", "BPKB");
            }

            var locations = await _bpkbService.GetAllLocationsAsync();
            var viewModel = new BPKBCreateViewModel
            {
                Locations = locations
            };

            Console.WriteLine($"Locations count: {viewModel.Locations.Count()}");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BPKBCreateViewModel viewModel)
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                viewModel.TrBpkb.CreatedBy = username;
                viewModel.TrBpkb.CreatedOn = DateTime.Now;
                viewModel.TrBpkb.LastUpdatedBy = username;
                viewModel.TrBpkb.LastUpdatedOn = DateTime.Now;
                var createdBpkb = await _bpkbService.CreateTrBpkbAsync(viewModel.TrBpkb);

                if (createdBpkb != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to create the transaction.");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            var locations = await _bpkbService.GetAllLocationsAsync();
            viewModel.Locations = locations;

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var username = HttpContext.Session.GetString("Username");

            if (username == null)
            {
                return RedirectToAction("Login", "BPKB");
            }

            var trBpkb = await _bpkbService.GetTrBpkbAsync(id);
            if (trBpkb == null)
            {
                return NotFound();
            }

            var locations = await _bpkbService.GetAllLocationsAsync();

            var viewModel = new BPKBEditViewModel
            {
                TrBpkb = trBpkb,
                Locations = locations
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BPKBEditViewModel viewModel)
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                viewModel.TrBpkb.LastUpdatedBy = username;
                viewModel.TrBpkb.LastUpdatedOn = DateTime.Now;
                var updatedBpkb = await _bpkbService.UpdateTrBpkbAsync(viewModel.TrBpkb);

                if (updatedBpkb != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to update the transaction.");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            viewModel.Locations = await _bpkbService.GetAllLocationsAsync();

            return View(viewModel);
        }
    }
}
