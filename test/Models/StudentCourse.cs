﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[PrimaryKey("StId", "CrsId")]
[Table("StudentCourse")]
public partial class StudentCourse
{
    [Key]
    [Column("StID")]
    public int StId { get; set; }

    [Key]
    [Column("CrsID")]
    public int CrsId { get; set; }

    public double? Grade { get; set; }

    [ForeignKey("CrsId")]
    [InverseProperty("StudentCourses")]
    public virtual Course Crs { get; set; }

    [ForeignKey("StId")]
    [InverseProperty("StudentCourses")]
    public virtual Student St { get; set; }
}