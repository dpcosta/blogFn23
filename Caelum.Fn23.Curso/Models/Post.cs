using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caelum.Fn23.Curso.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(255)]
        public string Resumo { get; set; }

        public string Categoria { get; set; }

        [Display(Name = "Data de Publicação")]
        public DateTime? DataPublicacao { get; set; }

        public bool Publicado { get; set; }

        public virtual Usuario Autor { get; set; }
        public string AutorId { get; set; }
    }
}