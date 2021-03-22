﻿using Project.Core.Objects;
using System.Collections.Generic;
namespace Project.Core.Models
{
    public interface IApplicantResponsibility
    {
        List<Applicant> GetApplicants();
        Applicant GetApplicant(string Email, string Phone);
        Applicant AddApplication(Applicant applicant);
        void AddVacancyApplicant(VacancyApplicant vacancyApplicant);
        List<VacancyApplicant> GetVaCancyApplicantsForVacancy(int id);
    }
}
