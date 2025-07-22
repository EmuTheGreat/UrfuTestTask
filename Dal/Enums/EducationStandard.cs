using System.ComponentModel.DataAnnotations;

namespace Dal.Enums;

/// <summary>
/// Стандарты обучения
/// </summary>
public enum EducationStandard
{
    [Display(Name = "СУОС")]
    SUOS = 0,

    [Display(Name = "ФГОС ВО")]
    FGOSVO = 1,

    [Display(Name = "СУТ")]
    SUT = 2,

    [Display(Name = "ФГОС ВПО")]
    FGOSVPO = 3,

    [Display(Name = "ФГОС 3++")]
    FGOS3PlusPlus = 4
}