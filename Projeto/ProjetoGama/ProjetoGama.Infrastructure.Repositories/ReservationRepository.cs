using Microsoft.Extensions.Configuration;
using ProjetoGama.Domain.Entities;
using ProjetoGama.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGama.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly IConfiguration _configuration;
        public ReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Reservation> GetReservations()
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    con.Open();
                    var sqlCmd = @"SELECT ReservationDate,
                                            ReservationId,
                                            ProducerId,
                                            ActorId
                                         FROM RESERVATION";

                    using (var cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        var reader = cmd.ExecuteReader();

                        var reservations = new List <Reservation>();

                        while (reader.Read())
                        {
                            var reservation = new Reservation(int.Parse(reader["ReservationId"].ToString()),
                                                            DateTime.Parse(reader["ReservationDate"].ToString()),
                                                            int.Parse(reader["ProducerId"].ToString()),
                                                            int.Parse(reader["ActorId"].ToString())) ;
                            reservations.Add(reservation);
                        }

                        return reservations;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Reservation> GetReservationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertReservationAsync(Reservation reservation)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO 
                                     [RESERVATION](ActorId, 
                                         ProducerId, 
                                         ReservationDate) 
                                VALUES (@ActorId, 
                                         @ProducerId,
                                         @ReservationDate); SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("ActorId", reservation.ActorId);
                        cmd.Parameters.AddWithValue("ProducerId", reservation.ProducerId);
                        cmd.Parameters.AddWithValue("ReservationDate", reservation.StartDate);

                        var id = await cmd
                                         .ExecuteScalarAsync()
                                         .ConfigureAwait(false);

                        return int.Parse(id.ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
