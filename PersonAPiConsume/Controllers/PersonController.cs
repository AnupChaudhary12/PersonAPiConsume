
using Microsoft.AspNetCore.Mvc;
using PersonApiConsume.Services.Implementation;
using PersonAPiConsume.Models;

namespace PersonApiConsume.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonApiService _personApiService;
        public PersonController(IPersonApiService personApiService)
        {
            _personApiService = personApiService;
        }
        public async Task<IActionResult> Persons()
        {
            List<Person> persons = new List<Person>();
            persons = await _personApiService.GetPersons();
            return View(persons);
        }

        public async Task<IActionResult> GetPersonById(int id)
        {
            Person person = await _personApiService.GetPersonById(id);
            return View(person);
        }
        [HttpGet("api/GetPersonById/{id}")]
        public async Task<IActionResult> GetPersonDetailsById(int id)
        {
            try
            {
                Person person = await _personApiService.GetPersonById(id);
                return View(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = await _personApiService.AddPerson(model);

                    if (isSuccess)
                    {
                        TempData["SuccessMessage"] = "Person added successfully!";
                        return RedirectToAction(nameof(Persons));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to add person. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personApiService.GetPersonById(id);

            if (person == null)
            {
                return NotFound(); 
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isSuccess = await _personApiService.DeletePerson(id);

            if (isSuccess)
            {
                return RedirectToAction(nameof(Persons)); 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete person. Please try again.");
                return RedirectToAction(nameof(Delete), new { id }); 
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePerson(int id)
        {
            var existingPerson = await _personApiService.GetPersonById(id);

            if (existingPerson == null)
            {
                return NotFound(); 
            }

            return View(existingPerson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePerson( Person model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = await _personApiService.UpdatePerson( model);

                    if (isSuccess)
                    {
                        TempData["SuccessMessage"] = "Person updated successfully!";
                        return RedirectToAction(nameof(Persons)); 
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to update person. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                }
            }

            return View(model);
        }
    }

}

