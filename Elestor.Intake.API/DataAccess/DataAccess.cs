using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elestor.Intake.API.Interfaces;
using Elestor.Intake.API.Models;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Elestor.Intake.API.Helpers;
using System.Linq;

namespace Elestor.Intake.API.DataAccess
{
    public class DataAccess : IDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Elestor.Intake.API.DataAccess.DataAccess"/> class.
        /// </summary>
        public DataAccess()
        {

        }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public IDbConnection Connection
        {
            get
            {
            return new MySqlConnection(Constants.ConnectionString);

            }
        }
        /// <summary>
        /// Registro the specified usuario.
        /// </summary>
        /// <returns>The registro.</returns>
        /// <param name="usuario">Usuario.</param>
        public async Task<bool> Registro(Usuario usuario)
        {
                bool ret = false;

                byte[] photo = { };

                if (usuario.fotografia != null)
                {

                    //photo = usuario.fotografia.GetBytes();
                }

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
                                cmd.Parameters.Add(new MySqlParameter("thisfotografia", usuario.fotografia));
                                cmd.Parameters.Add(new MySqlParameter("thisestatus", 0));
                                cmd.Parameters.Add(new MySqlParameter("thisclientid", guid.ToString()));


                                await cmd.ExecuteNonQueryAsync();
                                conn.Close();

                                ret = true;

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                    }

                return ret;
        }
        /// <summary>
        /// Login the specified usuario.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="usuario">Usuario.</param>
        public async Task<IEnumerable<Usuario>> Login(Usuario usuario)
        {
                List<Usuario> userResponse = null;

                try
                {
                    using (IDbConnection conn = Connection)
                    {
                      
                        conn.Open();
                        
                        userResponse = new List<Usuario>();

                        var result = await conn.QueryAsync<UsuarioResponse>("usp_Usuario_Select",new {thispassword = usuario.password, thisemail = usuario.email},null,3000000,CommandType.StoredProcedure);

                        if(result.AsList().Count > 0)
                        {
                            string clientid = result.AsList()[0].clientid;

                            IEnumerable<Negocio> negocio = await this.ObtenerNegocio(clientid);

                            result.AsList()[0].negocio = negocio.AsList();

                            //IEnumerable<Producto> productos = await this.ObtenerProductos(negocio.ToList()[0]);

                            //if(productos.Any())
                            //{
                            //    result.AsList()[0].negocio[0].productos = productos.ToList();
                            //}
                            
                        }
                        else
                        {
                            result.AsList()[0].negocio = new List<Negocio>();
                        }

                        userResponse = ToResponseUser((List<UsuarioResponse>)result);

                        return userResponse;

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                return null;

            }



        /// <summary>
        /// Recuperars the cuenta.
        /// </summary>
        /// <returns>The cuenta.</returns>
        /// <param name="usuario">Usuario.</param>
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
        /// <summary>
        /// Agregars the negocio.
        /// </summary>
        /// <returns>The negocio.</returns>
        /// <param name="negocio">Negocio.</param>
        public async Task<object> AgregarNegocio(Negocio negocio)
            {
                object ret = new object();

                await Task.Run(() => {

                    try
                    {
                        //byte[] photo = negocio.fotografia.GetBytes();
                        //byte[] photo2 = negocio.fotografia2.GetBytes();

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
                                cmd.Parameters.Add(new MySqlParameter("thissubcategoria", negocio.subcategoria));
                                cmd.Parameters.Add(new MySqlParameter("thisdescripcion", negocio.descripcion));
                                cmd.Parameters.Add(new MySqlParameter("thislatitud", negocio.latitud));
                                cmd.Parameters.Add(new MySqlParameter("thislongitud", negocio.longitud));
                                cmd.Parameters.Add(new MySqlParameter("thisactive", negocio.active));
                                cmd.Parameters.Add(new MySqlParameter("thisfotografia", negocio.fotografia));
                                cmd.Parameters.Add(new MySqlParameter("thisfotografia2", negocio.fotografia2));

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

        /// <summary>
        /// Obteners the negocio.
        /// </summary>
        /// <returns>The negocio.</returns>
        /// <param name="clientid">Clientid.</param>
        public async Task<IEnumerable<Negocio>> ObtenerNegocio(string clientid)
        {
            List<Negocio> negocioResponse = null;
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    negocioResponse = new List<Negocio>();

                    var result = await conn.QueryAsync<NegocioResponse>("usp_Negocio_Select", new { thisclientid = clientid }, null, 30000, CommandType.StoredProcedure);

                    negocioResponse = ToResponseNegocio((List<NegocioResponse>)result);

                    return negocioResponse;
                
                }
            }
            catch (Exception ex)
            {
                return  null;
            }
        }


        /// <summary>
        /// Obteners the negocio.
        /// </summary>
        /// <returns>The negocio.</returns>
        /// <param name="clientid">Clientid.</param>
        public async Task<IEnumerable<Negocio>> ObtenerNegocios()
        {
            List<Negocio> negocioResponse = null;
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    negocioResponse = new List<Negocio>();

                    var result = await conn.QueryAsync<NegocioResponse>("usp_NegociosAll_Select", null, null, 30000, CommandType.StoredProcedure);

                    negocioResponse = ToResponseNegocio((List<NegocioResponse>)result);

                    return negocioResponse;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Actualizar the specified usuario.
        /// </summary>
        /// <returns>The actualizar.</returns>
        /// <param name="usuario">Usuario.</param>
        public async Task<object> Actualizar(Usuario usuario)
        {
            object ret = new object();
            byte[] photo = { };

            try
                {
                if(usuario.fotografia != null || !string.IsNullOrEmpty(usuario.fotografia))
                {
                    //photo = usuario.fotografia.GetBytes();
                }
               

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
                            cmd.Parameters.Add(new MySqlParameter("thisfotografia", usuario.fotografia));

                            ret = cmd.ExecuteNonQuery();
                            conn.Close();

                            cmd.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ret = ex.Message;
                }
           
            return ret;
        }
        /// <summary>
        /// Obteners the cat negocio.
        /// </summary>
        /// <returns>The cat negocio.</returns>
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
        /// <summary>
        /// Obteners the sub cat negocio.
        /// </summary>
        /// <returns>The sub cat negocio.</returns>
        /// <param name="id">Identifier.</param>
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
        /// <summary>
        /// Obteners the productos.
        /// </summary>
        /// <returns>The productos.</returns>
        /// <param name="negocio">Negocio.</param>
        public async Task<IEnumerable<Producto>> ObtenerProductos(Negocio negocio)
        {
            List<Producto> productos = null;
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    productos = new List<Producto>();

                    var result = await conn.QueryAsync<ProductoResponse>("usp_Producto_Select", new { thisnegocioid = negocio.clientid }, null, 30000, CommandType.StoredProcedure);

                    productos = ToResponseProductos((List<ProductoResponse>)result);

                    return productos;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Guardars the producto.
        /// </summary>
        /// <returns>The producto.</returns>
        /// <param name="producto">Producto.</param>
        public async Task<object> GuardarProducto(Producto producto)
        {
            object ret = new object();
            byte[] photo = { };

                try
                {
                  using(IDbConnection conn = Connection) 
                  {
                    if (producto.fotografia != null || !string.IsNullOrEmpty(producto.fotografia))
                    {
                        //photo = producto.fotografia.GetBytes();
                    }

                            conn.Open();


                           var result = await conn.ExecuteScalarAsync("usp_Producto_Insert", 
                                new { nombre = producto.nombre
                                    , descripcion = producto.descripcion
                                    , clave = producto.clave
                                    , estatus = producto.estatus
                                    , fotografia = producto.fotografia
                                    , precio = producto.precio
                                    , negocioid = producto.negocioid
                                    , tiempopreparacion = producto.tiempopreparacion
                                    , otracategoria = producto.otracategoria
                                    , id_catProducto = producto.id_catProducto
                                    , complemento = producto.complemento}
                                    ,null,30000,CommandType.StoredProcedure);
                        conn.Close();

                        return  1;


                        }
                    
                }
                catch (Exception ex)
                {
                    return null;
                }

        }


        /// <summary>
        /// Guardars the producto.
        /// </summary>
        /// <returns>The producto.</returns>
        /// <param name="producto">Producto.</param>
        public async Task<object> EditarProducto(Producto producto)
        {
            object ret = new object();
            byte[] photo = { };

            try
            {
                using (IDbConnection conn = Connection)
                {
                    if(producto.fotografia != null || !string.IsNullOrEmpty(producto.fotografia))
                    {
                        //photo = producto.fotografia.GetBytes();
                    }
                    
                    conn.Open();


                    var result = await conn.ExecuteScalarAsync("usp_Producto_Update",
                         new
                         {
                             nombre = producto.nombre
                             ,idproducto = producto.id_producto
                             ,descripcion = producto.descripcion
                             ,clave = producto.clave
                             ,estatus = producto.estatus
                             ,fotografia = producto.fotografia
                             ,precio = producto.precio
                             ,negocioid = producto.negocioid
                             ,tiempopreparacion = producto.tiempopreparacion
                             ,otracategoria = producto.otracategoria
                             ,id_catProducto = producto.id_catProducto
                             ,complemento = producto.complemento
                         }
                             , null, 30000, CommandType.StoredProcedure);
                    conn.Close();

                    return 1;


                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// Negocios the editar.
        /// </summary>
        /// <returns>The editar.</returns>
        /// <param name="negocio">Negocio.</param>
        public async Task<object> NegocioEditar(Negocio negocio)
        {
            byte[] photo = { },  photo2  = { };
            try
            {
                using (IDbConnection conn = Connection)
                {
                    //if(negocio.fotografia != null || !string.IsNullOrEmpty(negocio.fotografia))
                    //{
                    //    photo = negocio.fotografia.GetBytes();
                    //}
                    //if (negocio.fotografia2 != null || !string.IsNullOrEmpty(negocio.fotografia2))
                    //{
                    //    photo2 = negocio.fotografia2.GetBytes();
                    //}
                    
                  
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
                            , thissubcategoria = negocio.subcategoria
                            , thisdescripcion = negocio.descripcion
                            , thislatitud = negocio.latitud
                            , thislongitud = negocio.longitud
                            , thisactive = negocio.active
                            , thisfotografia = negocio.fotografia
                            , thisfotografia2 = negocio.fotografia2}
                            ,null,30000,CommandType.StoredProcedure);

                    return  1;

                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// Borrars the producto.
        /// </summary>
        /// <returns>The producto.</returns>
        /// <param name="producto">Producto.</param>
        public async Task<IEnumerable<Producto>> BorrarProducto(Producto producto)
        {

            List<Producto> productos = null;

            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    productos = new List<Producto>();

                    var result = await conn.QueryAsync<ProductoResponse>("usp_Producto_Delete", 
                        new { thisnegocioid = producto.negocioid, 
                        thisproductoid = producto.id_producto}, null, 30000, CommandType.StoredProcedure);

                    productos = ToResponseProductos((List<ProductoResponse>)result);

                    return productos;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CatProducto>> ObtnerCatProductoPorIdCatNegocio(string categoria)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();

                    var result = await conn.QueryAsync<CatProducto>("usp_CatProducto_Select",
                        new
                        {
                            categoria = categoria
                        }, null, 30000, CommandType.StoredProcedure);

                    return result;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Tos the response user.
        /// </summary>
        /// <returns>The response user.</returns>
        /// <param name="usuario">Usuario.</param>
        internal List<Usuario> ToResponseUser(List<UsuarioResponse> usuario)
        {
            List<Usuario> lstUsuario = new List<Usuario>();
            if (usuario.Count > 0)
            {
                lstUsuario.Add(new Usuario()
                {
                    clientid = usuario[0].clientid,
                    nombre = usuario[0].nombre,
                    apellidoMaterno = usuario[0].apellidoMaterno,
                    apellidoPaterno = usuario[0].apellidoPaterno,
                    nombreUsuario = usuario[0].nombreUsuario,
                    password = usuario[0].password,
                    confirmPassword = usuario[0].confirmPassword,
                    email = usuario[0].email,
                    numeroTelefonico = usuario[0].numeroTelefonico,
                    fotografia = usuario[0].fotografia,
                    negocio = usuario[0].negocio
                }
               );
            }

            return lstUsuario;
        }

        /// <summary>
        /// Tos the response productos.
        /// </summary>
        /// <returns>The response productos.</returns>
        /// <param name="productos">Productos.</param>
        internal List<Producto> ToResponseProductos(List<ProductoResponse> productos)
        {
            List<Producto> lstProd = new List<Producto>();

            if (productos.Count > 0)
            {
                foreach (var prod in productos)
                {
                    lstProd.Add(new Producto()
                    {
                        id_producto = prod.id_producto,
                        nombre = prod.nombre,
                        descripcion = prod.descripcion,
                        clave = prod.clave,
                        estatus = prod.estatus,
                        fotografia = prod.fotografia,
                        precio = prod.precio,
                        negocioid = prod.negocioid,
                        tiempopreparacion = prod.tiempopreparacion,
                        otracategoria = prod.otracategoria,
                        complemento = prod.complemento,
                        id_catProducto = prod.id_catProducto
                    });
                }
            }

            return lstProd;

        }

        internal List<Negocio> ToResponseNegocio(List<NegocioResponse> negocio)
        {
            List<Negocio> lstUsuario = new List<Negocio>();

            if (negocio.Count > 0)
            {
                foreach (var item in negocio)
                {
                    lstUsuario.Add(new Negocio()
                    {
                        clientid = item.clientid,
                        negocioid = item.negocioid,
                        nombre = item.nombre,
                        callenumero = item.callenumero,
                        colonia = item.colonia,
                        ciudad = item.ciudad,
                        estado = item.estado,
                        codigopostal = item.codigopostal,
                        horaapertura = item.horaapertura,
                        horacierre = item.horacierre,
                        categoria = item.categoria,
                        subcategoria = item.subcategoria,
                        FK_subcategoria = item.FK_subcategoria,
                        descripcion = item.descripcion,
                        latitud = item.latitud,
                        longitud = item.longitud,
                        active = item.active,
                        fotografia = item.fotografia,
                        fotografia2 = item.fotografia2
                    });
                }

            }

            return lstUsuario;

        }


    }
}
