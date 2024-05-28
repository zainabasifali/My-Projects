using System;
using System.Collections.Generic;

namespace MyProject.Models;

public partial class Task
{
    public int Id { get; set; }

    public string? TaskName { get; set; }

    public string? TaskType { get; set; }

    public string? TaskDescription { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? Duration { get; set; }
}
