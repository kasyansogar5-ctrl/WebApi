using ClienteWeb.Models;

namespace ClienteWeb.Servicios
{
    public class CursoServicio
    {
        private readonly HttpClient _httpClient;
        private readonly string Ruta = "api/Cursos";

        public CursoServicio(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CursoDto>> GetAllCursos()
        {
            var res = await _httpClient.GetFromJsonAsync<List<CursoDto>>(Ruta);
            if (res == null)
            {
                return new List<CursoDto>();
            }
            return res.ToList();
        }
    }
}
