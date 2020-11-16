using Microsoft.Extensions.Configuration;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ProjetoGama.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    { 
        private readonly IConfiguration _configuration;
        public GenreRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Genre>> GetGenreByIdAsync(List<int> id)
        {
            try
            {

                var genres = new List<Genre>();

                foreach (int element in id)
                { 
                    using (var con = new SqlConnection(_configuration["ConnectionString"]))
                    {
                        var sqlCmd = $@"SELECT GenreId,
                                               Description 
                                        FROM [GENRE] 
                                    WHERE GenreId={element}";

                        using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            con.Open();

                            var reader = await cmd
                                                .ExecuteReaderAsync()
                                                .ConfigureAwait(false);

                            while (reader.Read())
                            {
                                var genre = new Genre(int.Parse(reader["GenreId"].ToString()),
                                                    reader["Description"].ToString());

                                genres.Add(genre);
                            }

                        }
                    }
                }

                return genres;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
