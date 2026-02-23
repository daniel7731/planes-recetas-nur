namespace PlanesRecetas.webapi.Parameters.Plan
{
    public class DietaRectaParameter
    {
        public int Orden { get; set; }
        public int TiempoId { get; set; } // 1: Desayuno, 2: Almuerzo, 3: Cena, etc.
        public Guid RecetaId { get; set; }
    }
}
