    using System;
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
            public DataAccess()
            {

            }
            public IDbConnection Connection
            {
                get
                {
                return new MySqlConnection(Constants.ConnectionString);

                }
            }

            public async Task<Usuario> Registro(Usuario usuario)
            {
                Usuario ret = null;

                await Task.Run(() => {

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(Constants.ConnectionString))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("usp_Usuario_Insert", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;

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


                                MySqlDataReader dataReader = cmd.ExecuteReader();

                                if(dataReader.HasRows)
                                {
                                    ret = new Usuario();

                                    while (dataReader.Read())
                                    {
                                        ret.nombre = dataReader["nombre"].ToString();
                                        ret.apellidoPaterno = dataReader["apellidoPaterno"].ToString();
                                        ret.apellidoMaterno = dataReader["apellidoMaterno"].ToString();
                                        ret.nombreUsuario = dataReader["nombreUsuario"].ToString();
                                        ret.password = dataReader["password"].ToString();
                                        ret.email = dataReader["email"].ToString();
                                        ret.numeroTelefonico = dataReader["numeroTelefonico"].ToString();
                                        ret.clientid = dataReader["clientid"].ToString();
                                    }
                                }
                                conn.Close();

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ret = null;
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

                        var result = await conn.QueryAsync<Usuario>("usp_Usuario_Select",new {thispassword = usuario.password, thisemail = usuario.email},null,3000000,CommandType.StoredProcedure);

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
                        using (MySqlConnection conn = new MySqlConnection(Constants.ConnectionString))
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
                        using (MySqlConnection conn = new MySqlConnection(Constants.ConnectionString))
                        {
                            using (MySqlCommand cmd = new MySqlCommand("usp_Negocio_Insert", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                conn.Open();
                                var guid = System.Guid.NewGuid().ToString();
                                negocio.negocioid = guid.ToString();

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
                                cmd.Parameters.Add(new MySqlParameter("thissubcategoria", negocio.FK_subcategoria));
                                cmd.Parameters.Add(new MySqlParameter("thisdescripcion", negocio.descripcion));
                                cmd.Parameters.Add(new MySqlParameter("thislatitud", negocio.latitud));
                                cmd.Parameters.Add(new MySqlParameter("thislongitud", negocio.longitud));
                                cmd.Parameters.Add(new MySqlParameter("thisactive", negocio.active));

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
                    using (MySqlConnection conn = new MySqlConnection(Constants.ConnectionString))
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

        public async Task<IEnumerable<CatNegocio>> ObtenerCatNegocio()
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var result = await conn.QueryAsync<CatNegocio>("usp_CatNegocio_Select", null, null, 30000, CommandType.StoredProcedure);

                    return result;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<SubCatNegocio>> ObtenerSubCatNegocio(int id)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var result = await conn.QueryAsync<SubCatNegocio>("usp_SubCatNegocio_Select", new { catid = id }, null, 30000, CommandType.StoredProcedure);

                    return result;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Producto>> ObtenerProductos(int clientid)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var result = await conn.QueryAsync<Producto>("usp_Producto_Select", new { clientid = clientid }, null, 30000, CommandType.StoredProcedure);

                    return result;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<object> GuardarProducto(Producto producto)
        {
            object ret = new object();

            await Task.Run(() => {

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Constants.ConnectionString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("usp_Producto_Insert", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            conn.Open();


                           


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

        public async Task<object> NegocioEditar(Negocio negocio)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var result = conn.ExecuteScalar("usp_Negocio_Update", 
                        new { thisclientid = negocio.clientid
                            , thisnegocioid = negocio.negocioid
                            , thisnombre = negocio.nombre
                            , thiscallenumero = negocio.callenumero
                            , thiscolonia = negocio.colonia
                            , thisciudad = negocio.ciudad
                            , thisestado = negocio.estado
                            , thiscodigopostal = negocio.codigopostal
                            , thishoraapertura = negocio.horaapertura
                            , thishoracierre = negocio.horacierre
                            , thiscategoria = negocio.categoria
                            , thissubcategoria = negocio.FK_subcategoria
                            , thisdescripcion = negocio.descripcion
                            , thislatitud = negocio.latitud
                            , thislongitud = negocio.longitud
                            , thisactive = negocio.active},null,30000,CommandType.StoredProcedure);

                    return  1;

                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
