using Project.Core;
using Project.Core.Models;
using Project.Core.Objects;
using System;
using System.Linq;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using System.Net;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
namespace Project.Application.Controllers
{
    public class ApplicantsController : Controller
    {
        private CompanyDbContext db = new CompanyDbContext();
        IApplicantResponsibility context = new ApplicantResponsibility();
        public bool VerifyEmail(string emailVerify)
        {
            using (WebClient webclient = new WebClient())
            {
                string url = "http://verify-email.org/";
                NameValueCollection formData = new NameValueCollection();
                formData["check"] = emailVerify;
                byte[] responseBytes = webclient.UploadValues(url, "POST", formData);
                string response = Encoding.ASCII.GetString(responseBytes);
                if (response.Contains("Result: Ok"))
                {
                    return true;
                }
                return false;
            }
        }
        public ActionResult Error()
        {
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
            {
                Vacancy vacancy = db.Vacancies.SingleOrDefault(p => p.Id == id.Value);
                if (vacancy != null)
                {
                    ViewBag.vacancyId = id;
                    return View();
                }
            }
            return RedirectToAction("Index", "Vacancies");

        }
        [HttpPost]
        public ActionResult Create(FormCollection f)
        {
            string email = f["Email"];
            string phone = f["Phone"];
            if (!this.IsCaptchaValid("Validate your captcha"))
            {
                ViewBag.ErrorMessage = "Invalid Captcha";
                ViewBag.vacancyId = Int32.Parse(f["vacancyId"]);
                return View();
            }
            bool CheckVerifyEmail = VerifyEmail(email);
            if (!CheckVerifyEmail)
            {
                TempData["StatusMessage"] = "Invalid Email";
                ViewBag.vacancyId = Int32.Parse(f["vacancyId"]);
                return View();
            }
            TempData["StatusMessage"] = "You have applied for this job";
            Applicant CheckApplicant = context.GetApplicant(email, phone);
            if (CheckApplicant == null)
            {
                Applicant applicant = new Applicant();
                applicant.Name = f["Name"];
                applicant.Phone = f["Phone"];
                applicant.Status = true;
                applicant.Experience = f["Experience"];
                applicant.BirthDay = Convert.ToDateTime(f["BirthDay"]);
                applicant.Address = f["Address"];
                applicant.Email = f["Email"];
                applicant.AcademicLevel = f["AcademicLevel"];
                applicant.CreateAt = DateTime.Now;
                CheckApplicant = context.AddApplication(applicant);

            }
            if (CheckApplicant.Status == false)
            {
                TempData["messErr"] = "You applied to company.";
                ViewBag.vacancyId = Int32.Parse(f["vacancyId"]);
                return View();
            }
            bool NotExistApplicant = true;
            foreach (var item in context.GetVaCancyApplicantsForVacancy(Int32.Parse(f["vacancyId"])))
            {
                if (item.ApplicantId == CheckApplicant.Id)
                {
                    NotExistApplicant = false;
                }
            }
            if (!NotExistApplicant)
            {
                TempData["messErr"] = "You applied to this Job.";
                ViewBag.vacancyId = Int32.Parse(f["vacancyId"]);
                return View();
            }
            VacancyApplicant vacancyApplicant = new VacancyApplicant();
            vacancyApplicant.ApplicantId = CheckApplicant.Id;
            vacancyApplicant.VacancyId = Int32.Parse(f["vacancyId"]);
            vacancyApplicant.InterviewTime = Convert.ToDateTime(f["InterviewTime"]);
            vacancyApplicant.Status = 0;
            vacancyApplicant.CreateAt = DateTime.Now;
            vacancyApplicant.InterviewId = 1;
            context.AddVacancyApplicant(vacancyApplicant);
            return RedirectToAction("Details", "Vacancies", new { id = Int32.Parse(f["vacancyId"]) });
        }

    }
}
