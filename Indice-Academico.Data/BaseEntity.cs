using System;
namespace Indice_Academico.Data
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = new Guid();

        public DateTime Timestamp { get; set; }

    }
}
