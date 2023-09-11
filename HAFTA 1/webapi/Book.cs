using System;
using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "GenreId is required")]
    public int GenreId { get; set; }

    [Required(ErrorMessage = "PageCount is required")]
    public int PageCount { get; set; }

    [Required(ErrorMessage = "PublishDate is required")]
    public DateTime PublishDate { get; set; }
}
