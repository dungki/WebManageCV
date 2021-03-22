using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Core.Objects
{
    public partial class Vacancy
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*Please enter a name")]
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "*Please enter a Language")]
        public string Language { get; set; }
        [Required(ErrorMessage = "*Please enter a Location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "*Please enter a Salary")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "*Enter a number")]
        public string Salary { get; set; }
        [Required(ErrorMessage = "*Please enter a Experience")]
        public string Experience { get; set; }
        public DateTime Posted { get; set; }
        [Required(ErrorMessage = "*Please enter a Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "*Please enter a Odernumber")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "*Enter a number > 0")]
        public int OrderNumber { get; set; }
        public int AccsessNumber { get; set; }
        public Nullable<DateTime> CloseDate { get; set; }
        public int CreateBy { get; set; }


        public virtual ICollection<VacancyApplicant> VacancyApplicants { get; set; }

    }
}
