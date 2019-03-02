﻿    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Elestor.Intake.API.Interfaces;
    using Elestor.Intake.API.Models;
    using MySql.Data.MySqlClient;
    using Dapper;
    using System.Data.SqlClient;
    using System.Data;

    namespace Elestor.Intake.API.DataAccess
    {
        public class DataAccess : IDataAccess
        {
        private string guid = String.Empty;

            public DataAccess()
            {

            }
            public IDbConnection Connection
            {
                get
                {
                    return new MySqlConnection("server=localhost;uid=root;pwd=sim0n11.;database=ELESTOR");
                }
            }

            public async Task<object> Registro(Usuario usuario)
            {
                object ret = new object();

                await Task.Run(() => {

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection("server=localhost;uid=root;pwd=sim0n11.;database=ELESTOR"))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("usp_Usuario_Insert", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                conn.Open();
                                var guid = System.Guid.NewGuid();

                                cmd.Parameters.Add(new MySqlParameter("thisnombre", usuario.nombre));
                                cmd.Parameters.Add(new MySqlParameter("thisapellidoPaterno", usuario.apellidoPaterno));
                                cmd.Parameters.Add(new MySqlParameter("thisapellidoMaterno", usuario.apellidoMaterno));
                                cmd.Parameters.Add(new MySqlParameter("thisnombreUsuario", usuario.nombreUsuario));
                                cmd.Parameters.Add(new MySqlParameter("thispassword", usuario.password));
                                cmd.Parameters.Add(new MySqlParameter("thisemail", usuario.email));
                                cmd.Parameters.Add(new MySqlParameter("thisnumeroTelefonico", usuario.numeroTelefonico));
                                cmd.Parameters.Add(new MySqlParameter("thisestatus", 1));
                                cmd.Parameters.Add(new MySqlParameter("thisclientid", guid.ToString()));


                                ret = cmd.ExecuteNonQuery();
                                conn.Close();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ret = ex.Message;
                    }
                });
                return ret;
            }

            public async Task<IEnumerable<Usuario>> Login(Usuario usuario)
            {
            
                try
                {
                    using (IDbConnection conn = Connection)
                    {
                        conn.Open();

                        var result = await conn.QueryAsync<Usuario>("usp_Usuario_Select",new {thispassword = usuario.password, thisemail = usuario.email},null,30000,CommandType.StoredProcedure);

                        return result;

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                return null;

            }

            public async Task<object> RecuperarCuenta(Usuario usuario)
            {
                object ret = new object();

                await Task.Run(() => {

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection("server=localhost;uid=root;pwd=sim0n11.;database=ELESTOR"))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("usp_Usuario_Update", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                conn.Open();

                                cmd.Parameters.Add(new MySqlParameter("thispassword", usuario.password));
                                cmd.Parameters.Add(new MySqlParameter("thisemail", usuario.email));

                                ret = cmd.ExecuteNonQuery();
                                conn.Close();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ret = ex.Message;
                    }
                });
                return ret;
            }

            public async Task<object> AgregarNegocio(Negocio negocio)
            {
                object ret = new object();

                await Task.Run(() => {

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection("server=localhost;uid=root;pwd=sim0n11.;database=ELESTOR"))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("usp_Negocio_Insert", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                conn.Open();
                                guid = System.Guid.NewGuid().ToString();

                                cmd.Parameters.Add(new MySqlParameter("thisclientid", negocio.clientid));
                                cmd.Parameters.Add(new MySqlParameter("thisnegocioid", negocio.negocioid));
                                cmd.Parameters.Add(new MySqlParameter("thisnombre", negocio.nombre));
                                cmd.Parameters.Add(new MySqlParameter("thiscallenumero", negocio.callenumero));
                                cmd.Parameters.Add(new MySqlParameter("thiscolonia", negocio.colonia));
                                cmd.Parameters.Add(new MySqlParameter("thisciudad", negocio.ciudad));
                                cmd.Parameters.Add(new MySqlParameter("thisestado", negocio.estado));
                                cmd.Parameters.Add(new MySqlParameter("thiscodigopostal", negocio.codigopostal));
                                cmd.Parameters.Add(new MySqlParameter("thishoraapertura", negocio.horaapertura));
                                cmd.Parameters.Add(new MySqlParameter("thishoracierre", negocio.horacierre));
                                cmd.Parameters.Add(new MySqlParameter("thiscategoria", negocio.categoria));
                                cmd.Parameters.Add(new MySqlParameter("thissubcategoria", negocio.subcategoria));
                                cmd.Parameters.Add(new MySqlParameter("thisdescripcion", negocio.descripcion));
                                cmd.Parameters.Add(new MySqlParameter("thislatitud", negocio.latitud));
                                cmd.Parameters.Add(new MySqlParameter("thislongitud", negocio.longitud));



                                ret = cmd.ExecuteNonQuery();
                                conn.Close();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ret = ex.Message;
                    }
                });
                return ret;
            }


        public async Task<IEnumerable<Negocio>> ObtenerNegocio(string clientid)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var result = await conn.QueryAsync<Negocio>("usp_Negocio_Select", new { thisclientid = clientid }, null, 30000, CommandType.StoredProcedure);

                    return result;

                }
            }
            catch (Exception ex)
            {
                return  null;
            }
        }

        public async Task<object> Actualizar(Usuario usuario)
        {
            object ret = new object();

            await Task.Run(() => {

                try
                {
                    using (MySqlConnection conn = new MySqlConnection("server=localhost;uid=root;pwd=sim0n11.;database=ELESTOR"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("usp_Cuenta_Update", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            conn.Open();


                            cmd.Parameters.Add(new MySqlParameter("thisnombre", usuario.nombre));
                            cmd.Parameters.Add(new MySqlParameter("thisapellidoPaterno", usuario.apellidoPaterno));
                            cmd.Parameters.Add(new MySqlParameter("thisapellidoMaterno", usuario.apellidoMaterno));
                            cmd.Parameters.Add(new MySqlParameter("thisnombreUsuario", usuario.nombreUsuario));
                            cmd.Parameters.Add(new MySqlParameter("thispassword", usuario.password));
                            cmd.Parameters.Add(new MySqlParameter("thisemail", usuario.email));
                            cmd.Parameters.Add(new MySqlParameter("thisnumeroTelefonico", usuario.numeroTelefonico));
                            cmd.Parameters.Add(new MySqlParameter("thisestatus", 1));
                            cmd.Parameters.Add(new MySqlParameter("thisclientid", usuario.clientid));


                            ret = cmd.ExecuteNonQuery();
                            conn.Close();

                        }
                    }
                }
                catch (Exception ex)
                {
                    ret = ex.Message;
                }
            });
            return ret;
        }
    }
}
