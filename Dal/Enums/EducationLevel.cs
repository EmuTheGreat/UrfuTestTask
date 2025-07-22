using System.ComponentModel.DataAnnotations;

namespace Dal.Enums;

/// <summary>
/// Уровни обучения
/// </summary>
public enum EducationLevel
{
    [Display(Name = "Бакалавр")]
    Bachelor = 0,

    [Display(Name = "Прикладной бакалавриат")]
    AppliedBachelor = 1,

    [Display(Name = "Специалист")]
    Specialist = 2,

    [Display(Name = "Магистр")]
    Master = 3,

    [Display(Name = "Аспирант")]
    Postgraduate = 4
}