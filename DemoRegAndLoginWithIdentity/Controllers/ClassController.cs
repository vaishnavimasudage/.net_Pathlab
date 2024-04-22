namespace DemoRegAndLoginWithIdentity.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using static System.Net.Mime.MediaTypeNames;
    using System.Xml.Linq;
    using DemoRegAndLoginWithIdentity.Data;
    using DemoRegAndLoginWithIdentity.Models;
    using DemoRegAndLoginWithIdentity.Models.Domain;
    using Microsoft.AspNetCore.Authorization;

    namespace WebApplication2.Controllers
    {
        [Authorize(Roles = "Patient,Admin")]
        public class ClassController : Controller
        {
            private readonly MyDbContext mvcDemoDbContext;
            public ClassController(MyDbContext mvcDemoDbContext)
            {
                this.mvcDemoDbContext = mvcDemoDbContext;
            }

            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var classes = await mvcDemoDbContext.classes.ToListAsync();
                return View(classes);
            }

            [HttpGet]
            public IActionResult Add()
            {
                return View();
            }

            [HttpPost]

            public async Task<IActionResult> Add(AddClassView addPatientRequest)
            {
                var Class = new Class()
                {
                    id = Guid.NewGuid(),
                    name = addPatientRequest.name,
                    test = addPatientRequest.test,
                    time = addPatientRequest.time,
                    DateOfBirth = addPatientRequest.DateOfBirth,
                    BloodGroup = addPatientRequest.BloodGroup
                };

                await mvcDemoDbContext.classes.AddAsync(Class);
                await mvcDemoDbContext.SaveChangesAsync();
                return RedirectToAction("Index");


            }

            [Authorize(Roles = "Admin")]
            [HttpGet]
            public async Task<IActionResult> View(Guid id)
            {
                var Class = await mvcDemoDbContext.classes.FirstOrDefaultAsync(x => x.id == id);

                if (Class != null)
                {

                    var viewModel = new Update()
                    {
                        id = Class.id,
                        name = Class.name,
                        test = Class.test,
                        time = Class.time,
                        DateOfBirth = Class.DateOfBirth,
                        BloodGroup = Class.BloodGroup

                    };
                    return await Task.Run(() => View("View", viewModel));

                }

                return RedirectToAction("Index");
            }

            [Authorize(Roles = "Admin")]
            [HttpPost]

            public async Task<IActionResult> View(Update update)
            { 
                var Class = await mvcDemoDbContext.classes.FindAsync(update.id);

                if (Class != null)
                {
                    Class.id = update.id;
                    Class.name = update.name;
                    Class.test = update.test;
                    Class.time = update.time;
                    Class.DateOfBirth = update.DateOfBirth;
                    Class.BloodGroup = update.BloodGroup;

                    await mvcDemoDbContext.SaveChangesAsync();

                    return RedirectToAction("Index");

                }
                return RedirectToAction("Index");

            }

            [HttpPost]
            public async Task<IActionResult> Delete(Update update)
            {
                // var Class = mvcDemoDbContext.classes.FindAsync(update.id);
                var Class = await mvcDemoDbContext.classes.FindAsync(update.id);

                if (Class != null)
                {
                    mvcDemoDbContext.classes.Remove(Class);
                    await mvcDemoDbContext.SaveChangesAsync();

                    return RedirectToAction("Index");

                }

                return RedirectToAction("Index");
            }
        }
    }
}
