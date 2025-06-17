using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoMatrixSystem.Domain.Common
{
    public class MyEntity : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Establece el nivel de actualización de los cambios aplicados
        /// </summary>
        public void SetChanges(OpResul opResul)
        {
            AppChanges.SetChanges(opResul);
        }

        public virtual string ToString()
        {
            return string.Empty;
        }
    }
}
