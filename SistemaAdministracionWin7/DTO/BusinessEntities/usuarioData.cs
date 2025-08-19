namespace DTO.BusinessEntities
{
    public enum GrupoCliente
    {
        CalzadosMell = 1,
        Opiparo = 2,
        Chinela = 3,
        Slipak = 4,
        Balarino=5

    }
    public  class usuarioData
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string hashPassword { get; set; }
        public GrupoCliente Cliente { get; set; }
    }
}
